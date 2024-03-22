namespace FoodPortal.ViewModel
{
    public class ViewPlateSize
    {
        public int Id { get; set; }

        public string PlateType { get; set; } = null!;

        public decimal? VegPlateCost { get; set; }

        public decimal? NonvegPlateCost { get; set; }

        public int? Starter { get; set; }

        public int? Curry { get; set; }

        public int? Bread { get; set; }

        public int? Rice { get; set; }

        public int? Dessert { get; set; }

        public bool? SodaorWater { get; set; }

        public decimal? BothCost { get; set; }
    }
}
