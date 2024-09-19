using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_api_dakota.Models.AI
{
    public record AiResponseDTO
    (

        int Id,

        string Name,

        byte[] Logo
    );
}