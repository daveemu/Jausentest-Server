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
        public string Address { get; set; }
        public List<TagEntity> Tags { get; set; }
        public double Rating { get; set; }
        public int JustTesting { get; set; }


    }
}
