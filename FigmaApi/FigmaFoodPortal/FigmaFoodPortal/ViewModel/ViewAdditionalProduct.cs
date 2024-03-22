namespace FoodPortal.ViewModel
{
    public class ViewAdditionalProduct
    {
        public int Id { get; set; }

        public int? AdditionalCategoryId { get; set; }

        /*        private string? _additionalProductsName;
        */
        public string AdditionalProductsName { get; set; } = null!;

        /*public string? AdditionalProductsName
        {
            get => _additionalProductsName;
            set => _additionalProductsName = value + " kannappan";
        }*/
        public bool? IsVeg { get; set; }

        public decimal? UnitPrice { get; set; }

        public string? AdditionalProductsImages { get; set; }

        public bool? IsAvailable { get; set; }
    }
}
