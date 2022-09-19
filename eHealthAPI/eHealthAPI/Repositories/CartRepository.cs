using eHealthAPI.Data;
using eHealthAPI.Models.Domain;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace eHealthAPI.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly eHealthDBContext neHealthDBContext;
        public CartRepository(eHealthDBContext eHealthDBContext)
        {
            this.neHealthDBContext = eHealthDBContext;
        }

        //Asynchronous: Get All Carts
        public async Task<IEnumerable<Cart>> GetAllAsync()
        {
            return await neHealthDBContext.Carts.ToListAsync();
        }

        //Asynchronous: Get Cart by id
        public async Task<Cart> GetAsync(int Id)
        {
            var cart = await neHealthDBContext.Carts.FirstOrDefaultAsync(x => x.Id == Id);
            return cart;
        }

        //Asynchronous: Add Cart
        public async Task<Cart> AddAsync(Cart cart)
        {
            await neHealthDBContext.AddAsync(cart);
            await neHealthDBContext.SaveChangesAsync();
            return cart;
        }

        //Asynchronous: Delete Cart by Id
        public async Task<Cart> DeleteAsync(int Id)
        {
            var medicne = await neHealthDBContext.Carts.FirstOrDefaultAsync(x => x.Id == Id);

            if (medicne == null)
            {
                return null;
            }

            //Delete Cart
            neHealthDBContext.Carts.Remove(medicne);
            await neHealthDBContext.SaveChangesAsync();
            return medicne;
        }

        //Asynchronous: Delete Cart by Id
        public async Task<Cart> UpdateAsync(int Id, Cart cart)
        {
            var existingMedicne = await neHealthDBContext.Carts.FirstOrDefaultAsync(x => x.Id == Id);

            if (existingMedicne == null)
            {
                return null;
            }

            existingMedicne.UserId = cart.UserId;
            existingMedicne.MedicineName = cart.MedicineName;
            existingMedicne.UnitPrice = cart.UnitPrice;
            existingMedicne.Discount = cart.Discount;
            existingMedicne.Quantity = cart.Quantity;
            existingMedicne.TotalPrice = cart.TotalPrice;

            await neHealthDBContext.SaveChangesAsync();

            return existingMedicne;
        }
    }
}
