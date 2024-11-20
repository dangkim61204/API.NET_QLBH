using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using BTL_ASP.NetCore.Areas.Admin.Models.ViewModels;

namespace Clothing_storeAPI.Models.DTO
{
    public class OrderDTO
    {   
        public string? code { get; set; }
    
        public string userName { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public List<paymentDTO> cartItems { get; set; }  // Danh sách các item trong giỏ hàng


        public bool active { get; set; }

        public DateTime orderDate { get; set; }

    }
}
