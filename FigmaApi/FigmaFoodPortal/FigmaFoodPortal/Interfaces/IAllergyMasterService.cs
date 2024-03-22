using FoodPortal.Models.DTO;
using FoodPortal.Models;
using FoodPortal.ViewModel;

namespace FoodPortal.Interfaces
{
    public interface IAllergyMasterService
    {
        Task<List<ViewAllergyMaster>> View_All_AllergyMasters();
    }
}
