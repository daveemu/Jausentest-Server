using Jausentest.Core.Models;
using Jausentest.Domain.Entities;
using Jausentest.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jausentest.Core.Services
{
    public class BeislService : IBeislService
    {
        private readonly IBeislRepository _beislRepository;
        public BeislService(IBeislRepository beislRepository)
        {
            _beislRepository = beislRepository ?? throw new ArgumentNullException(nameof(beislRepository));
        }

        
        public async Task<BeislEntity> AddBeisl(BeislEntity b)
        {
            return await _beislRepository.AddBeisl(b);
        }

        public async Task<IEnumerable<BeislEntity>> GetBeisls()
        {
            return await _beislRepository.GetBeisls();
        }

    }
}
