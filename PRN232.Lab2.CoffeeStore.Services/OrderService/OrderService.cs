using Microsoft.EntityFrameworkCore;
using PRN232.Lab2.CoffeeStore.Repositories;
using PRN232.Lab2.CoffeeStore.Repositories.Entities;
using PRN232.Lab2.CoffeeStore.Repositories.UnitOfWork;
using PRN232.Lab2.CoffeeStore.Services.Exceptions;
using PRN232.Lab2.CoffeeStore.Services.Models.Order;
using PRN232.Lab2.CoffeeStore.Services.PaymentService;
using PRN232.Lab2.CoffeeStore.Services.UserService;

namespace PRN232.Lab2.CoffeeStore.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPaymentService _paymentService;
        public OrderService(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IPaymentService paymentService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _paymentService = paymentService;
        }

        public async Task<(List<OrderPlacingResponse>, MetaData metaData)> GetAllOrders(OrderSearchParams searchParams)
        {
            (string userId, string role) = _currentUserService.GetCurrentUser();

            var query = _unitOfWork.Orders.Query();

            // Filtering
            if (!string.IsNullOrWhiteSpace(searchParams.Search))
            {
                query = query.Where(o =>
                    o.Status.ToString().Contains(searchParams.Search) ||
                    (o.Customer != null && o.Customer.FullName.Contains(searchParams.Search))
                );
            }

            // Restrict to current user if not admin
            if (role != Role.Admin.ToString())
            {
                query = query.Where(o => o.CustomerId == Guid.Parse(userId));
            }

            // Sorting
            string sortBy = string.IsNullOrWhiteSpace(searchParams.SortBy) ? "OrderDate" : searchParams.SortBy;
            string sortOrder = string.IsNullOrWhiteSpace(searchParams.SortOrder) ? "desc" : searchParams.SortOrder.ToLower();

            switch (sortBy.ToLower())
            {
                case "orderdate":
                    query = sortOrder == "asc" ? query.OrderBy(o => o.OrderDate) : query.OrderByDescending(o => o.OrderDate);
                    break;
                case "totalamount":
                    query = sortOrder == "asc" ? query.OrderBy(o => o.TotalAmount) : query.OrderByDescending(o => o.TotalAmount);
                    break;
                case "status":
                    query = sortOrder == "asc" ? query.OrderBy(o => o.Status) : query.OrderByDescending(o => o.Status);
                    break;
                default:
                    query = sortOrder == "asc" ? query.OrderBy(o => o.OrderDate) : query.OrderByDescending(o => o.OrderDate);
                    break;
            }



            // Include navigation properties before passing to repository
            query = query.Include(o => o.OrderItems)
                         .ThenInclude(oi => oi.Product)
                         .Include(o => o.Customer);

            // Use repository for paging
            var pagedOrders = await _unitOfWork.Orders.GetAllOrders(query, searchParams.PageNumber, searchParams.PageSize);

            // Select only requested fields
            var selectFields = searchParams.SelectFields;

            var result = pagedOrders.Select(order =>
            {
                var response = new OrderPlacingResponse
                {
                    Id = order.Id
                };

                // If no select fields specified, populate all fields
                if (selectFields == null || selectFields.Count == 0)
                {
                    response.OrderDate = order.OrderDate;
                    response.Status = order.Status.ToString();
                    response.TotalAmount = order.TotalAmount;
                    response.CustomerId = order.CustomerId ?? Guid.Empty;
                    response.OrderItems = order.OrderItems.Select(oi => new OrderItemResponse
                    {
                        Id = oi.Id,
                        ProductId = oi.ProductId,
                        ProductName = oi.Product?.Name ?? "",
                        Quantity = oi.Quantity,
                        UnitPrice = oi.UnitPrice
                    }).ToList();
                }
                else
                {
                    // Always include Id, populate only selected fields
                    foreach (var field in selectFields)
                    {
                        switch (field)
                        {
                            case OrderSearchParams.SelectField.OrderDate:
                                response.OrderDate = order.OrderDate ?? null;
                                break;
                            case OrderSearchParams.SelectField.Status:
                                response.Status = order.Status.ToString();
                                break;
                            case OrderSearchParams.SelectField.TotalAmount:
                                response.TotalAmount = order.TotalAmount;
                                break;
                            case OrderSearchParams.SelectField.Customer:
                                response.CustomerId = order.CustomerId ?? null;
                                break;
                            case OrderSearchParams.SelectField.OrderItems:
                                response.OrderItems = order.OrderItems.Select(oi => new OrderItemResponse
                                {
                                    Id = oi.Id,
                                    ProductId = oi.ProductId,
                                    ProductName = oi.Product?.Name ?? "",
                                    Quantity = oi.Quantity,
                                    UnitPrice = oi.UnitPrice
                                }).ToList();
                                break;
                        }
                    }
                }

                return response;
            });

            return (result.ToList(), pagedOrders.MetaData);
        }

        public async Task<(OrderPlacingResponse order, string paymentUrl)> PlaceOrder(OrderPlacingRequest request)
        {
            (string userId, string role) = _currentUserService.GetCurrentUser();
            var user = await _unitOfWork.Users.FindOneAsync(u => u.Id == Guid.Parse(userId)) ?? throw new NotFoundException("Not found user");
            if (role != Role.Customer.ToString() && role != Role.Admin.ToString())
            {
                throw new UnauthorizedAccessException("Only customers can place orders and admin");
            }
            // Ensure unique ProductId in OrderItems
            if (request.OrderItems.Select(oi => oi.ProductId).Distinct().Count() != request.OrderItems.Count)
            {
                throw new InvalidOperationException("Each product in the order must be unique.");
            }
            if (request.OrderItems.Count == 0)
            {
                throw new InvalidOperationException("Order must have at least one item.");
            }
            try
            {
                await _unitOfWork.BeginTransaction();
                var order = new Order
                {
                    CustomerId = Guid.Parse(userId),
                    OrderDate = DateTime.UtcNow,
                    Status = OrderStatus.Pending,
                };
                await _unitOfWork.Orders.AddAsync(order);
                await _unitOfWork.SaveChangesAsync();  // Save để có order.Id

                var products = new List<Product>();
                foreach (var item in request.OrderItems)
                {
                    var product = await _unitOfWork.Products.FindOneAsync(p => p.Id == item.ProductId)
                                  ?? throw new NotFoundException($"Product with ID {item.ProductId} not found.");
                    products.Add(product);
                }

                var orderItems = request.OrderItems.Select(itemReq =>  // Rename để rõ
                {
                    var product = products.First(p => p.Id == itemReq.ProductId);
                    if (itemReq.Quantity > product.Stock)
                    {
                        throw new InvalidOperationException($"Insufficient stock for product {product.Name}. Available: {product.Stock}, Requested: {itemReq.Quantity}");
                    }
                    product.Stock -= itemReq.Quantity;
                    _unitOfWork.Products.Update(product);

                    var orderItem = new OrderDetail
                    {
                        OrderId = order.Id,
                        ProductId = product.Id,
                        Quantity = itemReq.Quantity,
                        UnitPrice = product.Price
                    };
                    return orderItem;
                }).ToList();

                order.OrderItems = orderItems;
                order.TotalAmount = orderItems.Sum(item => item.Quantity * item.UnitPrice);
                await _unitOfWork.SaveChangesAsync();  // Save OrderItems và TotalAmount
                await _unitOfWork.CommitTransaction();

                var paymentUrl = await _paymentService.CreatePaymentLink(order);

                return (new OrderPlacingResponse
                {
                    Id = order.Id,
                    OrderDate = order.OrderDate,
                    Status = order.Status.ToString(),
                    TotalAmount = order.TotalAmount,
                    CustomerId = order.CustomerId.Value,
                    OrderItems = order.OrderItems.Select(oi => new OrderItemResponse
                    {
                        Id = oi.Id,
                        ProductId = oi.ProductId,
                        Quantity = oi.Quantity,
                        UnitPrice = oi.UnitPrice
                    }).ToList()
                }, paymentUrl);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<bool> ProcessPayingOrder(OrderPayingRequest request)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId) ?? throw new NotFoundException("Order not found");
            try
            {
                await _unitOfWork.BeginTransaction();
                order.Status = OrderStatus.Completed;
                _unitOfWork.Orders.Update(order);
                var payment = new Payment
                {
                    OrderId = order.Id,
                    PaymentDate = DateTime.UtcNow,
                    Amount = order.TotalAmount,
                    Method = PaymentMethod.OnlineBanking,
                    Status = PaymentStatus.Completed
                };
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }

        }
    }
}
