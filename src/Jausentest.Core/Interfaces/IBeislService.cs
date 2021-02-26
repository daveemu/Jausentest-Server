using Jausentest.Core.Models;
using Jausentest.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jausentest.Core.Services
{
    public interface IBeislService
    {
        public Task<IEnumerable<BeislDto>> GetBeislsAsync();
        public Task<BeislDto> AddBeislAsync(BeislDto b);
        public Task<BeislDto> GetBeislByIdAsync(long beislId);
        public Task<IEnumerable<TagDto>> GetTagsForBeislIdAsync(long beislId);


    }
}