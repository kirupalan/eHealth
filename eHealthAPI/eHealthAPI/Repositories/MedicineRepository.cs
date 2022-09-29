using eHealthAPI.Data;
using eHealthAPI.Models.Domain;
//using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace eHealthAPI.Repositories
{
    public class MedicineRepository : IMedicineRepository
    {
        //DB Context
        private readonly eHealthDBContext _context;
        public MedicineRepository(eHealthDBContext context)
        {
            _context = context;
        }

        //Asynchronous: Get Medicine by id
        public async Task<Medicine> GetAsync(int Id)
        {
            return await _context.Medicines.FindAsync(Id);
        }

        //Asynchronous: Get All Medicines
        public async Task<IEnumerable<Medicine>> GetAllAsync()
        {
            return await _context.Medicines.ToListAsync();
        }

        //Asynchronous: Add Medicine
        public async Task<Medicine> AddAsync(Medicine medicine)
        {
            await _context.AddAsync(medicine);
            await _context.SaveChangesAsync();
            return medicine;
        }

        //Asynchronous: Delete Medicine by Id
        public async Task<Medicine> DeleteAsync(int Id)
        {
            var medicine = await _context.Medicines.FirstOrDefaultAsync(x => x.Id == Id);

            if (medicine == null)
            {
                return null;
            }

            //Delete Medicine
            _context.Medicines.Remove(medicine);
            await _context.SaveChangesAsync();
            return medicine;
        }

        //Asynchronous: Delete Medicine by Id
        public async Task<Medicine> UpdateAsync(int Id, Medicine medicine)
        {
            var existingMedicinee = await _context.Medicines.FirstOrDefaultAsync(x => x.Id == Id);

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

            await _context.SaveChangesAsync();

            return existingMedicinee;
        }

        //Kiru: Upload Image
        public async Task<bool> UpdateProfileImageAsync(int Id, string profileImageUrl)
        {
            var medicine = await GetAsync(Id);

            if(medicine != null)
            {
                medicine.ImageUrl = profileImageUrl;
                await _context.SaveChangesAsync();
                return true;

            }
            return false;
        }
    }
}
