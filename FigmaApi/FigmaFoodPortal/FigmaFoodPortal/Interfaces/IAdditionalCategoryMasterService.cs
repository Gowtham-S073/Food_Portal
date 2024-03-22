using FoodPortal.Models;
using FoodPortal.Models.DTO;
using FoodPortal.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FoodPortal.Interfaces
{
    public interface IAdditionalCategoryMasterService
    {
        Task<List<ViewAdditionalCategoryMaster>> View_All_AdditionalCategoryMasters();

    }
}
