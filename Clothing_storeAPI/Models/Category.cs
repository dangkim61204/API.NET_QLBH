using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Clothing_storeAPI.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int categoryId { get; set; }

        [Column(TypeName ="nvarchar(100)")]
        public string categoryName { get; set; }
        public bool status { get; set; }

        // lien ket mot-nhieu với product
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
