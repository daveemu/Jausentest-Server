using Jausentest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jausentest.Domain.Interfaces
{
    public interface IBeislRepository
    {

        public Task<IEnumerable<BeislEntity>> GetAllAsync();
        public Task<BeislEntity> AddOrUpdateAsync(BeislEntity beisl);
        public Task<BeislEntity> GetBeislByIdAsync(long beislId);
        public Task<IEnumerable<TagEntity>> GetTagsForBeislIdAsync(long beislId);
        public Task<BeislEntity> AddBeislAsync(BeislEntity beisl);
        public Task<BeislEntity> AddTagToBeislAsync(TagEntity tag, long beislId);
    }
}
