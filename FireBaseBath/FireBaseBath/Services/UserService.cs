using Firebase.Database;
using Firebase.Database.Query;
using FireBaseBath.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBaseBath.Services
{
    public class UserService
    {
        FirebaseClient client;

        public UserService()
        {
            client = new FirebaseClient("https://liffeybath.firebaseio.com/");
        }

        public async Task<bool> IsUserExist(string name)
        {
            var user = (await client.Child("Users")
                .OnceAsync<User>())
                .Where(u => u.Object.UserName == name)
                .FirstOrDefault();
            return (user != null);
        }

        public async Task<bool> RegisterUser(string name,string passwd)
        {
            if(await IsUserExist(name) == false)
            {
                await client.Child("Users")
                    .PostAsync(new User()
                    {
                    UserName = name,
                    Password = passwd
                });
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> LoginUser(string name, string passwd)
        {
            var user = (await client.Child("Users")
                .OnceAsync<User>())
                .Where(u => u.Object.UserName == name)
                .Where(u => u.Object.Password == passwd)
                .FirstOrDefault();
            return (user != null);
        }
    }
}
