namespace FoodPortal.ViewModel
{
    public class ViewStdProduct
    {
        public int Id { get; set; }

        public int? CategoriesId { get; set; }

        public string? ProductsName { get; set; }

        public int? IsVeg { get; set; }

        public string? FoodProductImage { get; set; }

        public bool? IsVegan { get; set; }

        public bool? IsGluten { get; set; }

        public bool? IsSpicy { get; set; }
    }
}
