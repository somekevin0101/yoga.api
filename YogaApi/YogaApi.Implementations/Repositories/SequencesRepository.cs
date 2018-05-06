using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using System.Threading.Tasks;
using YogaApi.Core.Interfaces;
using YogaApi.Core.Models;

namespace YogaApi.Implementations.Repositories
{
    public class SequencesRepository : ISequencesRepository
    {
        private readonly string _connectionString;

        public SequencesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<long> SaveSequence(Sequence sequence)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@SequenceName", sequence.SequenceName);
                parameters.Add("@SequenceStyle", sequence.SequenceStyle);
                parameters.Add("@UserId", sequence.UserId);

                return await db.ExecuteScalarAsync<long>
                    ("Insert into dbo.Sequences values(@SequenceName, @SequenceStyle, @UserId) select @@Identity", parameters, commandType: CommandType.Text);
            }
        }
    }
}
