using FoodPortal.Models.DTO;

namespace FoodPortal.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> Register(UserRegisterDTO userRegisterDTO);
        Task<UserDTO> LogIN(UserDTO userDTO);
    }
}
