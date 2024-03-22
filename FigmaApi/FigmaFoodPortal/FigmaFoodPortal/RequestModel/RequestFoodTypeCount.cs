namespace FoodPortal.RequestModel
{
    public class RequestFoodTypeCount
    {
        public int Id { get; set; }

        public int? OrderId { get; set; }

        public int? FoodTypeCount1 { get; set; }

        public bool? IsVeg { get; set; }

        public int? PlateSizeId { get; set; }
    }
}
