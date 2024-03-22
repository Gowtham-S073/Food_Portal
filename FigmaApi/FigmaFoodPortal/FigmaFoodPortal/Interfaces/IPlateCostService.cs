using FoodPortal.Models;
using FoodPortal.Models.DTO;

namespace FoodPortal.Interfaces
{
    public interface IPlateCostService
    {
        Task Setvalue();
        Task<int> calculateCount(string id);
        bool GetIsveg(string id);
        T CalculatePrice<T>(string id, Func<PlateSize, T> priceSelector);
        PlatesizeDTO GetPlatecostWithFoodType(int count, string id);
        string GetPlatesize(string id);
        int GetGuestCount(string id);
    }
}
