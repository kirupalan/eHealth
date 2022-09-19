using eHealthAPI.Models.Domain;

namespace eHealthAPI.Repositories
{
    public interface ICartRepository
    {
        // Asynchronous
        Task<IEnumerable<Cart>> GetAllAsync();

        Task<Cart> GetAsync(int Id);

        // Kiru: Insert new Medicine
        Task<Cart> AddAsync(Cart cart);

        // Kiru: Delete Medicine
        Task<Cart> DeleteAsync(int Id);

        // Kiru: Update Medicine
        Task<Cart> UpdateAsync(int Id, Cart cart);
    }
}
