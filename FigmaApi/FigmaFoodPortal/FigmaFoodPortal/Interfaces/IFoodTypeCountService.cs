using FoodPortal.Models;
using FoodPortal.Models.DTO;
using FoodPortal.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace FoodPortal.Interfaces
{
    public interface IFoodTypeCountService
    {
        Task<List<RequestFoodTypeCount>> Add_FoodTypeCount(List<RequestFoodTypeCount> FoodTypeCount);
    }
}
