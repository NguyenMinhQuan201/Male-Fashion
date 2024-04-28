using AutoMapper;
using DataDemo.Common;
using Domain.Features;
using Domain.Models.Dto.Rating;
using Infrastructure.Reponsitories.ProductReponsitories;
using Infrastructure.Reponsitories.RatingReponsitories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IMapper _mapper;
        public RatingService(IRatingRepository ratingRepository, IMapper mapper)
        {
            _mapper= mapper;
            _ratingRepository= ratingRepository;
        }

        public async Task<ApiResult<RatingDto>> Create(RatingDto request)
        {
            if (request == null)
            {
                return new ApiErrorResult<RatingDto>("Doi tuong khong ton tai");
            }
            var obj = new Infrastructure.Entities.Rating()
            {
                Id = request.Id,
                Name = request.Name,
                SDT = request.SDT,
                Stars = request.Stars,
                DateCreate = DateTime.Now,
                Des = request.Des,
                IdOrder = request.IdOrder,
            };
            await _ratingRepository.CreateAsync(obj);
            return new ApiSuccessResult<RatingDto>(request);
        }

        public async Task<ApiResult<List<RatingDto>>> GetById(int productId)
        {
            var query = await _ratingRepository.GetAllAsQueryable();
            var data = query.Where(x=>x.Id==productId).Select(x => new RatingDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    SDT = x.SDT,
                    Stars = x.Stars,
                    Des = x.Des,
                    DateCreate = x.DateCreate,
                    IdOrder= x.IdOrder,
                }).ToList();
            return new ApiSuccessResult<List<RatingDto>>(data);
        }
    }
}
