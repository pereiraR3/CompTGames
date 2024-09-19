using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_api_dakota.Models.Category
{
    public record CategoryResponseDTO
    (
        int id,

        String Name,

        String Description

    );

}