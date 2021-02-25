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

        public Task<IEnumerable<BeislEntity>> GetAll();
        public Task<BeislEntity> AddOrUpdateAsync(BeislEntity beisl);

    }
}
