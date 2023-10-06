using ERNI.Data.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERNI.Reporsitory.Application.Account.Commands
{
    public class RegisterCommand : IRequest<ClientModel>
    {
        public string email_address { get; set; }
        public string password { get; set; }
    }
}
