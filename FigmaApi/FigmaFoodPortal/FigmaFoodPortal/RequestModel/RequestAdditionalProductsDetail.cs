namespace FoodPortal.RequestModel
{
    public class RequestAdditionalProductsDetail
    {
        public long Id { get; set; }

        public int? AdditionalProductsId { get; set; }

        public int? OrderId { get; set; }

        public int? Quantity { get; set; }

        public decimal? Cost { get; set; }
    }
}
