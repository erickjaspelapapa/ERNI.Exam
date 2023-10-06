using ERNI.Reporsitory.Application.Account.Commands.Handlers;
using ERNI.Reporsitory.Application.Account.Queries;
using ERNI.Reporsitory.Application.Account.Queries.Handlers;
using ERNI.Reporsitory.Repository.Contracts;
using Moq;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ERNI.Test
{
    public class ClienLoginQueryTest
    {

        private Mock<IClientAccount> mockClientAccount;

        [SetUp]
        public void Setup()
        {
            mockClientAccount = new Mock<IClientAccount>();
        }

        [Test]
        public async Task ClientQuery_Handle_WhenClientLogsIn_ReturnToken()
        {
            //Arrange
            ClientLoginQuery query = new ClientLoginQuery()
            { 
                EmailAddress = "erick",
                Password = "erick"
            };

            string expected = "Token";

            mockClientAccount.Setup(x => x.GetToken(query)).ReturnsAsync(expected);
            var ClientQueryService = new ClientLoginQueryHandler(mockClientAccount.Object);

            string token = await ClientQueryService.Handle(query, It.IsAny<CancellationToken>());
            //Act
            //Assert
            Assert.IsNotNull(token);
            Assert.IsTrue(token.Length > 0);
        }
    }
}
