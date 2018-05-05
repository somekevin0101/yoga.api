using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YogaApi.Core.Interfaces;
using YogaApi.Core.Models;

namespace YogaApi.Implementations.Repositories
{
    public class SequencesRepository : ISequencesRepository
    {
        public Task<string> SaveSequence(Sequence sequence)
        {
            throw new NotImplementedException();
        }
    }
}
