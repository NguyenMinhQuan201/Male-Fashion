using AutoMapper;
using Domain.Models.Dto.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Mapper
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDetailDto, OrderDetailRequest>().ForMember(destination => destination.Id,
options => options.MapFrom(source => source.IdProduct)); ;
            CreateMap<OrderDetailRequest, OrderDetailDto>();
        }
    }
}
