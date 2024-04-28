using DataDemo.Common;
using Domain.Models.Dto.Rating;
using Domain.Models.Dto.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features
{
    public interface IRatingService
    {
        public Task<ApiResult<RatingDto>> Create(RatingDto request);
        public Task<ApiResult<List<RatingDto>>> GetById(int productId);
    }
}
