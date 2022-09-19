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

        //Synchronous: Get All Medicines
        /*
        public IEnumerable<Medicine> GetAll()
        {
            return neHealthDBContext.Medicines.ToList();
        }
        */

        //Asynchronous: Get All Medicines
        public async Task<IEnumerable<Medicine>> GetAllAsync()
        {
            return await neHealthDBContext.Medicines.ToListAsync();
        }

        //Asynchronous: Get Medicine by id
        public async Task<Medicine> GetAsync(int Id)
        {
            var medicine = await neHealthDBContext.Medicines.FirstOrDefaultAsync(x => x.Id == Id);
            return medicine;
        }

        //Asynchronous: Add Medicine
        public async Task<Medicine> AddAsync(Medicine medicine)
        {
            await neHealthDBContext.AddAsync(medicine);
            await neHealthDBContext.SaveChangesAsync();
            return medicine;
        }

        //Asynchronous: Delete Medicine by Id
        public async Task<Medicine> DeleteAsync(int Id)
        {
            var medicne = await neHealthDBContext.Medicines.FirstOrDefaultAsync(x => x.Id == Id);

            if (medicne == null)
            {
                return null;
            }

            //Delete Medicine
            neHealthDBContext.Medicines.Remove(medicne);
            await neHealthDBContext.SaveChangesAsync();
            return medicne;
        }

        //Asynchronous: Delete Medicine by Id
        public async Task<Medicine> UpdateAsync(int Id, Medicine medicine)
        {
            var existingMedicne = await neHealthDBContext.Medicines.FirstOrDefaultAsync(x => x.Id == Id);

            if (existingMedicne == null)
            {
                return null;
            }

            existingMedicne.MedicineName = medicine.MedicineName;
            existingMedicne.Manufacturer = medicine.Manufacturer;
            existingMedicne.UnitPrice = medicine.UnitPrice;
            existingMedicne.Discount = medicine.Discount;
            existingMedicne.Quantity = medicine.Quantity;
            existingMedicne.Disease = medicine.Disease;
            existingMedicne.Uses = medicine.Uses;
            existingMedicne.ExpDate = medicine.ExpDate;
            existingMedicne.ImageUrl = medicine.ImageUrl;
            existingMedicne.Status = medicine.Status;

            await neHealthDBContext.SaveChangesAsync();

            return existingMedicne;
        }
    }
}
