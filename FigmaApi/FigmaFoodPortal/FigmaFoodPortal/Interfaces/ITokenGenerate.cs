using FoodPortal.Models;
using FoodPortal.Models.DTO;

namespace FoodPortal.Interfaces
{
    public interface ITokenGenerate
    {
        public string GenerateToken(User user);

    }
}
