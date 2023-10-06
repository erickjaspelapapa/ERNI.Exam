using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERNI.Data.Model
{
    public  class ClientModel : BaseEntity
    {
        public string email_address { get; set; }
        public string password { get; set; }
        public int? protected_pin { get; set;}
    }
}
