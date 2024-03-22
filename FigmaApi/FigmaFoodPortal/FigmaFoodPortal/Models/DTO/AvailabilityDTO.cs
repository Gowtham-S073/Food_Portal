namespace FoodPortal.Models.DTO
{
    public class AvailabilityDTO
    {
        public DateTime? UnavailableDate { get; set; }

        public int? Count { get; set; }

        public string? availableTimeSlot { get; set; }
    }
}
