using FoodPortal.Models;
using FoodPortal.Models.DTO;
using FoodPortal.ViewModel;

namespace FoodPortal.Interfaces
{
    public interface IDrinksMasterService
    {
       
        Task<List<ViewDrinksMaster>> View_All_DrinksMasters();

    }
}
