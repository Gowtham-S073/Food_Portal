using FoodPortal.Models;

namespace FoodPortal.Interfaces
{
    public interface ICrud<T, K>
    {
        Task<T> Add(T item);
        Task<T> Update(T item);
        Task<T> Delete(K item);
        Task<T> GetValue(K item);
        Task<List<T>> GetAll();
    }
}
