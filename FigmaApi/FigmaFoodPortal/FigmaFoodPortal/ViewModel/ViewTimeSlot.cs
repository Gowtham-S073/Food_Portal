namespace FoodPortal.ViewModel
{
    public class ViewTimeSlot
    {
        public int Id { get; set; }

        public string AddTimeSlot { get; set; } = null!;

        public TimeSpan? StartTime { get; set; }
    }
}
