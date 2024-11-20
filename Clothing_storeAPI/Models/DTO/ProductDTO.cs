using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Clothing_storeAPI.Models.DTO
{
    public class ProductDTO
    {
        public int id { get; set; }
        public string code { get; set; }
        public string productName { get; set; }
        
        public IFormFile? image { get; set; }

        public float price { get; set; }

        public string desciption { get; set; }

        public int categoryId { get; set; }
        
    }
}
