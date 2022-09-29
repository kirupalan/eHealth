using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using eHealthAPI.Repositories;

namespace eHealthAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UserController(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // Kiru: Get All Users
        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _repo.GetAllAsync();
            var usersDTO = _mapper.Map<List<Models.DTO.User>>(users);
            return Ok(usersDTO);
        }

        // Kiru: Get User by id
        [HttpGet]
        [Route("{Id:int}")]
        [ActionName("GetUserAsync")]
        public async Task<IActionResult> GetUserAsync(int Id)
        {
            var user = await _repo.GetAsync(Id);

            if (user == null)
            {
                return NotFound();
            }

            var usersDTO = _mapper.Map<Models.DTO.User>(user);
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
            user =  await _repo.AddAsync(user);

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
            var user = await _repo.DeleteAsync(Id);

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
            user = await _repo.UpdateAsync(Id, user);

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
