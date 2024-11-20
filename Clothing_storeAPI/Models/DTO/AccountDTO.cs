using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Clothing_storeAPI.Models.DTO
{
    public class AccountDTO
    {
        public string? accountId { get; set; }
        public string userName { get; set; }

        public string password { get; set; }
        //public string phone {  get; set; }
        public string fullName { get; set; }

        public string email { get; set; }
        //public bool gender { get; set; }
        public string address { get; set; }

        public string? role { get; set; }
    }
}
