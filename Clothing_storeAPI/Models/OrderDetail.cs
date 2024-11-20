using Clothing_storeAPI.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Clothing_storeAPI.Models
{
    [Table("OrderDetails")]
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int id { get; set; }

        public float quantity { get; set; }

        public float price { get; set; }


        [ForeignKey("Order")]
        public int orderId { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; } // Navigation property back to Order

        [ForeignKey("Product")]
        public int productId { get; set; }
        public virtual Product Product { get; set; } // Navigation property to Product
    }
}
