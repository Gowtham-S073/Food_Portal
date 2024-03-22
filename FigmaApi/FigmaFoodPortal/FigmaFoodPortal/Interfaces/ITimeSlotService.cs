using FoodPortal.Models;
using FoodPortal.Models.DTO;
using FoodPortal.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FoodPortal.Interfaces
{
    public interface ITimeSlotService
    {
        Task<List<ViewTimeSlot>> View_All_TimeSlots();
    }
}
