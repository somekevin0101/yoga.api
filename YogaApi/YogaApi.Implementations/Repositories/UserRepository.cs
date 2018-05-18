using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using YogaApi.Core.ConfigManager;
using YogaApi.Core.Interfaces;
using YogaApi.Core.Models;

namespace YogaApi.Implementations.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfigManager configManager)
        {
            _connectionString = configManager.GetConfigValue("ConnYoga");
        }

        public async Task<int> CreateUser(User user)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@EmailAddress", user.EmailAddress);
                parameters.Add("@FirstName", user.FirstName);
                parameters.Add("@LastName", user.LastName);
                parameters.Add("@ZipCode", user.ZipCode);

                return await db.ExecuteScalarAsync<int>
                    ("Insert into dbo.Users values(@EmailAddress, @FirstName, @LastName, @ZipCode) select @@Identity",
                    parameters, commandType: CommandType.Text).ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<ShallowSequence>> GetShallowSequences(int userId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", userId);

                return await db.QueryAsync<ShallowSequence>
                    ("Select SequenceId, SequenceName, SequenceStyle, IsCustomMiniSequence From dbo.Sequences Where UserId = @UserId",
                    parameters, commandType: CommandType.Text).ConfigureAwait(false);
            }
        }

        public async Task<User> GetUser(int userId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", userId);

                return await db.QuerySingleOrDefaultAsync<User>
                    ("Select * from dbo.Users Where UserId = @UserId",
                    parameters, commandType: CommandType.Text).ConfigureAwait(false);
            }
        }
    }
}
