using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_api_dakota.Models.Organization
{
    public record  OrganizationResponseDTO
    (
        int Id,
        
        string Name, 

        string Website,

        byte[] Logo

    );
}