namespace shopApi.Models
{
    public class SaleHistory
    {
        public int Id { get; set; }
        public string Cid { get; set; }
        public Decimal Total { get; set; }
        public string Product { get; set; }
    }
}
