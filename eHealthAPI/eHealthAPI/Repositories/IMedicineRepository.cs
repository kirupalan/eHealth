using eHealthAPI.Models.Domain;

namespace eHealthAPI.Repositories
{
    public interface IMedicineRepository
    {
        // Synchronous
        // IEnumerable<Medicine> GetAll(); 

        // Asynchronous
        Task<IEnumerable<Medicine>> GetAllAsync();

        Task<Medicine>GetAsync(int Id);

        // Kiru: Insert new Medicine
        Task<Medicine> AddAsync(Medicine medicine);

        // Kiru: Delete Medicine
        Task<Medicine> DeleteAsync(int Id);

        // Kiru: Update Medicine
        Task<Medicine> UpdateAsync(int Id, Medicine medicine);
    }
}
