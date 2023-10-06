using Azure.Core;
using ERNI.Data.Model;
using ERNI.Reporsitory.Application.Account.Commands;
using ERNI.Reporsitory.Application.Account.Commands.Handlers;
using ERNI.Reporsitory.Repository.Contracts;
using Moq;

namespace ERNI.Test
{
    public class RegisterCommandTest
    {
        private Mock<IClientAccount> mockClientAccount; 

        [SetUp]
        public void Setup()
        {
            mockClientAccount = new Mock<IClientAccount>();
        }

        [Test]
        public async Task RegisterCommand_Handle_WhenClientRegistered_ReturnClientModel ()
        {
            //Arrange
            ClientModel expectedClient = new ClientModel()
            {
                id = It.IsAny<int>(),
                email_address = It.IsAny<string>(),
                password = It.IsAny<string>(),
                protected_pin = It.IsAny<int?>()
            };

            AccountModel expectedAccount = new AccountModel()
            {
                id = It.IsAny<int>(),
                account_balance = It.IsAny<Decimal>(),
                client_id = It.IsAny<int>(),
            };

            RegisterCommand registerClient = new RegisterCommand()
            {
                email_address = It.IsAny<string>(),
                password = It.IsAny<string>()
            };

            mockClientAccount.Setup(x => x.RegisterClient(It.IsAny<ClientModel>())).ReturnsAsync(expectedClient);
            mockClientAccount.Setup(x => x.RegisterAccount(It.IsAny<AccountModel>())).ReturnsAsync(expectedAccount);

            var RegisterService = new RegisterCommandHandler(mockClientAccount.Object);

            //Act
            var result = await RegisterService.Handle(registerClient, It.IsAny<CancellationToken>());

            //Assert

            Assert.That(result, Is.EqualTo(expectedClient));
        }

    }
}