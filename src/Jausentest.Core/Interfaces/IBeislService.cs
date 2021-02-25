using Jausentest.Core.Models;
using Jausentest.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jausentest.Core.Services
{
    public interface IBeislService
    {
        public Task<IEnumerable<BeislDto>> GetBeisls();
        public Task<BeislDto> AddBeisl(BeislDto b);
    }
}