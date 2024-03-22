namespace FoodPortal.RequestModel
{
    public class RequestOrder
    {
        public int Id { get; set; }

        public string? Oid { get; set; }

        public int? DeliveryOptionId { get; set; }

        public int? PlateSizeId { get; set; }

        public string? AdditionalNote { get; set; }

        public string? Address { get; set; }

        public DateTime? Date { get; set; }

        public int? GroupSizeId { get; set; }

        public int? TimeSlotId { get; set; }

        public string? UserName { get; set; }

        public string? AdditionalAllergy { get; set; }

        public string? ContactName { get; set; }

        public string? ContactNumber { get; set; }

        public string? ContactEmail { get; set; }

        public int? DrinkId { get; set; }
    }
}
