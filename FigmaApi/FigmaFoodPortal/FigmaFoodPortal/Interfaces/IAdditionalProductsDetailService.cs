using FoodPortal.Models;
using FoodPortal.Models.DTO;
using FoodPortal.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace FoodPortal.Interfaces
{
    public interface IAdditionalProductsDetailService
    {
        Task<List<RequestAdditionalProductsDetail>> Add_AdditionalProductsDetail(List<RequestAdditionalProductsDetail> AdditionalProductsDetail);
       
    }
}
