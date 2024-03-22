using FoodPortal.Models;
using FoodPortal.Models.DTO;
using FoodPortal.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace FoodPortal.Interfaces
{
    public interface IAllergyDetailService
    {
        Task<List<RequestAllergyDetail>> Add_AllergyDetail(List<RequestAllergyDetail> AllergyDetail);
        Task<string> Getallergies(string id);
    }
}
