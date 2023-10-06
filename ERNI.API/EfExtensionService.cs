using ERNI.Data.Data;
using ERNI.Reporsitory.Repository.Contracts;
using ERNI.Reporsitory.Repository.Concretes;
using Microsoft.EntityFrameworkCore;
using ERNI.Reporsitory.Application.Account.Commands;
using ERNI.Reporsitory.Application.Account.Commands.Handlers;
using ERNI.Reporsitory.Application.Account.Queries.Handlers;
using ERNI.Reporsitory.Application.Transaction.Queries.Handlers;
using ERNI.Reporsitory.Application.Transaction.Commands;
using ERNI.Reporsitory.Application.Transaction.Commands.Handler;

namespace ERNI.API
{
    public static class EfExtensionService
    {
        public static IServiceCollection AddEntityFramework(this IServiceCollection services, string conStr)
        {
            //DBContext making connection to Database
            services.AddDbContext<ERNIContext>(opt => opt.UseLazyLoadingProxies().UseSqlServer(conStr, x => x.MigrationsAssembly("ERNI.Data")));

            //Dependency Injections
            services.AddScoped<IClientAccount, ClientAccountRepo>();
            services.AddScoped<ITransaction, TransactionRepo>();
            services.AddScoped<RegisterCommandHandler>();
            services.AddScoped<ClientLoginQueryHandler>();
            services.AddScoped<ClientBalanceQueryHandler>();
            services.AddScoped<AccountWithdrawCommandHandler>();
            services.AddScoped<AccountDepositCommandHandler>();
            services.AddScoped<AccountTransferCommandHandler>();

            //MediatR
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(RegisterCommand).Assembly));

            return services;
        }
    }
}
