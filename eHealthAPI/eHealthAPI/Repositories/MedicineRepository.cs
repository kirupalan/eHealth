using eHealthAPI.Data;
using eHealthAPI.Models.Domain;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace eHealthAPI.Repositories
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly eHealthDBContext neHealthDBContext;
        public MedicineRepository(eHealthDBContext eHealthDBContext)
        {
            this.neHealthDBContext = eHealthDBContext;
        }

        //Asynchronous
        public async Task<IEnumerable<Medicine>> GetAllAsync()
        {
            return await neHealthDBContext.Medicines.ToListAsync();
        }

        //Synchronous
        /*
        public IEnumerable<Medicine> GetAll()
        {
            return neHealthDBContext.Medicines.ToList();
        }
        */
    }
}
