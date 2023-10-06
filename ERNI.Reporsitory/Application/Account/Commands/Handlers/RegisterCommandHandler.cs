using ERNI.Data.Model;
using ERNI.Reporsitory.Repository.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERNI.Reporsitory.Application.Account.Commands.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ClientModel>
    {
        private readonly IClientAccount _clientAccount;
        public RegisterCommandHandler(IClientAccount clientAccount)
        {
            _clientAccount = clientAccount;
        }
        //HANDLER : Client Registeration
        public async Task<ClientModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            ClientModel model = new ClientModel()
            {
                email_address = request.email_address,
                password = request.password,
            };
            
            ClientModel result = await _clientAccount.RegisterClient(model);

            AccountModel newAccount = new AccountModel()
            {
                client_id = result.id,
                account_balance = 0
            };

            await _clientAccount.RegisterAccount(newAccount);

            return result;
        }
    }
}
