using FoodPortal.Models.DTO;

namespace FoodPortal.Interfaces
{
    public interface IAdditionalOrderDetailService
    {
        Task Setvalue();
        Task<List<Addons>> Getaddons(string id);
        Task<List<AdditionalProductDTO>> GetAdditionalProduct(string id);
    }
}
