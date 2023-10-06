using ERNI.Data.Model;
using ERNI.Reporsitory.Repository.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERNI.Reporsitory.Application.Account.Queries.Handlers
{
    public class ClientLoginQueryHandler : IRequestHandler<ClientLoginQuery, string>
    {
        private readonly IClientAccount _clientAccount;
        //HANDLER : Client login to create token
        public ClientLoginQueryHandler(IClientAccount clientAccount)
        {
            _clientAccount = clientAccount;
        }
        public async Task<string> Handle(ClientLoginQuery request, CancellationToken cancellationToken)
        {
            string result =  await _clientAccount.GetToken(request);
            return result;
        }
    }
}
