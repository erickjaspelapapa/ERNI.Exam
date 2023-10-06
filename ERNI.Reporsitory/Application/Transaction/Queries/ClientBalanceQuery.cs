using ERNI.Data.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERNI.Reporsitory.Application.Account.Queries
{
    public class ClientBalanceQuery: IRequest<AccountModel>
    {
        public string Email { get; set; }
    }
}
