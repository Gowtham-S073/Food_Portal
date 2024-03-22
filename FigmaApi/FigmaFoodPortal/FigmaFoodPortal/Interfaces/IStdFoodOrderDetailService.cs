using FoodPortal.Models;
using FoodPortal.Models.DTO;
using FoodPortal.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace FoodPortal.Interfaces
{
    public interface IStdFoodOrderDetailService
    {
        Task<List<RequestStdFoodOrderDetail>> Add_StdFoodOrderDetail(List<RequestStdFoodOrderDetail> StdFoodOrderDetails);
        Task<string> Getmenu(string id);


    }
}
