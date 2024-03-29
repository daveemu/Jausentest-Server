﻿using AutoMapper;
using Jausentest.Core.Models;
using Jausentest.Domain.Entities;
using Jausentest.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jausentest.Core.Interfaces;

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

        public async Task<BeislDto> AddBeislAsync(BeislDto beisl)
        {
            var _beisl = await _beislRepository.AddBeislAsync(_mapper.Map<BeislDto, BeislEntity>(beisl));
            return _mapper.Map<BeislEntity, BeislDto>(_beisl);
        }

        public async Task<BeislDto> AddOrUpdateBeislAsync(BeislDto beisl)
        {
            var _beisl = await _beislRepository.AddOrUpdateAsync(_mapper.Map<BeislDto, BeislEntity>(beisl));
            return _mapper.Map<BeislEntity, BeislDto>(_beisl);
        }

        public async Task<BeislDto> AddTagToBeislAsync(TagDto tag, long beislId)
        {
            var _beisl = await _beislRepository.AddTagToBeislAsync(_mapper.Map<TagDto, TagEntity>(tag), beislId);
            return _mapper.Map<BeislEntity, BeislDto>(_beisl);
        }

        public async Task<BeislDto> DeleteTagFromBeislAsync(TagDto tag, long beislId)
        {
            var _beisl = await _beislRepository.DeleteTagFromBeislAsync(_mapper.Map<TagDto, TagEntity>(tag), beislId);
            return _mapper.Map<BeislEntity, BeislDto>(_beisl);
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
        
        public async Task<IEnumerable<RatingDto>> GetRatingsForBeislIdAsync(long beislId)
        {
            return _mapper.Map<IEnumerable<RatingEntity>, IEnumerable<RatingDto>>(
                await _beislRepository.GetRatingsForBeislIdAsync(beislId));
        }

        public async Task<BeislDto> AddRatingToBeislAsync(RatingDto rating, long beislId)
        {
            var _beisl = await _beislRepository.AddRatingToBeislAsync(_mapper.Map<RatingDto, RatingEntity>(rating), beislId);
            return _mapper.Map<BeislEntity, BeislDto>(_beisl);
        }

        public async Task<BeislDto> DeleteRatingFromBeislAsync(RatingDto rating, long beislId)
        {
            var _beisl =
                await _beislRepository.DeleteRatingFromBeislAsync(_mapper.Map<RatingDto, RatingEntity>(rating),
                    beislId);
            return _mapper.Map<BeislEntity, BeislDto>(_beisl);
        }

        public async Task<BeislDto> AddImageToBeisl(ImageDto image, long beislId)
        {
            var _beisl = await _beislRepository.AddImageToBeisl(_mapper.Map<ImageDto, ImageEntity>(image), beislId);
            return _mapper.Map<BeislEntity, BeislDto>(_beisl);
        }
    }
}
