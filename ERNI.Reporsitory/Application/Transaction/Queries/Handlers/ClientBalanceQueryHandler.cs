using ERNI.Data.Model;
using ERNI.Reporsitory.Application.Account.Queries;
using ERNI.Reporsitory.Repository.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERNI.Reporsitory.Application.Transaction.Queries.Handlers
{
    public class ClientBalanceQueryHandler : IRequestHandler<ClientBalanceQuery, AccountModel>
    {
        private readonly ITransaction _trans;
        //HANDLER : Get Client Account Balance
        public ClientBalanceQueryHandler(ITransaction trans)
        {
            _trans = trans;
        }
        public async Task<AccountModel> Handle(ClientBalanceQuery request, CancellationToken cancellationToken)
        {
            AccountModel result = await _trans.GetAccountBalance(request);
           
            return result;
        }
    }
}
