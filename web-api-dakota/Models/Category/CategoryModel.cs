using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_api_dakota.Models.Category
{
    public class CategoryModel
    {
        
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    
        public CategoryModel(){}
        
        public CategoryModel(CategoryRequestDTO category){

            this.Name = category.Name;

            this.Description = category.Description;

        }

    }
}