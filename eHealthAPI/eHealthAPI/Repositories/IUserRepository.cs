using eHealthAPI.Models.Domain;

namespace eHealthAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string email, string password);

        // Kiru: all users
        Task<IEnumerable<User>> GetAllAsync();

        // Kiru: single user
        Task<User> GetAsync(int Id);

        // Kiru: Insert new User
        Task<User> AddAsync(User user);

        // Kiru: Delete User
        Task<User> DeleteAsync(int Id);

        // Kiru: Update User
        Task<User> UpdateAsync(int Id, User user);
    }
}
