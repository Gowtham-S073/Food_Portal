namespace FoodPortal.ViewModel
{
    public class ViewAddOnsMaster
    {
        public int Id { get; set; }

        public string AddOnsName { get; set; } = null!;

        public decimal? UnitPrice { get; set; }

        public string? AddOnsImage { get; set; }

        public bool? IsAvailable { get; set; }

    }
}
