namespace shopApi.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public int Amount { get; set; }
        public string Image { get; set; }
    }
}
