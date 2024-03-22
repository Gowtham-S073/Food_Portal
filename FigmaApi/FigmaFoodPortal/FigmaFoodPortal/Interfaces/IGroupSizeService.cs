using FoodPortal.Models;
using FoodPortal.Models.DTO;
using FoodPortal.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FoodPortal.Interfaces
{
    public interface IGroupSizeService
    {
        Task<List<ViewGroupSize>> View_All_GroupSizes();

    }
}
