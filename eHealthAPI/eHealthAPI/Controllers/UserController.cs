using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using eHealthAPI.Models.Domain;
using eHealthAPI.Repositories;

using Microsoft.AspNetCore.Mvc;
using eHealthAPI.Models.DTO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace eHealthAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserController(IUserRepository userRepository, IMapper user)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }


        // Kiru: Get All Users
        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await userRepository.GetAllAsync();
            var usersDTO = mapper.Map<List<Models.DTO.User>>(users);
            return Ok(users);
        }

        // Kiru: Get User by id
        [HttpGet]
        [Route("{Id:int}")]
        [ActionName("GetUserAsync")]
        public async Task<IActionResult> GetUserAsync(int Id)
        {
            var user = await userRepository.GetAsync(Id);

            if (user == null)
            {
                return NotFound();
            }

            var usersDTO = mapper.Map<Models.DTO.User>(user);
            return Ok(usersDTO);
        }

        // Kiru: Add User
        [HttpPost]
        public async Task<IActionResult> AddUserAsync(Models.DTO.AddUserRequest addUserRequest)
        {
            // Request(DTO) to Domain Model
            var user = new Models.Domain.User()
            {
                FirstName = addUserRequest.FirstName,
                LastName = addUserRequest.LastName,
                Password = addUserRequest.Password,
                Email = addUserRequest.Email,
                Phone = addUserRequest.Phone,
                DOB = addUserRequest.DOB,
                Address = addUserRequest.Address,
                Fund = addUserRequest.Fund,
                Type = addUserRequest.Type,
                Status = addUserRequest.Status
            };


            //Pass details to repository
            user =  await userRepository.AddAsync(user);

            //Comnvert the data back to DTO
            var userDTO = new Models.Domain.User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                Email = user.Email,
                Phone = user.Phone,
                DOB = user.DOB,
                Address = user.Address,
                Fund = user.Fund,
                Type = user.Type,
                Status = user.Status
            };

            return CreatedAtAction(nameof(GetUserAsync), new { Id = userDTO.Id}, userDTO);
        }

        // Kiru: Delete User
        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteUserAsync(int Id)
        {
            //Get User
            var user = await userRepository.DeleteAsync(Id);

            //If null NotFound
            if (user == null)
            {
                return NotFound();
            }

            //Comnvert the data back to DTO
            var userDTO = new Models.Domain.User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                Email = user.Email,
                Phone = user.Phone,
                DOB = user.DOB,
                Address = user.Address,
                Fund = user.Fund,
                Type = user.Type,
                Status = user.Status
            };

            //Return OK Response
            return Ok(userDTO);
        }

        // Kiru: Update User
        [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] int Id, [FromBody] Models.DTO.UpdateUserRequest updateUserRequest)
        {
            // Request(DTO) to Domain Model
            var user = new Models.Domain.User()
            {
                FirstName = updateUserRequest.FirstName,
                LastName = updateUserRequest.LastName,
                Password = updateUserRequest.Password,
                Email = updateUserRequest.Email,
                Phone = updateUserRequest.Phone,
                DOB = updateUserRequest.DOB,
                Address = updateUserRequest.Address,
                Fund = updateUserRequest.Fund,
                Type = updateUserRequest.Type,
                Status = updateUserRequest.Status
            };

            //Pass details to repository
            user = await userRepository.UpdateAsync(Id, user);

            //If null NotFound
            if (user == null)
            {
                return NotFound();
            }

            //Comnvert the data back to DTO
            var userDTO = new Models.Domain.User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                Email = user.Email,
                Phone = user.Phone,
                DOB = user.DOB,
                Address = user.Address,
                Fund = user.Fund,
                Type = user.Type,
                Status = user.Status
            };


            //Return OK Response
            return Ok(userDTO);

        }

    }
}
