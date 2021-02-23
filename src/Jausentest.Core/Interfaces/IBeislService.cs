using Jausentest.Core.Models;
using Jausentest.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jausentest.Core.Services
{
    public interface IBeislService
    {
        public Task<IEnumerable<BeislEntity>> GetBeisls();
        public Task<BeislEntity> AddBeisl(BeislEntity b);
    }
}