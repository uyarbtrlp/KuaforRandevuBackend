using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KuaforRandevuBackend.Models
{
    public class AppUserModel
    {
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Username { get; set; }
        public String Email { get; set; }
        public String Address { get; set; }
        public String PhoneNumber { get; set; }
        public String Password { get; set; }
        public String StoreName { get; set; }
        public String StoreType { get; set; }
        public String StoreAddress { get; set; }
        public String StorePhoneNumber { get; set; }

    }
}
