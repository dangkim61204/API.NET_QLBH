using System.ComponentModel.DataAnnotations.Schema;

namespace Clothing_storeAPI.Models.DTO
{
    public class CartItemDTO
    {
        public int id { get; set; }
        public int productId { get; set; }
        public string? AccountId { get; set; }
        public int quantity { get; set; }

    }
}
