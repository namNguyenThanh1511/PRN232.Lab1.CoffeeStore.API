using System.ComponentModel;
using System.Text.Json.Serialization;
namespace PRN232.Lab2.CoffeeStore.Services.Models.Order
{
    public class OrderSearchParams : RequestParams
    {
        public string Search { get; set; }
        public string SortBy { get; set; } = "OrderDate"; // Default sort by OrderDate
        public string SortOrder { get; set; } = "desc"; // Default sort order descending
        public string Field { get; set; } = "All"; // Default filter by all

        public List<SelectField> SelectFields { get; set; } = new List<SelectField>();

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum SelectField
        {
            [Description("OrderDate")]
            OrderDate,
            [Description("TotalAmount")]
            TotalAmount,
            [Description("Status")]
            Status,
            [Description("Customer")]
            Customer,
            [Description("OrderItems")]
            OrderItems,
        }

    }
}
