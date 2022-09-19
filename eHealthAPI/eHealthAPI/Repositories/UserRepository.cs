using eHealthAPI.Data;
using eHealthAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace eHealthAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly eHealthDBContext neHealthDBContext;
        public UserRepository(eHealthDBContext eHealthDBContext)
        {
            this.neHealthDBContext = eHealthDBContext;
        }

        //Asynchronous: Get All Users
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await neHealthDBContext.Users.ToListAsync();
        }

        //Asynchronous: Get User by id
        public async Task<User> GetAsync(int Id)
        {
            var user = await neHealthDBContext.Users.FirstOrDefaultAsync(x => x.Id == Id);
            return user;
        }

        //Asynchronous: Add User
        public async Task<User> AddAsync(User user)
        {
            await neHealthDBContext.AddAsync(user);
            await neHealthDBContext.SaveChangesAsync();
            return user;
        }

        //Asynchronous: Delete User by Id
        public async Task<User> DeleteAsync(int Id)
        {
            var user = await neHealthDBContext.Users.FirstOrDefaultAsync(x => x.Id == Id);

            if (user == null)
            {
                return null;
            }

            //Delete User
            neHealthDBContext.Users.Remove(user);
            await neHealthDBContext.SaveChangesAsync();
            return user;
        }

        //Asynchronous: Delete User by Id
        public async Task<User> UpdateAsync(int Id, User user)
        {
            var existingUser = await neHealthDBContext.Users.FirstOrDefaultAsync(x => x.Id == Id);

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

            await neHealthDBContext.SaveChangesAsync();

            return existingUser;
        }
    }
}
