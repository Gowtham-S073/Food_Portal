namespace FoodPortal.Models.DTO
{
    public class AdditionalProductDTO
    {
        public string? AdditionalProductName { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }

        public decimal? PriceTotal { get; set; }
    }
}
