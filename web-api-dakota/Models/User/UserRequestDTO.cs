using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_api_dakota.Models.User
{
    public record UserRequestDTO
    (
        string Username,
        
        string Password,

        string[] Role

    );
}