using FoodPortal.Models;
using FoodPortal.Models.DTO;
using FoodPortal.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace FoodPortal.Interfaces
{
    public interface IAddOnsDetailService
    {
        Task<List<RequestAddOnsDetail>> Add_AddOnsDetail(List<RequestAddOnsDetail> AddOnsDetail);
      
    }
}
