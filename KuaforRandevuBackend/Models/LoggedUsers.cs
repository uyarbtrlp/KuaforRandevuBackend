using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KuaforRandevuBackend.Models
{
    public class LoggedUsers
    {
        public String Id { get; set; }
        public String Username{ get; set; }
        public String Name{ get; set; }
        public String Surname{ get; set; }
        public String Email{ get; set; }
        public AppUser User{ get; set; }
        public String UserId{ get; set; }
    }
}
