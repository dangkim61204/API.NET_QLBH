using Clothing_storeAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace BTL_ASP.NetCore.Areas.Admin.Models.ViewModels
{
    public class CartItem
    {
        public List<Cart> Carts { get; set; }
        public float GrandTotal { get; set; }
    }
}
