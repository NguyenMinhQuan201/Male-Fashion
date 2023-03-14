using AutoMapper;
using Domain.Models.Dto.Product;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mapper
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<ImageDto, ProductImg>();
            CreateMap<ProductImg, ImageDto>();
        }
    }
}
