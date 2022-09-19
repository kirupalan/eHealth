using eHealthAPI.Models.Domain;

namespace eHealthAPI.Repositories
{
    public interface IUserRepository
    {
        // Asynchronous
        Task<IEnumerable<User>> GetAllAsync();

        Task<User> GetAsync(int Id);

        // Kiru: Insert new User
        Task<User> AddAsync(User user);

        // Kiru: Delete User
        Task<User> DeleteAsync(int Id);

        // Kiru: Update User
        Task<User> UpdateAsync(int Id, User user);
    }
}
