using eHealthAPI.Models.Domain;

namespace eHealthAPI.Repositories
{
    public interface IMedicineRepository
    {
        // Kiru: Get Medicine by Id
        Task<Medicine> GetAsync(int Id);

        // Kiru: Get All Medicines
        Task<IEnumerable<Medicine>> GetAllAsync();

        // Kiru: Insert new Medicine
        Task<Medicine> AddAsync(Medicine medicine);

        // Kiru: Delete Medicine
        Task<Medicine> DeleteAsync(int Id);

        // Kiru: Update Medicine
        Task<Medicine> UpdateAsync(int Id, Medicine medicine);

        // Kiru: Update Image
        Task<bool> UpdateProfileImageAsync(int Id, string profileImageUrl);
    }
}
