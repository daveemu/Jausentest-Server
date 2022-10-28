using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jausentest.Core.Models
{
    public class BeislDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public AddressDto Address { get; set; }
        public HashSet<TagDto> Tags { get; set; }
        public HashSet<RatingDto> Ratings { get; set; }
        public HashSet<ImageDto> Images { get; set; }
    }
}
