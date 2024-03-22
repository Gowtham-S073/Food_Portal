using FoodPortal.Models;
using FoodPortal.Models.DTO;
using FoodPortal.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FoodPortal.Interfaces
{
    public interface IAdditionalProductService
    {
        
        Task<List<ViewAdditionalProduct>> View_by_category_AdditionalProducts(int cat);
        Task<List<ViewAdditionalProduct>> View_by_foodtype_AdditionalProducts(bool isveg, int cat);
    }
}
