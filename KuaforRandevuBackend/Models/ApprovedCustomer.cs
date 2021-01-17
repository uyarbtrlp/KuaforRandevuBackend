using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KuaforRandevuBackend.Models
{

    public class ApprovedCustomer
    {
        [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public String Id { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public DateTime Date { get; set; }
        public String Hour { get; set; }
        public String Transactions { get; set; }
        public String Price { get; set; }
        public String PaymentChoice { get; set; }
        public String UserId { get; set; }
        public AppUser User { get; set; }
    }
}
