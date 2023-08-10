using AutoMapper;
using Domain.Models.Dto.Product;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            //CreateMap<GetProductDto, Product>();
            //CreateMap<Product, GetProductDto>();
        }
    }
}
