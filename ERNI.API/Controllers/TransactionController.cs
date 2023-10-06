using ERNI.Data.Model;
using ERNI.Reporsitory.Application.Account.Queries;
using ERNI.Reporsitory.Application.Transaction.Commands;
using ERNI.Reporsitory.Application.Transaction.Commands.Handler;
using ERNI.Reporsitory.Application.Transaction.Queries.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ERNI.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ClientBalanceQueryHandler _balanceQueryHandler;
        private readonly AccountWithdrawCommandHandler _withdrawCommand;
        private readonly AccountDepositCommandHandler _depositCommand;
        private readonly AccountTransferCommandHandler _transferCommand;

        public TransactionController(ClientBalanceQueryHandler balanceQueryHandler, 
                                     AccountWithdrawCommandHandler withdrawCommand,
                                     AccountDepositCommandHandler depositCommand,
                                     AccountTransferCommandHandler transferCommand)
        {
            _balanceQueryHandler = balanceQueryHandler;
            _withdrawCommand = withdrawCommand;
            _depositCommand = depositCommand;
            _transferCommand = transferCommand;
        }

        [HttpGet]
        [Route("get-balance")]
        //ENDPOINT :  Get Client Account Balance
        public async Task<IActionResult> GetBalance()
        {
            ClientBalanceQuery user = new ClientBalanceQuery() { Email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value };

            AccountModel account = await _balanceQueryHandler.Handle(user, new CancellationToken());

            string result = string.Format("Balance for Account {0} is {1:n2}", account.ClientBase?.email_address, account.account_balance);

            return Ok(result);
        }

        [HttpPost]
        [Route("account-withdraw")]
        //ENDPOINT :  Withdraw Amount from Account
        public async Task<IActionResult> WithdrawFromAccount([FromBody] decimal amount)
        {
            ClientBalanceQuery user = new ClientBalanceQuery() { Email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value };
            AccountModel account = await _balanceQueryHandler.Handle(user, new CancellationToken());

            AccountTransCommand AccountBase = new AccountTransCommand()
            {
                Account = account,
                Amount = amount
            };

            string result = await _withdrawCommand.Handle(AccountBase, new CancellationToken());

            return Ok(result);
        }

        [HttpPost]
        [Route("account-deposit")]
        //ENDPOINT :  Deposit Amount from Account
        public async Task<IActionResult> DepositToAccount([FromBody] decimal amount)
        {
            ClientBalanceQuery user = new ClientBalanceQuery() { Email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value };
            AccountModel account = await _balanceQueryHandler.Handle(user, new CancellationToken());
            
            AccountTransCommand AccountBase = new AccountTransCommand()
            {
                Account = account,
                Amount = amount
            };

            string result = await _depositCommand.Handle(AccountBase, new CancellationToken());

            return Ok(result);
        }

        [HttpPost]
        [Route("transfer")]
        //ENDPOINT : Transfer amount to anoter Account
        public async Task<IActionResult> TransferToAccount([FromBody] AccountTransferCommand request)
        {
            ClientBalanceQuery user = new ClientBalanceQuery() { Email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value };

            AccountTransferCommand payload = new AccountTransferCommand()
            {
                AccountTranferToId = request.AccountTranferToId,
                AccountTranferFromId = user.Email,
                Amount = request.Amount
            };

            string result = await  _transferCommand.Handle(payload, new CancellationToken());
            return Ok(result);
        }

    }
}
