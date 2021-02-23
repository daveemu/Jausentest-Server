using Jausentest.Domain.Entities;
using Jausentest.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jausentest.Infrastructure.Repositories
{
    public class BeislRepository : IBeislRepository
    {
        private readonly JausentestContext _jausentestContext;
        public BeislRepository(JausentestContext jausentestContext)
        {
            _jausentestContext = jausentestContext ?? throw new ArgumentNullException(nameof(jausentestContext));
        }

        public async Task<BeislEntity> AddBeisl(BeislEntity b)
        {
            var createdBeisl = _jausentestContext.Beisl.Add(b);
            await _jausentestContext.SaveChangesAsync();
            return createdBeisl.Entity;
        }

        public async Task<IEnumerable<BeislEntity>> GetBeisls()
        {
            return await _jausentestContext.Beisl.ToListAsync();
        }




    }
}
