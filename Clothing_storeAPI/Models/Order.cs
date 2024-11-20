using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Clothing_storeAPI.Models
{
    [Table("Orders")]
    public class Order
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [DisplayName("Mã Đặt Hàng")]
        public string code { get; set; }

        [DisplayName("Tên Khách Hàng")]
        public string userName { get; set; }

        public string phone { get; set; }
        public string address { get; set; }
        [DisplayName("Trạng Thái")]
        public bool active { get; set; }

        [DisplayName("Ngày Đặt Hàng")]
        public DateTime orderDate { get; set; }
        //[ForeignKey("Cart")]
        //public int cartId { get; set; }
        //public virtual Cart Cart { get; set; }

        //[JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
