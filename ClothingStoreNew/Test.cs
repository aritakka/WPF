using System.Linq;

namespace ClothingStoreNew
{
    public class Test
    {
        public void Check()
        {
            using (var db = new Store123Entities())
            {
                var users = db.Users.ToList();
            }
        }
    }
}