using AutoMapper;
using Jausentest.Core.Models;
using Jausentest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jausentest.Core.Config
{
    public class AutomapperConfig : Profile
    {

        public AutomapperConfig()
        {
            CreateMap<BeislEntity, BeislDto>();
            CreateMap<TagEntity, TagDto>();
            CreateMap<BeislDto, BeislEntity>();
            CreateMap<TagDto, TagEntity>();
        }

    }
}
