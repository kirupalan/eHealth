using eHealthAPI.Models.Domain;

namespace eHealthAPI.Repositories
{
    public interface IOrderRepository
    {
        // Synchronous
        // IEnumerable<Medicine> GetAll(); 

        // Asynchronous
        Task<IEnumerable<Order>> GetAllAsync();

        Task<Order> GetAsync(int Id);

        // Kiru: Insert new Medicine
        Task<Order> AddAsync(Order order);

        // Kiru: Delete Medicine
        Task<Order> DeleteAsync(int Id);

        // Kiru: Update Medicine
        Task<Order> UpdateAsync(int Id, Order order);
    }
}
