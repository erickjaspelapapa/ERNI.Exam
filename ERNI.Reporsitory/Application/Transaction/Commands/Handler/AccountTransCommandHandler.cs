using ERNI.Data.Model;
using ERNI.Reporsitory.Application.Account.Queries;
using ERNI.Reporsitory.Repository.Concretes;
using ERNI.Reporsitory.Repository.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERNI.Reporsitory.Application.Transaction.Commands.Handler
{
    public class AccountWithdrawCommandHandler : IRequestHandler<AccountTransCommand, string>
    {
        private readonly ITransaction _transaction;
        public AccountWithdrawCommandHandler(ITransaction transaction)
        {
            _transaction = transaction;
        }
        //HANDLER: Withdrawal Function
        public async Task<string> Handle(AccountTransCommand request, CancellationToken cancellationToken)
        {
            (bool,string) result = await _transaction.AccountWithdraw(request);
            if(result.Item1)
            {
                await _transaction.UpdateBalance(request, TransactionType.CR);
            }
            return result.Item2;
        }
    }

    public class AccountDepositCommandHandler : IRequestHandler<AccountTransCommand, string>
    {
        private readonly ITransaction _transaction;

        public AccountDepositCommandHandler(ITransaction transaction)
        {
            _transaction = transaction;
        }
        //HANDLER: Deposit Function
        public async Task<string> Handle(AccountTransCommand request, CancellationToken cancellationToken)
        {
            string result = await _transaction.AccountDeposit(request);
            await _transaction.UpdateBalance(request, TransactionType.DB);

            return result;
        }
    }

    public class AccountTransferCommandHandler : IRequestHandler<AccountTransferCommand, string>
    {
        private readonly ITransaction _transaction;

        public AccountTransferCommandHandler(ITransaction transaction)
        {
            _transaction = transaction;
        }
        //HANDLER: Transfer Function

        public async Task<string> Handle(AccountTransferCommand request, CancellationToken cancellationToken)
        {
            AccountModel AccountFromBase = await _transaction.GetAccountBalance(new ClientBalanceQuery() { Email = request.AccountTranferFromId });
            AccountTransCommand AccountFrom = new AccountTransCommand()
            {
                Account = AccountFromBase,
                Amount = request.Amount
            };
            AccountModel AccountToBase = await _transaction.GetAccountBalance(new ClientBalanceQuery() { Email = request.AccountTranferToId });
            if (AccountToBase == null)
                return string.Format("Account for {0} not existed!", request.AccountTranferToId);
            AccountTransCommand AccountTo = new AccountTransCommand()
            {
                Account = AccountToBase,
                Amount = request.Amount
            };

            (bool, string) hasTransfered = await _transaction.AccountTransferToAnother(AccountFrom, AccountTo);

            if (hasTransfered.Item1)
            {
                await _transaction.UpdateBalance(AccountFrom, TransactionType.CR);
                await _transaction.UpdateBalance(AccountTo, TransactionType.DB);
            }

            return hasTransfered.Item2;
        }
    }

}
