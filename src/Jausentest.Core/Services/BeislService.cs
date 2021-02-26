using AutoMapper;
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
        private readonly IMapper _mapper;

        public BeislService(IBeislRepository beislRepository, IMapper mapper)
        {
            _beislRepository = beislRepository ?? throw new ArgumentNullException(nameof(beislRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        
        public async Task<BeislDto> AddBeislAsync(BeislDto b)
        {
            var _b = await _beislRepository.AddOrUpdateAsync(_mapper.Map<BeislDto, BeislEntity>(b));
            return _mapper.Map<BeislEntity, BeislDto>(_b);
        }

        public async Task<BeislDto> GetBeislByIdAsync(long beislId)
        {
            return _mapper.Map<BeislEntity, BeislDto>(await _beislRepository.GetBeislByIdAsync(beislId));
        }

        public async Task<IEnumerable<BeislDto>> GetBeislsAsync()
        {
            return _mapper.Map<IEnumerable<BeislEntity>, IEnumerable<BeislDto>>(await _beislRepository.GetAllAsync());
        }

        public async Task<IEnumerable<TagDto>> GetTagsForBeislIdAsync(long beislId)
        {

            return _mapper.Map<IEnumerable<TagEntity>, IEnumerable<TagDto>>(await _beislRepository.GetTagsForBeislIdAsync(beislId));
        }
    }
}
