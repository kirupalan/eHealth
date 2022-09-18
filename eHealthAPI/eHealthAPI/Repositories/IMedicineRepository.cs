using eHealthAPI.Models.Domain;

namespace eHealthAPI.Repositories
{
    public interface IMedicineRepository
    {
        // Synchronous
        // IEnumerable<Medicine> GetAll(); 

        // Asynchronous
        Task<IEnumerable<Medicine>> GetAllAsync();
    }
}
