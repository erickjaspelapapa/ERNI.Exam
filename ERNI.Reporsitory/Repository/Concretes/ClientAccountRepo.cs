using ERNI.Data.Data;
using ERNI.Data.Model;
using ERNI.Reporsitory.Application.Account.Queries;
using ERNI.Reporsitory.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ERNI.Reporsitory.Repository.Concretes
{
    public class ClientAccountRepo : IClientAccount
    {
        private readonly ERNIContext _erniContext;
        private readonly IConfiguration _configuration;

        //instanstiate context from Data folder
        public ClientAccountRepo(ERNIContext erniContext, IConfiguration configuration)
        {
            _erniContext = erniContext;
            _configuration = configuration;
        }

        //Get Client Token based on client login 
        public async Task<string> GetToken(ClientLoginQuery client)
        {
            ClientModel user = await _erniContext.client.FirstOrDefaultAsync(w => w.email_address == client.EmailAddress && w.password == w.password);
            string tokenString = string.Empty;
            
            if(user != null) {
                tokenString = GenerateToken(user);
            }

            return tokenString;
        }
        // Generate JWT Token to access authorized endpoints
        private string GenerateToken(ClientModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                            new Claim(JwtRegisteredClaimNames.Sub, user.email_address),
                            new Claim(JwtRegisteredClaimNames.Email, user.email_address),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //Register new Client to DB
        public async Task<ClientModel> RegisterClient(ClientModel newClient)
        {
            _erniContext.client.Add(newClient);
            await _erniContext.SaveChangesAsync();

            return newClient;
        }

        //Register new Account to DB
        public async Task<AccountModel> RegisterAccount(AccountModel newAccount)
        {
            _erniContext.account.Add(newAccount);
            await _erniContext.SaveChangesAsync();

            return newAccount;
        }
    }
}
