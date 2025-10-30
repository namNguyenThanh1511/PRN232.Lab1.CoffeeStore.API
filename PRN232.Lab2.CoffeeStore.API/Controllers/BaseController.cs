using Microsoft.AspNetCore.Mvc;
using PRN232.Lab2.CoffeeStore.API.Models;

namespace PRN232.Lab2.CoffeeStore.API.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        // Success responses
        protected ActionResult<ApiResponse<T>> Ok<T>(T data, string message = "Thao tác thành công")
        {
            var response = ResponseBuilder.SuccessWithData(data, message);
            return base.Ok(response);
        }

        protected ActionResult<ApiResponse> Ok(string message = "Thao tác thành công")
        {
            var response = ResponseBuilder.SuccessWithMessage(message);
            return base.Ok(response);
        }

        protected ActionResult<ApiResponse<T>> Created<T>(T data, string message = "Tạo thành công")
        {
            var response = ResponseBuilder.SuccessWithData(data, message);
            return StatusCode(201, response);
        }

        protected ActionResult<ApiResponse> Created(string message = "Tạo thành công")
        {
            var response = ResponseBuilder.SuccessWithMessage(message);
            return StatusCode(201, response);
        }

        // Error responses
        protected ActionResult<ApiResponse> BadRequest(string message = "Yêu cầu không hợp lệ", List<ApiError>? errors = null)
        {
            var response = ResponseBuilder.BadRequest(message, errors);
            return base.BadRequest(response);
        }

        // Generic version (keep existing)
        protected ActionResult<ApiResponse<T>> BadRequest<T>(string message = "Yêu cầu không hợp lệ", List<ApiError>? errors = null)
        {
            var response = ResponseBuilder.BadRequest(message, errors);
            return base.BadRequest(response);
        }

        protected ActionResult<ApiResponse> ValidationError(string message = "Dữ liệu không hợp lệ")
        {
            var response = ResponseBuilder.ValidationError(ModelState, message);
            return base.BadRequest(response);
        }

        protected ActionResult<ApiResponse> NotFound(string message = "Không tìm thấy dữ liệu")
        {
            var response = ResponseBuilder.NotFound(message);
            return base.NotFound(response);
        }

        protected ActionResult<ApiResponse> Unauthorized(string message = "Không có quyền truy cập")
        {
            var response = ResponseBuilder.Unauthorized(message);
            return base.Unauthorized(response);
        }

        protected ActionResult<ApiResponse> Forbidden(string message = "Bị cấm truy cập")
        {
            var response = ResponseBuilder.Forbidden(message);
            return StatusCode(403, response);
        }

        protected ActionResult<ApiResponse> InternalServerError(string message = "Lỗi máy chủ nội bộ")
        {
            var response = ResponseBuilder.InternalServerError(message);
            return StatusCode(500, response);
        }
    }
}
