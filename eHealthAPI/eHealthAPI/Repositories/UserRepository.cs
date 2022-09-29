using eHealthAPI.Data;
using eHealthAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace eHealthAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly eHealthDBContext _context;
        public UserRepository(eHealthDBContext context)
        {
            _context = context;
        }

        //Kiru: Authenticate
        public async Task<User> AuthenticateAsync(string email, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower() && x.Password == password);
            if (user == null)
            {
                return null;
            }
            user.Password = null;
            return user;

        }

        //Kiru: Get All Users
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        //Asynchronous: Get User by id
        public async Task<User> GetAsync(int Id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == Id);
            return user;
        }

        //Asynchronous: Add User
        public async Task<User> AddAsync(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        //Asynchronous: Delete User by Id
        public async Task<User> DeleteAsync(int Id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == Id);

            if (user == null)
            {
                return null;
            }

            //Delete User
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        //Asynchronous: Delete User by Id
        public async Task<User> UpdateAsync(int Id, User user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == Id);

            if (existingUser == null)
            {
                return null;
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Password = user.Password;
            existingUser.Email = user.Email;
            existingUser.Phone = user.Phone;
            existingUser.DOB = user.DOB;
            existingUser.Address = user.Address;
            existingUser.Fund = user.Fund;
            existingUser.Type = user.Type;
            existingUser.Status = user.Status;

            await _context.SaveChangesAsync();

            return existingUser;
        }


    }

}
