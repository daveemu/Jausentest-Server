using Jausentest.Domain.Entities;
using Jausentest.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<BeislEntity> AddBeislAsync(BeislEntity beisl)
        {
            beisl.Id = 0;
            beisl.Tags = null;
            var addedBeisl = (_jausentestContext.Beisl.Add(beisl)).Entity;
            await _jausentestContext.SaveChangesAsync();
            return addedBeisl;
        }

        public async Task<BeislEntity> AddOrUpdateAsync(BeislEntity beisl)
        {


            //check if beisl is already in database
            var existingBeisl = await _jausentestContext.Beisl
                .Include(b => b.Tags)
                .Include(b => b.Address)
                .FirstOrDefaultAsync(_b => _b.Id == beisl.Id);

            
            if(existingBeisl == null) //beisl does not exist in db
            {
                //if we add a new beisl, we have to check the appended tags
                foreach (var tag in beisl.Tags)
                {
                    if (!_jausentestContext.Tags.AsNoTracking().Any(t => t.Name == tag.Name))
                    {
                        _jausentestContext.Add(tag);
                    }
                    else
                    {
                        _jausentestContext.Update(tag);
                    }

                }
                //after tags are save and cozy in our db, add the beisl
                _jausentestContext.Add(beisl);
            }
            else // beisl already in db
            {
                //update values of our existingBeisl
                _jausentestContext.Entry(existingBeisl).CurrentValues.SetValues(beisl);
                _jausentestContext.Entry(existingBeisl.Address).CurrentValues.SetValues(beisl.Address);
                
                //we have to check each appended tag if already in database
                foreach (var tag in beisl.Tags)
                {
                    //check if submitted tag is already appended to existingBeisl
                    var existingTag = existingBeisl.Tags.AsQueryable().FirstOrDefault(t => t.Name == tag.Name);

                    //if tag is new for the existingBeisl
                    if(existingTag == null)
                    {
                        //check if tag is already in db
                        var existingTagInDb = _jausentestContext.Tags.Find(tag.Name);
                        if(existingTagInDb == null)
                        {
                            //if new add to database
                            existingBeisl.Tags.Add(tag);
                        } 
                        else
                        {
                            //if already exists, only add reference to existingBeisl
                            _jausentestContext.Beisl.Find(existingBeisl.Id).Tags.Add(_jausentestContext.Tags.Find(tag.Name));
                        }   
                    }
                    
                }

                //all tags which are in existingBeisl, but not in submitted beisl, will be deleted
                foreach (var tag in existingBeisl.Tags.ToList())
                {
                    if (!beisl.Tags.Any(t => t.Name == tag.Name))
                    {
                        existingBeisl.Tags.Remove(tag);
                    }
                }
                
                beisl = existingBeisl;

            }

            await _jausentestContext.SaveChangesAsync();

            return beisl;
        }


        
        public async Task<BeislEntity> AddTagToBeislAsync(TagEntity tag, long beislId)
        {
            var _beisl = await _jausentestContext
                        .Beisl
                        .Include(b => b.Tags)
                        .FirstOrDefaultAsync(b => b.Id == beislId);

            if (_beisl == null)
                return null;

            if(!_beisl.Tags.Contains(tag))
            {
                var _tag = await _jausentestContext.Tags.FirstOrDefaultAsync(t => t.Name == tag.Name);
                
                if(_tag == null)
                {
                    _tag = (await _jausentestContext.AddAsync(tag)).Entity;
                } 

                _beisl.Tags.Add(_tag);
                await _jausentestContext.SaveChangesAsync();
            } 

            return _beisl;
        }

        public async Task<BeislEntity> DeleteTagFromBeislAsync(TagEntity tag, long beislId)
        {
            var _beisl = await _jausentestContext
                        .Beisl
                        .Include(b => b.Tags)
                        .FirstOrDefaultAsync(b => b.Id == beislId);

            if (_beisl == null)
                return null;

            var _tag = await _jausentestContext
                       .Tags
                       .FirstOrDefaultAsync(t => t.Name == tag.Name);

            if(_beisl.Tags.Contains(_tag))
            {
                _beisl.Tags.Remove(_tag);
                await _jausentestContext.SaveChangesAsync();
            }

            return _beisl;
        }

        public async Task<BeislEntity> AddImageToBeisl(ImageEntity image, long beislId)
        {
            var _beisl = await _jausentestContext.Beisl
                .Include(b => b.Images)
                .FirstOrDefaultAsync(b => b.Id == beislId);

            if (_beisl != null)
            {
                _beisl.Images.Add(image);
                await _jausentestContext.SaveChangesAsync();
            }

            return _beisl;  
        }

        public async Task<IEnumerable<BeislEntity>> GetAllAsync()
        {
            return await _jausentestContext.Beisl
                .Include(b => b.Tags)
                .Include(b => b.Ratings)
                .Include(b => b.Images)
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<BeislEntity> GetBeislByIdAsync(long beislId)
        {
            return await _jausentestContext.Beisl.FindAsync(beislId);
        }

        public async Task<IEnumerable<TagEntity>> GetTagsForBeislIdAsync(long beislId)
        {
            return (await _jausentestContext.Beisl
                .Include(b => b.Tags)
                .FirstOrDefaultAsync(_b => _b.Id == beislId))
                .Tags;
        }

        public async Task<IEnumerable<RatingEntity>> GetRatingsForBeislIdAsync(long beislId)
        {
            return (await _jausentestContext.Beisl
                    .Include(b => b.Ratings)
                    .FirstOrDefaultAsync(_b => _b.Id == beislId))
                .Ratings;
        }

        public async Task<BeislEntity> AddRatingToBeislAsync(RatingEntity rating, long beislId)
        {
            var _beisl = await _jausentestContext.Beisl
                .Include(b => b.Ratings)
                .FirstOrDefaultAsync(b => b.Id == beislId);

            if (_beisl == null)
                return null;
            
            _beisl.Ratings.Add(rating);
            await _jausentestContext.SaveChangesAsync();

            return _beisl;
        }
        
        public async Task<BeislEntity> DeleteRatingFromBeislAsync(RatingEntity rating, long beislId)
        {
            var _beisl = await _jausentestContext
                .Beisl
                .Include(b => b.Ratings)
                .FirstOrDefaultAsync(b => b.Id == beislId);

            if (_beisl == null)
                return null;

            var _rating = _beisl.Ratings.Where(r => r.Equals(rating));
            
            if(_rating.Any())
            {
                _beisl.Ratings.Remove(_rating.First());
                await _jausentestContext.SaveChangesAsync();
            }

            return _beisl;
        }
    }
}
