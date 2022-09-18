using eHealthAPI.Models.Domain;

namespace eHealthAPI.Repositories
{
    public interface IMedicineRepository
    {
        IEnumerable<Medicine> GetAll();
    }
}
