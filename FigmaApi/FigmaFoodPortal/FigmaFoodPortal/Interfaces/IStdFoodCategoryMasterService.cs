using FoodPortal.Models;
using FoodPortal.Models.DTO;
using FoodPortal.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FoodPortal.Interfaces
{
    public interface IStdFoodCategoryMasterService
    {
        Task<List<ViewStdFoodCategoryMaster>> View_All_StdFoodCategoryMasters();
    }
}
