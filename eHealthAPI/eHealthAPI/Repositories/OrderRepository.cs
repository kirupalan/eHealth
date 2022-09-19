using eHealthAPI.Data;
using eHealthAPI.Models.Domain;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace eHealthAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly eHealthDBContext neHealthDBContext;
        public OrderRepository(eHealthDBContext eHealthDBContext)
        {
            this.neHealthDBContext = eHealthDBContext;
        }

        //Asynchronous: Get All Orders
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await neHealthDBContext.Orders.ToListAsync();
        }

        //Asynchronous: Get Order by id
        public async Task<Order> GetAsync(int Id)
        {
            var order = await neHealthDBContext.Orders.FirstOrDefaultAsync(x => x.Id == Id);
            return order;
        }

        //Asynchronous: Add Order
        public async Task<Order> AddAsync(Order order)
        {
            await neHealthDBContext.AddAsync(order);
            await neHealthDBContext.SaveChangesAsync();
            return order;
        }

        //Asynchronous: Delete Order by Id
        public async Task<Order> DeleteAsync(int Id)
        {
            var order = await neHealthDBContext.Orders.FirstOrDefaultAsync(x => x.Id == Id);

            if (order == null)
            {
                return null;
            }

            //Delete Order
            neHealthDBContext.Orders.Remove(order);
            await neHealthDBContext.SaveChangesAsync();
            return order;
        }

        //Asynchronous: Delete Order by Id
        public async Task<Order> UpdateAsync(int Id, Order order)
        {
            var existingOrder = await neHealthDBContext.Orders.FirstOrDefaultAsync(x => x.Id == Id);

            if (existingOrder == null)
            {
                return null;
            }

            existingOrder.UserId = order.UserId;
            existingOrder.OrderNumber = order.OrderNumber;
            existingOrder.OrderTotal = order.OrderTotal;
            existingOrder.OrderStatus = order.OrderStatus;

            await neHealthDBContext.SaveChangesAsync();

            return existingOrder;
        }
    }
}
