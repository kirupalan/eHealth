using Microsoft.AspNetCore.Mvc;
using eHealthAPI.Data;
using Microsoft.EntityFrameworkCore;
using eHealthAPI.Helper;
using eHealthAPI.Repositories;

namespace eHealthAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _repo;
        private readonly ITokenHandler _token;
        public AuthController(IUserRepository repo, ITokenHandler token)
        {
            _repo = repo;
            _token = token;
        }

        //Kiru: LoginAsync
        //[HttpPost]
        //[Route("[login]")]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(Models.DTO.LoginRequest loginRequest)
        {
            //Validate the incoming request
            //Check username and password
            //Check if the user is authenticated

            //System.Diagnostics.Debug.WriteLine(loginRequest.Email);
           // System.Diagnostics.Debug.WriteLine(loginRequest.Password);


            var user =  await _repo.AuthenticateAsync(loginRequest.Email, loginRequest.Password);

            if (user != null)
            {
                //Generate a JWT Token
                var token = await _token.CreateTokenAsync(user);
                return Ok(token);
            }
            return BadRequest("Username or Password is incorrect.");
        }




        /*
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] eHealthAPI.Models.Domain.Auth userObj)
        {
            if(userObj == null)
            {
                return BadRequest();
            }

            var user = await _repo.GetAsync(x => x.Email == userObj.Email & x.Password == userObj.Password);

            if (user == null)
            {
                return NotFound(new {Message = "User Not Found or wrong password"});
            }

            return Ok(new { Message = "Login Successful" });
        }
        */
        

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] eHealthAPI.Models.Domain.User userObj)
        {
            if (userObj == null)
            {
                return BadRequest();
            }

            //userObj.Password = PasswordHasher.HashPassword(userObj.Password);
            userObj.Status = "Active";

            await _repo.AddAsync(userObj);

            return Ok(new { Message = "User Registered" });

        }
    }
}
