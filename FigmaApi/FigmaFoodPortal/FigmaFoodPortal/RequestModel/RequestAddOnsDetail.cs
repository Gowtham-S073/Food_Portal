namespace FoodPortal.RequestModel
{
    public class RequestAddOnsDetail
    {
        public long Id { get; set; }

        public int? AddOnsId { get; set; }

        public int? OrderId { get; set; }

        public int? Quantity { get; set; }

        public decimal? Cost { get; set; }

    }
}
