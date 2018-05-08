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

        public async Task<long> SaveSequenceData(Sequence sequence)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@SequenceName", sequence.SequenceName);
                parameters.Add("@SequenceStyle", sequence.SequenceStyle);
                parameters.Add("@UserId", sequence.UserId);

                return await db.ExecuteScalarAsync<long>
                    ("Insert into dbo.Sequences values(@SequenceName, @SequenceStyle, @UserId) select @@Identity",
                    parameters, commandType: CommandType.Text).ConfigureAwait(false);
            }
        }

        public async Task<SequencePoses> SavePoseData(long sequenceId, PoseOrder pose)
        {
            long sequencePosesId;
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@SequenceId", sequenceId);
                parameters.Add("@PoseId", pose.PoseId);
                parameters.Add("@OrderInSequence", pose.OrderInSequence);
                parameters.Add("@DurationInSeconds", pose.DurationInSeconds);
                parameters.Add("@IsMiniSequence", pose.IsMiniSequence);

                sequencePosesId = await db.ExecuteScalarAsync<long>
                    ("Insert into dbo.SequencePoses values(@SequenceId, @PoseId, @OrderInSequence, @DurationInSeconds, @IsMiniSequence) select @@Identity",
                    parameters, commandType: CommandType.Text).ConfigureAwait(false);
            }

            return new SequencePoses
            {
                SequencePosesId = sequencePosesId,
                PoseId = pose.PoseId,
                OrderInSequence = pose.OrderInSequence,
                IsMiniSequence = pose.IsMiniSequence
            };
        }

        public async Task SaveMiniSequence(long poseSequenceId, MiniPose pose)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                    var parameters = new DynamicParameters();
                    parameters.Add("@SequencePosesId", poseSequenceId);
                    parameters.Add("@PoseId", pose.PoseId);
                    parameters.Add("@OrderInMiniSequence", pose.OrderInMiniSequence);
                    parameters.Add("@DurationInSeconds", pose.DurationInSeconds);
                    await db.ExecuteAsync
                        ("Insert into dbo.MiniSequencePoses values(@SequencePosesId, @PoseId, @OrderInMiniSequence, @DurationInSeconds)",
                        parameters, commandType: CommandType.Text);
            }
        }
    }
}
