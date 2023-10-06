using ERNI.Data.Model;
using ERNI.Reporsitory.Application.Account.Queries;
using ERNI.Reporsitory.Application.Transaction.Queries.Handlers;
using ERNI.Reporsitory.Repository.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERNI.Test
{
    public class ClientBalanceQuerytTest
    {
        private Mock<ITransaction> mockTransaction;

        [SetUp]
        public void Setup()
        {
            mockTransaction = new Mock<ITransaction>();
        }

        [Test]
        public async Task ClientBalanceQuery_Handle_WhenClientCheckBalance_ReturnBalance() 
        {
            //Arrange
            AccountModel expected = new AccountModel()
            {
                account_balance = 1000,
                client_id = 1,
                id = 1,
            };
            ClientBalanceQuery client = new ClientBalanceQuery()
            {
                Email = It.IsAny<string>(),
            };

            mockTransaction.Setup(x => x.GetAccountBalance(client)).ReturnsAsync(expected);
            var service = new ClientBalanceQueryHandler(mockTransaction.Object);

            //Act

            var result = await service.Handle(client, It.IsAny<CancellationToken>());

            // Assert 
            Assert.That(result, Is.EqualTo(expected));
        }

    }
}
