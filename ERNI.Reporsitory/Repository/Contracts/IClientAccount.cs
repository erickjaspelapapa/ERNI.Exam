using ERNI.Data.Model;
using ERNI.Reporsitory.Application.Account.Queries;
using ERNI.Reporsitory.Application.Transaction.Queries;

namespace ERNI.Reporsitory.Repository.Contracts
{
    public interface IClientAccount
    {
        Task<ClientModel> RegisterClient(ClientModel newClient);
        Task<AccountModel> RegisterAccount(AccountModel newAccount);

        Task<string> GetToken(ClientLoginQuery client);
    }
}
