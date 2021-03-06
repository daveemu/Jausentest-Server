using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jausentest.Domain.Entities
{
    public class BeislEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public AddressEntity Address { get; set; }
        public double Rating { get; set; }
        public HashSet<TagEntity> Tags { get; set; }

    }
}
