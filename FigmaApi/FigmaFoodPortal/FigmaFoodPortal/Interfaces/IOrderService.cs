using FoodPortal.Models;
using FoodPortal.Models.DTO;
using FoodPortal.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace FoodPortal.Interfaces
{
    public interface IOrderService
    {
        Task<RequestOrder> Add_Order(RequestOrder Order);
        Task<List<AvailabilityDTO>> GetUnAvailableDate(DateTime date);
        Task<List<AvailabilityDTO>> GetAvailableTimeSlot(DateTime date);
        Task<UserDetailsDTO> GetUserDetails(string id);
    }
}
