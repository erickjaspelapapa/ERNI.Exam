using ERNI.Data.Data;
using ERNI.Data.Model;
using ERNI.Reporsitory.Application.Transaction.Commands;
using ERNI.Reporsitory.Application.Transaction.Commands.Handler;
using ERNI.Reporsitory.Repository.Concretes;
using ERNI.Reporsitory.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERNI.Test
{
    public class AccountTransCommandTest
    {
        private Mock<ERNIContext> mockContext;

        [SetUp]
        public void Setup()
        {
            mockContext = new Mock<ERNIContext>();
        }

        [Test]
        public async Task ITransaction_AccountWithdraw_WhenClientEnter0_ReturnZeroValidation() 
        {
            //Arrange
            AccountTransCommand mockData = new AccountTransCommand()
            {
                Account = new AccountModel () { account_balance = 0, client_id =  1, id = 1},
                Amount = 0
            };
            (bool, string) expected = (false, "Amount should be higher than 0");
           
            var service = new TransactionRepo(null);
            //Act
            var result = await service.AccountWithdraw(mockData);

            //Assert
            Assert.That(expected, Is.EqualTo(result));
        }
        [Test]
        public async Task ITransaction_AccountWithdraw_WhenClientEnterOverBalance_ReturnInsufficientValidation() {

            //Arrange
            AccountTransCommand mockData = new AccountTransCommand()
            {
                Account = new AccountModel() { account_balance = 1, client_id = 1, id = 1 },
                Amount = 2
            };
            (bool, string) expected = (false, "Insufficient Balance!");

            var service = new TransactionRepo(null);
            //Act
            var result = await service.AccountWithdraw(mockData);

            //Assert
            Assert.That(expected, Is.EqualTo(result));
        }
        [Test]
        public async Task ITransaction_AccountWithdraw_WhenClientWithdrawAmount_ReturnWithdrawn() {
            //Arrange
            AccountTransCommand mockData = new AccountTransCommand()
            {
                Account = new AccountModel() { account_balance = 1000, client_id = 2, id = 2, ClientBase = new ClientModel() { id = 1,email_address = "Test"} },
                Amount = 200
            };

            LedgerModel mockLedger = new LedgerModel()
            {
                id = 1,
                client_id = mockData.Account.client_id,
                description = $"An Amount of {mockData.Amount} was credited from account {mockData.Account.ClientBase.email_address}",
                trans_amount = 200,
                trans_type = "CR"
            };

            var context = await AddLedgerAsync(mockLedger);

            (bool, string) expected = (true, $"An Amount of {mockData.Amount} was credited from account {mockData.Account.ClientBase.email_address}");

            var service = new TransactionRepo(context);
            //Act
            var result = await service.AccountWithdraw(mockData);

            //Assert
            Assert.That(expected, Is.EqualTo(result));
        }

        private async Task<ERNIContext> AddLedgerAsync(LedgerModel mockData)
        {
            var options = new DbContextOptionsBuilder<ERNIContext>()
                .UseInMemoryDatabase(databaseName: "exERNIDB")
                .Options;
            var databaseContext = new ERNIContext(options);
            databaseContext.Database.EnsureCreated();

            databaseContext.ledger.Add(mockData);
            await databaseContext.SaveChangesAsync();

            return databaseContext;
        }

        [Test]
        public async Task ITransaction_AccountDeposit_WhenClientDepositZero_ReturnZeroValidation()
        {
            //Arrange
            AccountTransCommand mockData = new AccountTransCommand()
            {
                Account = new AccountModel() { account_balance = 0, client_id = 1, id = 1 },
                Amount = 0
            };
             string expected = "Amount should be higher than 0";

            var service = new TransactionRepo(null);
            //Act
            var result = await service.AccountDeposit(mockData);

            //Assert
            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public async Task ITransaction_AccountDeposit_WhenClientDepositAmount_ReturnDepositSuccess()
        {
            //Arrange
            AccountTransCommand mockData = new AccountTransCommand()
            {
                Account = new AccountModel() { account_balance = 1000, client_id = 1, id = 1, ClientBase = new ClientModel() { id = 1, email_address = "Test" } },
                Amount = 200
            };

            LedgerModel mockLedger = new LedgerModel()
            {
                id = 2,
                client_id = mockData.Account.client_id,
                description = $"An Amount of {mockData.Amount} was debited to account {mockData.Account.ClientBase.email_address}",
                trans_amount = 200,
                trans_type = "DB"
            };


            var context = await AddLedgerAsync(mockLedger);

            string expected =  $"An Amount of {mockData.Amount} was debited to account {mockData.Account.ClientBase.email_address}";

            var service = new TransactionRepo(context);
            //Act
            var result = await service.AccountDeposit(mockData);

            //Assert
            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public async Task ITransaction_AccountTransferToAnother_WhenClientTransferZero_ReturnZeroValidation()
        {
            AccountTransCommand mockFromData = new AccountTransCommand()
            {
                Account = new AccountModel() { account_balance = 0, client_id = 1, id = 1 },
                Amount = 0
            };
            AccountTransCommand mockToData = new AccountTransCommand()
            {
                Account = new AccountModel() { account_balance = 0, client_id = 2, id = 2 },
                Amount = 0
            };
            (bool,string) expected = (false, "Amount should be higher than 0");

            var service = new TransactionRepo(null);
            //Act
            var result = await service.AccountTransferToAnother(mockFromData, mockToData);

            //Assert
            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public async Task ITransaction_AccountTransferToAnother_WhenClientTransferOverBalance_ReturnZeroValidation()
        {
            AccountTransCommand mockFromData = new AccountTransCommand()
            {
                Account = new AccountModel() { account_balance = 0, client_id = 1, id = 1 },
                Amount = 200
            };
            AccountTransCommand mockToData = new AccountTransCommand()
            {
                Account = new AccountModel() { account_balance = 0, client_id = 2, id = 2 },
                Amount = 0
            };
            (bool, string) expected = (false, "Insufficient Balance!");

            var service = new TransactionRepo(null);
            //Act
            var result = await service.AccountTransferToAnother(mockFromData, mockToData);

            //Assert
            Assert.That(expected, Is.EqualTo(result));
        }
    }
}
