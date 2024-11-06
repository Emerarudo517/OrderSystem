using System.Text.Json.Serialization;

namespace OrderSystem.Core.Entities
{
    public class OrderItem
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int SoLuong { get; set; }
        public decimal Gia { get; set; }

        [JsonIgnore]
        public Order Order { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
