
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace Clothing_storeAPI.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public int quantity { get; set; }
        public float price { get; set; }
        public float totalPrice => quantity * price;
        //public List<Order> Orders { get; set; }
        public Cart() { }

        public Cart(Product product)
        {
            id = product.id;
            name = product.productName;
            price = product.price;
            quantity = 1;
            image = product.image;


        }
    }
}
