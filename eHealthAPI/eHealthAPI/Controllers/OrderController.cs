using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using eHealthAPI.Repositories;

namespace eHealthAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _repo;
        private readonly IMapper _mapper;

        //public OrderController(IOrderRepository orderRepository)
        public OrderController(IOrderRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        // Kiru: Get All Orders
        [HttpGet]
        //public IActionResult GetAllOrders()
        public async Task<IActionResult> GetAllOrdersAsync()
        {
            var orders = await _repo.GetAllAsync();
            var ordersDTO = _mapper.Map<List<Models.DTO.Order>>(orders);
            return Ok(ordersDTO);
        }

        // Kiru: Get Order by id
        [HttpGet]
        [Route("{Id:int}")]
        [ActionName("GetOrderAsync")]
        public async Task<IActionResult> GetOrderAsync(int Id)
        {
            var order = await _repo.GetAsync(Id);

            if (order == null)
            {
                return NotFound();
            }

            var ordersDTO = _mapper.Map<Models.DTO.Order>(order);
            return Ok(ordersDTO);
        }

        // Kiru: Add Order
        [HttpPost]
        public async Task<IActionResult> AddOrderAsync(Models.DTO.AddOrderRequest addOrderRequest)
        {
            // Request(DTO) to Domain Model
            var order = new Models.Domain.Order()
            {
                UserId = addOrderRequest.UserId,
                OrderNumber = addOrderRequest.OrderNumber,
                OrderTotal = addOrderRequest.OrderTotal,
                OrderStatus = addOrderRequest.OrderStatus
            };


            //Pass details to repository
            order = await _repo.AddAsync(order);

            //Comnvert the data back to DTO
            var orderDTO = new Models.Domain.Order
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderNumber = order.OrderNumber,
                OrderTotal = order.OrderTotal,
                OrderStatus = order.OrderStatus
            };

            return CreatedAtAction(nameof(GetOrderAsync), new { Id = orderDTO.Id }, orderDTO);
        }

        // Kiru: Delete Order
        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteOrderAsync(int Id)
        {
            //Get Order
            var order = await _repo.DeleteAsync(Id);

            //If null NotFound
            if (order == null)
            {
                return NotFound();
            }

            //Comnvert the data back to DTO
            var orderDTO = new Models.Domain.Order
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderNumber = order.OrderNumber,
                OrderTotal = order.OrderTotal,
                OrderStatus = order.OrderStatus
            };

            //Return OK Response
            return Ok(orderDTO);
        }

        // Kiru: Update Order
        [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateOrderAsync([FromRoute] int Id, [FromBody] Models.DTO.UpdateOrderRequest updateOrderRequest)
        {
            // Request(DTO) to Domain Model
            var order = new Models.Domain.Order()
            {
                UserId = updateOrderRequest.UserId,
                OrderNumber = updateOrderRequest.OrderNumber,
                OrderTotal = updateOrderRequest.OrderTotal,
                OrderStatus = updateOrderRequest.OrderStatus
            };

            //Pass details to repository
            order = await _repo.UpdateAsync(Id, order);

            //If null NotFound
            if (order == null)
            {
                return NotFound();
            }

            //Comnvert the data back to DTO
            var orderDTO = new Models.Domain.Order
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderNumber = order.OrderNumber,
                OrderTotal = order.OrderTotal,
                OrderStatus = order.OrderStatus
            };


            //Return OK Response
            return Ok(orderDTO);

        }

    }
}
