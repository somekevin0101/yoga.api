using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YogaApi.Core.Models;

namespace YogaApi.Core.Interfaces
{
    public interface ISequencesRepository
    {
        Task<long> SaveSequence(Sequence sequence);
    }
}
