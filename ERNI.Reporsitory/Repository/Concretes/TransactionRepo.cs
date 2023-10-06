using ERNI.Data.Data;
using ERNI.Data.Model;
using ERNI.Reporsitory.Application.Account.Queries;
using ERNI.Reporsitory.Application.Transaction.Commands;
using ERNI.Reporsitory.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ERNI.Reporsitory.Repository.Concretes
{
    public enum TransactionType
    {
        DB,
        CR
    }
    public class TransactionRepo : ITransaction
    {
        private readonly ERNIContext _erniContext;
        //instanstiate context from Data folder
        public TransactionRepo(ERNIContext erniContext)
        {
            _erniContext = erniContext;
        }


        //implementation of Deposit Function
        public async Task<string> AccountDeposit(AccountTransCommand AccountBase)
        {
            if (AccountBase.Amount == 0)
                return "Amount should be higher than 0";

            AccountModel account = AccountBase.Account;

            LedgerModel ledger = new LedgerModel()
            {
                client_id = account.ClientBase.id,
                trans_amount = AccountBase.Amount,
                trans_type = TransactionType.DB.ToString(),
                description = string.Format("An Amount of {0} was debited to account {1}", AccountBase.Amount, account.ClientBase.email_address)
            };

            _erniContext.ledger.Add(ledger);
            await _erniContext.SaveChangesAsync();
            string result = ledger.description;

            return result;
        }

        //implementation of Transfer Function

        public async Task<(bool, string)> AccountTransferToAnother(AccountTransCommand from, AccountTransCommand to)
        {
            (bool, string) hasWithdrawn = await AccountWithdraw(from);

            // if validates successful withdraw then passed to deposit to another account 
            // item2 result varies from withdraw function s

            if (hasWithdrawn.Item1)
            { 
                string hasDeposited = await AccountDeposit(to);
                string result = string.Format("{0}. {1}", hasWithdrawn.Item2, hasDeposited);

                return (true, result);
            }
            else
            {
                return (false, hasWithdrawn.Item2);
            }
        }
        
        //Implementation for Withdraw Function
        public async Task<(bool,string)> AccountWithdraw(AccountTransCommand AccountBase)
        {
            AccountModel account = AccountBase.Account;
            if (AccountBase.Amount == 0)
                return (false,"Amount should be higher than 0");
            if (account.account_balance >= AccountBase.Amount)
            {
                LedgerModel ledger = new LedgerModel()
                {
                    client_id = account.ClientBase.id,
                    trans_amount = AccountBase.Amount,
                    trans_type = TransactionType.CR.ToString(),
                    description = string.Format("An Amount of {0} was credited from account {1}", AccountBase.Amount, account.ClientBase.email_address)
                };

                _erniContext.ledger.Add(ledger);
                await _erniContext.SaveChangesAsync();
                return (true,ledger.description);
            }
            else
            {
                return (false, "Insufficient Balance!");
            }
        }

        //Get Account Balance
        public async Task<AccountModel> GetAccountBalance(ClientBalanceQuery account)
        {
            AccountModel accounts = await _erniContext.account.Where(w => w.ClientBase.email_address == account.Email).FirstOrDefaultAsync();
            return accounts;
        }

        public async Task UpdateBalance(AccountTransCommand AccountBase, TransactionType transType)
        {

            AccountBase.Account.account_balance += transType == TransactionType.CR ? AccountBase.Amount * -1 : AccountBase.Amount;

            _erniContext.account.Update(AccountBase.Account);
            await _erniContext.SaveChangesAsync();
        }
    }
}
