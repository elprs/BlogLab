using BlogLabModels.Account;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace BlogLab.Repository
{
    public class AccountRepository : IAccountRepository
    {
        // This configuration allows the access to the db via the appsettings.json
        private readonly IConfiguration _config;

        public AccountRepository( IConfiguration config)
        {
            _config = config;
        }
        public async Task<IdentityResult> CreateAsync(ApplicationUserIdentity user, 
            CancellationToken cancellationToken)
        {

            //Creating a virtual table that will represent our type 
            cancellationToken.ThrowIfCancellationRequested();
            var dataTable = new DataTable();
            dataTable.Columns.Add("UserName", typeof(string));
            dataTable.Columns.Add("NormalizedUserName", typeof(string));
            dataTable.Columns.Add("Email", typeof(string));
            dataTable.Columns.Add("NormalizedEmail", typeof(string));
            dataTable.Columns.Add("FullName", typeof(string));
            dataTable.Columns.Add("PasswordHash", typeof(string));

            dataTable.Rows.Add(
                user.UserName, 
                user.NormalizedUserName, 
                user.Email, 
                user.NormalizedEmail, 
                user.FullName, 
                user.PasswordHash);


            //Open a  connection 
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync(cancellationToken);

                // Execution of stored procedure that allows to insert an account
                await connection.ExecuteAsync("Account_Insert",
                    new { Account = dataTable.AsTableValuedParameter("dbo.AccountType") }, 
                    commandType: CommandType.StoredProcedure);
            }

            return IdentityResult.Success;
        }

        public async Task<ApplicationUserIdentity> GetByUserNameAsync(string normalizedUserName, 
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ApplicationUserIdentity applicationUserIdentity;
            //Open a  connection 
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync(cancellationToken);
                IDbTransaction dbTransaction = null;
                CommandType commandType = CommandType.StoredProcedure;

                // Execution of stored procedure that allows to insert an account
                    applicationUserIdentity = await connection.QuerySingleOrDefault<dynamic>(
                    "Account_GetByUserName",
                    new { NormalizedUserName = normalizedUserName },
                    dbTransaction,
                    null,
                    commandType
                    );
            }

            return applicationUserIdentity;
        }
    }
}
