﻿using System;
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
        public string Address { get; set; }
        public List<TagDto> Tags { get; set; }
        public double Rating { get; set; }


    }
}