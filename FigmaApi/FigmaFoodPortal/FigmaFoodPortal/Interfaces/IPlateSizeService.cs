using FoodPortal.Models;
using FoodPortal.Models.DTO;
using FoodPortal.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FoodPortal.Interfaces
{
    public interface IPlateSizeService
    {
        Task<List<ViewPlateSize>> View_All_PlateSizes();
    }
}
