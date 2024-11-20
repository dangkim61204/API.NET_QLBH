using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clothing_storeAPI.Models
{
    [Table("Accounts")]
    public class Account
    {
        //public Account()
        //{
        //    Orders = new List<Order>();
        //}


        [Key]
        [DisplayName("Mã TK"), StringLength(10), Column(TypeName = "varchar")]
        public string? accountId { get; set; }

        [DisplayName("Tên Đăng Nhập")]
        
        public string userName { get; set; }

        [DisplayName("Mật Khẩu")]
        public string password { get; set; }

        [DisplayName("Tên Hiển Thị")]
        public string fullName { get; set; }

        [DisplayName("Giới Tính")]
        public bool? gender { get; set; }
   
        [DisplayName("Email")]
        public string email { get; set; }

        [DisplayName("Điện Thoại")]
        public string? phone { get; set; }

        [DisplayName("Ngày Sinh")]
        public DateTime? birthday { get; set; }

        [DisplayName("Địa chỉ")]
        public string address { get; set; }

        public string role { get; set; }

        // lien ket mot-nhieu với Order
        //public virtual ICollection<Cart> Carts { get; set; }
    }
}
