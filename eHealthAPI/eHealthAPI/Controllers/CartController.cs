using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using eHealthAPI.Models.Domain;
using eHealthAPI.Repositories;
using eHealthAPI.Models.DTO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace eHealthAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : Controller
    {
        private readonly ICartRepository cartRepository;
        private readonly IMapper mapper;

        public CartController(ICartRepository cartRepository, IMapper cart)
        {
            this.cartRepository = cartRepository;
            this.mapper = mapper;
        }


        // Kiru: Get All Carts
        [HttpGet]
        public async Task<IActionResult> GetAllCartsAsync()
        {
            var carts = await cartRepository.GetAllAsync();
            var cartsDTO = mapper.Map<List<Models.DTO.Cart>>(carts);
            return Ok(carts);
        }

        // Kiru: Get Cart by id
        [HttpGet]
        [Route("{Id:int}")]
        [ActionName("GetCartAsync")]
        public async Task<IActionResult> GetCartAsync(int Id)
        {
            var cart = await cartRepository.GetAsync(Id);

            if (cart == null)
            {
                return NotFound();
            }

            var cartsDTO = mapper.Map<Models.DTO.Cart>(cart);
            return Ok(cartsDTO);
        }

        // Kiru: Add Cart
        [HttpPost]
        public async Task<IActionResult> AddCartAsync(Models.DTO.AddCartRequest addCartRequest)
        {
            // Request(DTO) to Domain Model
            var cart = new Models.Domain.Cart()
            {
                UserId = addCartRequest.UserId,
                MedicineName = addCartRequest.MedicineName,
                UnitPrice = addCartRequest.UnitPrice,
                Discount = addCartRequest.Discount,
                Quantity = addCartRequest.Quantity,
                TotalPrice = addCartRequest.TotalPrice
            };


            //Pass details to repository
            cart = await cartRepository.AddAsync(cart);

            //Comnvert the data back to DTO
            var cartDTO = new Models.Domain.Cart
            {
                Id = cart.Id,
                UserId = cart.UserId,
                MedicineName = cart.MedicineName,
                UnitPrice = cart.UnitPrice,
                Discount = cart.Discount,
                Quantity = cart.Quantity,
                TotalPrice = cart.TotalPrice
            };

            return CreatedAtAction(nameof(GetCartAsync), new { Id = cartDTO.Id }, cartDTO);
        }

        // Kiru: Delete Cart
        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteCartAsync(int Id)
        {
            //Get Cart
            var cart = await cartRepository.DeleteAsync(Id);

            //If null NotFound
            if (cart == null)
            {
                return NotFound();
            }

            //Comnvert the data back to DTO
            var cartDTO = new Models.Domain.Cart
            {
                Id = cart.Id,
                UserId = cart.UserId,
                MedicineName = cart.MedicineName,
                UnitPrice = cart.UnitPrice,
                Discount = cart.Discount,
                Quantity = cart.Quantity,
                TotalPrice = cart.TotalPrice
            };

            //Return OK Response
            return Ok(cartDTO);
        }

        // Kiru: Update Cart
        [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateCartAsync([FromRoute] int Id, [FromBody] Models.DTO.UpdateCartRequest updateCartRequest)
        {
            // Request(DTO) to Domain Model
            var cart = new Models.Domain.Cart()
            {
                UserId = updateCartRequest.UserId,
                MedicineName = updateCartRequest.MedicineName,
                UnitPrice = updateCartRequest.UnitPrice,
                Discount = updateCartRequest.Discount,
                Quantity = updateCartRequest.Quantity,
                TotalPrice = updateCartRequest.TotalPrice
            };

            //Pass details to repository
            cart = await cartRepository.UpdateAsync(Id, cart);

            //If null NotFound
            if (cart == null)
            {
                return NotFound();
            }

            //Comnvert the data back to DTO
            var cartDTO = new Models.Domain.Cart
            {
                Id = cart.Id,
                UserId = cart.UserId,
                MedicineName = cart.MedicineName,
                UnitPrice = cart.UnitPrice,
                Discount = cart.Discount,
                Quantity = cart.Quantity,
                TotalPrice = cart.TotalPrice
            };


            //Return OK Response
            return Ok(cartDTO);

        }

    }
}