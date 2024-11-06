namespace OrderSystem.Core.Entities
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime NgayTao { get; set; }
        public string TrangThai { get; set; } = "Pending";
        public decimal TongTien { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
