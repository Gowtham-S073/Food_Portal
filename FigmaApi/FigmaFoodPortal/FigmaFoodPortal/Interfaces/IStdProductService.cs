using FoodPortal.Models;
using FoodPortal.Models.DTO;
using FoodPortal.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FoodPortal.Interfaces
{
    public interface IStdProductService
    {
        Task<List<ViewStdProduct>> View_All_StdProducts();
        Task<List<ViewStdProduct>> View_by_category_StdProducts(int cat);
    }
}
