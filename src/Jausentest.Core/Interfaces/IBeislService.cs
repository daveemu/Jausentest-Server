using System.Collections.Generic;
using System.Threading.Tasks;
using Jausentest.Core.Models;

namespace Jausentest.Core.Interfaces
{
    public interface IBeislService
    {
        public Task<IEnumerable<BeislDto>> GetBeislsAsync();
        public Task<BeislDto> AddBeislAsync(BeislDto beisl);
        public Task<BeislDto> AddOrUpdateBeislAsync(BeislDto beisl);
        public Task<BeislDto> GetBeislByIdAsync(long beislId);
        public Task<IEnumerable<TagDto>> GetTagsForBeislIdAsync(long beislId);
        public Task<BeislDto> AddTagToBeislAsync(TagDto tag, long beislId);
        public Task<BeislDto> DeleteTagFromBeislAsync(TagDto tag, long beislId);
        public Task<IEnumerable<RatingDto>> GetRatingsForBeislIdAsync(long beislId);
        public Task<BeislDto> AddRatingToBeislAsync(RatingDto rating, long beislId);
        public Task<BeislDto> DeleteRatingFromBeislAsync(RatingDto rating, long beislId);
        public Task<BeislDto> AddImageToBeisl(ImageDto image, long beislId);
    }
}