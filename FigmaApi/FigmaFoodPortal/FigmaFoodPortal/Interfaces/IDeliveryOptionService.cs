using FoodPortal.Models;
using FoodPortal.Models.DTO;
using FoodPortal.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FoodPortal.Interfaces
{
    public interface IDeliveryOptionService
    {
        Task<List<ViewDeliveryOption>> View_All_DeliveryOptions();
    }
}
