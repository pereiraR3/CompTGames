using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace web_api_dakota.Models.AI
{
    public class AiModel
    {
        public string Name { get; set; }

        public byte[] Logo { get; set; }

        public AiModel(){}
        
        public AiModel(AiRequestDTO request) {
            
            this.Name = request.Name;
            this.Logo = request.Logo;

        }

    }
}