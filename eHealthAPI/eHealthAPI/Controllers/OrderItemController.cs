using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using eHealthAPI.Repositories;

namespace eHealthAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderItemController : Controller
    {
        private readonly IOrderItemRepository _repo;
        private readonly IMapper _mapper;

        public OrderItemController(IOrderItemRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // Kiru: Get All OrderItems
        [HttpGet]
        public async Task<IActionResult> GetAllOrderItemsAsync()
        {
            var orderitems = await _repo.GetAllAsync();
            var orderitemsDTO = _mapper.Map<List<Models.DTO.OrderItem>>(orderitems);
            return Ok(orderitemsDTO);
        }

        // Kiru: Get OrderItem by id
        [HttpGet]
        [Route("{Id:int}")]
        [ActionName("GetOrderItemAsync")]
        public async Task<IActionResult> GetOrderItemAsync(int Id)
        {
            var orderitem = await _repo.GetAsync(Id);

            if (orderitem == null)
            {
                return NotFound();
            }

            var orderitemsDTO = _mapper.Map<Models.DTO.OrderItem>(orderitem);
            return Ok(orderitemsDTO);
        }

        // Kiru: Add OrderItem
        [HttpPost]
        public async Task<IActionResult> AddOrderItemAsync(Models.DTO.AddOrderItemRequest addOrderItemRequest)
        {
            // Request(DTO) to Domain Model
            var orderitem = new Models.Domain.OrderItem()
            {
                OrderId = addOrderItemRequest.OrderId,
                MedicineName = addOrderItemRequest.MedicineName,
                UnitPrice = addOrderItemRequest.UnitPrice,
                Discount = addOrderItemRequest.Discount,
                Quantity = addOrderItemRequest.Quantity,
                TotalPrice = addOrderItemRequest.TotalPrice
            };


            //Pass details to repository
            orderitem = await _repo.AddAsync(orderitem);

            //Comnvert the data back to DTO
            var orderitemDTO = new Models.Domain.OrderItem
            {
                Id = orderitem.Id,
                OrderId = orderitem.OrderId,
                MedicineName = orderitem.MedicineName,
                UnitPrice = orderitem.UnitPrice,
                Discount = orderitem.Discount,
                Quantity = orderitem.Quantity,
                TotalPrice = orderitem.TotalPrice
            };

            return CreatedAtAction(nameof(GetOrderItemAsync), new { Id = orderitemDTO.Id }, orderitemDTO);
        }

        // Kiru: Delete OrderItem
        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteOrderItemAsync(int Id)
        {
            //Get OrderItem
            var orderitem = await _repo.DeleteAsync(Id);

            //If null NotFound
            if (orderitem == null)
            {
                return NotFound();
            }

            //Comnvert the data back to DTO
            var orderitemDTO = new Models.Domain.OrderItem
            {
                Id = orderitem.Id,
                OrderId = orderitem.OrderId,
                MedicineName = orderitem.MedicineName,
                UnitPrice = orderitem.UnitPrice,
                Discount = orderitem.Discount,
                Quantity = orderitem.Quantity,
                TotalPrice = orderitem.TotalPrice
            };

            //Return OK Response
            return Ok(orderitemDTO);
        }

        // Kiru: Update OrderItem
        [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateOrderItemAsync([FromRoute] int Id, [FromBody] Models.DTO.UpdateOrderItemRequest updateOrderItemRequest)
        {
            // Request(DTO) to Domain Model
            var orderitem = new Models.Domain.OrderItem()
            {
                OrderId = updateOrderItemRequest.OrderId,
                MedicineName = updateOrderItemRequest.MedicineName,
                UnitPrice = updateOrderItemRequest.UnitPrice,
                Discount = updateOrderItemRequest.Discount,
                Quantity = updateOrderItemRequest.Quantity,
                TotalPrice = updateOrderItemRequest.TotalPrice
            };

            //Pass details to repository
            orderitem = await _repo.UpdateAsync(Id, orderitem);

            //If null NotFound
            if (orderitem == null)
            {
                return NotFound();
            }

            //Comnvert the data back to DTO
            var orderitemDTO = new Models.Domain.OrderItem
            {
                Id = orderitem.Id,
                OrderId = orderitem.OrderId,
                MedicineName = orderitem.MedicineName,
                UnitPrice = orderitem.UnitPrice,
                Discount = orderitem.Discount,
                Quantity = orderitem.Quantity,
                TotalPrice = orderitem.TotalPrice
            };

            //Return OK Response
            return Ok(orderitemDTO);
        }

    }
}
