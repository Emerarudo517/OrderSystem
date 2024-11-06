namespace OrderSystem.Core.Entities
{
    public class Product
    {
        public int ProductID { get; set; }
        public string TenSanPham { get; set; } = string.Empty;
        public decimal Gia { get; set; }
        public int SoLuongTon { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
