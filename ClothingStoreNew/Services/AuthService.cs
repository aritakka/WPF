using System.Linq;

namespace ClothingStoreNew.Services
{
    public class AuthService
    {
        public Users Login(string email, string password)
        {
            using (var db = new Store123Entities())
            {
                return db.Users.FirstOrDefault(u =>
                    u.Email == email &&
                    u.PasswordHash == password);
            }
        }

        public bool Register(string email, string password, string userName)
        {
            using (var db = new Store123Entities())
            {
                bool exists = db.Users.Any(u => u.Email == email);

                if (exists)
                    return false;

                Users user = new Users();

                user.Email = email;
                user.PasswordHash = password;
                user.UserName = userName;
                user.RoleId = 1;

                db.Users.Add(user);

                db.SaveChanges();

                return true;
            }
        }
    }
}