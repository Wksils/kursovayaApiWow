using kursovayaApi.Models.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace kursovayaApi.Models.Services
{
    public class UsersService : AbstractionService, ICommonService<User, int>
    {
        private readonly KursovayaContext db;

        public UsersService(KursovayaContext db)
        {
            this.db = db;
        }

        public bool Create(User model)
        {
            bool result = DoAction(() =>
            {
                db.Users.Add(model);
                db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(() =>
            {
                User user = db.Users.FirstOrDefault(p => p.UserId == id)!;
                db.Users.Remove(user);
                db.SaveChanges();
            });
            return result;
        }

        public async Task<User> Get(int id)
        {
            var res = await db.Users.FirstOrDefaultAsync(p => p.UserId == id);
            return res!;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await db.Users.ToListAsync();
        }

        public bool Update(int id, User model)
        {
            bool result = DoAction(() =>
            {
                User user = db.Users.FirstOrDefault(p => p.UserId == id)!;
                user.Login = model.Login;
                user.FullName = model.FullName;
                user.PasswordHash = model.PasswordHash;
                user.Role=model.Role;
                user.Department=model.Department;
                user.IsActive=model.IsActive;
                user.CreatedAt=model.CreatedAt;
                db.Users.Update(user);
                db.SaveChanges();
            });
            return result;
        }
        public User GetUser(string login)
        {
            var user = db.Users.FirstOrDefault(p => p.Login == login);
            return user!;
        }
        public User GetUser(string login, string password)
        {
            var user = db.Users.FirstOrDefault(p => p.Login == login && p.PasswordHash == password);
            return user!;
        }
        public ClaimsIdentity GetIdentity(string username, string password)
        {
            User user = GetUser (username, password);
            if(user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType,user.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType,user.Role)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null!;
        }
    }
}
