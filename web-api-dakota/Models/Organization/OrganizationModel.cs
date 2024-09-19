using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_api_dakota.Models.Organization
{
    public class OrganizationModel
    {
        
        public int Id { get; set; }
        public string Name { get; set; }

        public string Website { get; set; }

        public byte[] Logo { get; set; }

        public OrganizationModel() { }
        
        public OrganizationModel(OrganizationRequestDTO request){

            this.Name = request.Name;
            
            this.Website = request.Website;

            this.Logo = request.Logo;

        }
    }
}