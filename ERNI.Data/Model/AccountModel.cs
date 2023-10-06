using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERNI.Data.Model
{
    public class AccountModel : BaseEntity
    {
        public decimal account_balance { get; set; }

        public int client_id { get; set; } // FK for Client Table
        public virtual ClientModel? ClientBase { get; set; }
    }
}
