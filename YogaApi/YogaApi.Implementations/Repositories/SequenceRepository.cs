using System;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using System.Threading.Tasks;
using YogaApi.Core.Interfaces;
using YogaApi.Core.Models;
using YogaApi.Core.ConfigManager;
using System.Collections.Generic;

namespace YogaApi.Implementations.Repositories
{
    public class SequenceRepository : ISequenceRepository
    {
        private readonly string _connectionString;

        public SequenceRepository(IConfigManager configManager)
        {
            _connectionString = configManager.GetConfigValue("ConnYoga");
        }

        public async Task<long> SaveSequenceData(Sequence sequence)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@SequenceName", sequence.SequenceName);
                parameters.Add("@SequenceStyle", sequence.SequenceStyle);
                parameters.Add("@UserId", sequence.UserId);
                parameters.Add("@IsCustomMiniSequence", sequence.IsCustomMiniSequence);

                return await db.ExecuteScalarAsync<long>
                    ("spSaveSequence", parameters, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            }
        }

        public async Task<SequencePose> SavePoseData(long sequenceId, PoseOrder pose)
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
                    ("spSaveSequencePoses", parameters, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            }

            return new SequencePose
            {
                SequencePosesId = sequencePosesId,
                PoseId = pose.PoseId,
                OrderInSequence = pose.OrderInSequence,
                IsMiniSequence = pose.IsMiniSequence
            };
        }

        public async Task SaveMiniSequencePose(long poseSequenceId, MiniPose pose)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                    var parameters = new DynamicParameters();
                    parameters.Add("@SequencePosesId", poseSequenceId);
                    parameters.Add("@PoseId", pose.PoseId);
                    parameters.Add("@OrderInMiniSequence", pose.OrderInMiniSequence);
                    parameters.Add("@DurationInSeconds", pose.DurationInSeconds);
                    await db.ExecuteAsync
                        ("spSaveMiniSequencePoses", parameters, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<SequencePose>> GetSequencePoses(long sequenceId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@SequenceId", sequenceId);

                 return await db.QueryAsync<SequencePose>
                    ("spGetSequencePoses", parameters, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<MiniPose>> GetMiniPoses(long sequencePosesId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@SequencePosesId", sequencePosesId);

                return await db.QueryAsync<MiniPose>
                   ("spGetMiniSequencePoses", parameters, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            }
        }
    }
}
