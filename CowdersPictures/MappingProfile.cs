using AutoMapper;
using CowdersPictures.Dtos;
using CowdersPictures.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CowdersPictures
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<IssueEntity, Issue>();
            CreateMap<Issue, IssueEntity>();
            CreateMap<Sensor, SensorEntity>();
            CreateMap<SensorEntity, Sensor>();
        }
    }
}
