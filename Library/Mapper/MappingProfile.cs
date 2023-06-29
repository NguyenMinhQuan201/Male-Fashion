using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Mapper
{
    public class MappingProfile<T,L> : Profile
    {
        public MappingProfile()
        {
            CreateMap<T, L>();
            CreateMap<L, T>();
        }
    }
}
