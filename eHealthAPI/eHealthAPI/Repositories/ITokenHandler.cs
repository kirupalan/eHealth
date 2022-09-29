using eHealthAPI.Models.Domain;

namespace eHealthAPI.Repositories
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}
