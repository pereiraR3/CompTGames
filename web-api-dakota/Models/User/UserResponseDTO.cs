using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api_dakota.Models.User;

namespace web_api_dakota.Models.User
{
    public record UserResponseDTO
    (
        
        int Id,

        string Username,

        string Password,

        string[] Role

    );
}