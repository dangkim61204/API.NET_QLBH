using BTL_ASP.NetCore.Areas.Admin.Models.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clothing_storeAPI.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int id { get; set; }

        [DisplayName("Mã Sản Phẩm")]
        [Required(ErrorMessage = "Nhập mã sản phẩm"), StringLength(10)]
        public string code { get; set; }

        [DisplayName("Tên Sản Phẩm")]
        [Required(ErrorMessage = "Hãy nhập tên loại sách"), StringLength(100)]
        public string productName { get; set; }

        [DisplayName("Ảnh Sản Phẩm")]

        public string image { get; set; }

        [DisplayName("Giá Sản Phẩm")]
        [Required(ErrorMessage = "Hãy nhập giá sản phẩm")]
        public float price { get; set; }
     
        public string desciption { get; set; }

        [DisplayName("Danh mục sản phẩm")]
        [ForeignKey("Category")]
        public int categoryId { get; set; }

        //lien ket nhieu-mot vơi category
        public virtual Category Category { get; set; }

        //public virtual CartItem CartItem { get; set; }

        // Navigation property for related OrderDetails
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }



    }
}
