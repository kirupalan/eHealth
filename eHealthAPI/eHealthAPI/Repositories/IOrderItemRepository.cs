using eHealthAPI.Models.Domain;

namespace eHealthAPI.Repositories
{
    public interface IOrderItemRepository
    {
         // Asynchronous
        Task<IEnumerable<OrderItem>> GetAllAsync();

        Task<OrderItem> GetAsync(int Id);

        // Kiru: Insert new OrderItem
        Task<OrderItem> AddAsync(OrderItem orderitem);

        // Kiru: Delete OrderItem
        Task<OrderItem> DeleteAsync(int Id);

        // Kiru: Update OrderItem
        Task<OrderItem> UpdateAsync(int Id, OrderItem orderitem);
    }
}
