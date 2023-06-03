using Microsoft.EntityFrameworkCore;

namespace task6x.Models
{
    public class UserModel
    {
        public string UserName { get; set; }

        public UserModel(string userName)
        {
            UserName = userName;
        }
    }
}