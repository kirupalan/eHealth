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
            var medicine = await neHealthDBContext.Medicines.FirstOrDefaultAsync(x => x.Id == Id);

            if (medicine == null)
            {
                return null;
            }

            //Delete Medicine
            neHealthDBContext.Medicines.Remove(medicine);
            await neHealthDBContext.SaveChangesAsync();
            return medicine;
        }

        //Asynchronous: Delete Medicine by Id
        public async Task<Medicine> UpdateAsync(int Id, Medicine medicine)
        {
            var existingMedicinee = await neHealthDBContext.Medicines.FirstOrDefaultAsync(x => x.Id == Id);

            if (existingMedicinee == null)
            {
                return null;
            }

            existingMedicinee.MedicineName = medicine.MedicineName;
            existingMedicinee.Manufacturer = medicine.Manufacturer;
            existingMedicinee.UnitPrice = medicine.UnitPrice;
            existingMedicinee.Discount = medicine.Discount;
            existingMedicinee.Quantity = medicine.Quantity;
            existingMedicinee.Disease = medicine.Disease;
            existingMedicinee.Uses = medicine.Uses;
            existingMedicinee.ExpDate = medicine.ExpDate;
            existingMedicinee.ImageUrl = medicine.ImageUrl;
            existingMedicinee.Status = medicine.Status;

            await neHealthDBContext.SaveChangesAsync();

            return existingMedicinee;
        }
    }
}
