using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api_dakota.Models.User;

namespace web_api_dakota.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole[] Role { get; set; }

        public UserModel() { }
        
        public UserModel(UserRequestDTO request)
        {
            
            this.Username = request.Username;

            this.Password = request.Password;

            this.Role = new UserRole[0];

        }

    }
}