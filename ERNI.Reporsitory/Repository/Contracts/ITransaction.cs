using ERNI.Data.Model;
using ERNI.Reporsitory.Application.Account.Queries;
using ERNI.Reporsitory.Application.Transaction.Commands;
using ERNI.Reporsitory.Repository.Concretes;
using System.Transactions;

namespace ERNI.Reporsitory.Repository.Contracts
{
    public interface ITransaction
    {
        Task<AccountModel> GetAccountBalance(ClientBalanceQuery account);
        Task UpdateBalance(AccountTransCommand AccountBase, TransactionType transType);

        Task<(bool,string)> AccountWithdraw(AccountTransCommand AccountBase);
        Task<string> AccountDeposit(AccountTransCommand AccountBase);
        Task<(bool, string)> AccountTransferToAnother(AccountTransCommand from, AccountTransCommand to);
    }
}
