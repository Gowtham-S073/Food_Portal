namespace FoodPortal.Models.DTO
{
    public class TrackDTO
    {
        public string? TrackId { get; set; }
        public string? OrderId { get; set; }
        public DateTime? DeliveryDate  { get; set; }
        public string? DeliveryTime { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Allergies { get; set; }
        public string? PlateSize { get; set; }
        public string? AdditionalNote { get; set; }




        public string? Menu { get; set; }

        public string? Foodtype { get; set; }
        public string? DeliveryType { get; set; }
        public string? GroupSize { get; set; }
        public int? Count { get; set; }
        public decimal? PlateCost { get; set; }
       /* public string? Addons { get; set; }
        public decimal? AddonsCost { get; set; }    */

        public List<Addons>? Additional { get; set; }

        public List<AdditionalProductDTO>? AdditionalProductInfo { get; set; }




    }
}
