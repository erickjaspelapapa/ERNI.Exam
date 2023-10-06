using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERNI.Data.Model
{
    public class LedgerModel : BaseEntity
    {
        public string trans_type{ get; set; }
        public string description { get; set; }
        public decimal trans_amount { get; set; }

        public int client_id { get; set; } // FK for Client Tblae
        public virtual ClientModel ClientBase { get; set; }
    }
}
