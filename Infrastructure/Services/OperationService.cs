using AutoMapper;
using DataDemo.Common;
using Domain.Common;
using Domain.Features;
using Domain.Models.Dto.UserDto;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.OperationReponsitories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class OperationService : IOperationService
    {
        private readonly IOperationRepository _operationRepository;
        private readonly IMapper _mapper;

        public OperationService(IOperationRepository operationRepository, IMapper mapper)
        {
            _operationRepository = operationRepository;
            _mapper = mapper;
        }
        public async Task<OperationDto> Create(OperationDto request)
        {
            var obj = new Operation()
            {
                Code = request.Code,
                Icon = request.Icon,
                IsShow = request.IsShow,
                Name = request.Name,
                Url = request.Url,
                ModuleId= request.ModuleId,
            };
            await _operationRepository.CreateAsync(obj);
            request.Id = obj.Id;
            return request;
        }

        public async Task<bool> Delete(int id)
        {
            var obj = await _operationRepository.GetById(id);
            if(obj == null)
            {
                return false;
            }
            await _operationRepository.DeleteAsync(obj);
            return true;
        }

        public async Task<ApiResult<PagedResult<OperationDto>>> GetAll(int? pageSize, int? pageIndex, string search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            Expression<Func<Infrastructure.Entities.Operation, bool>> expression = x =>x.Code!=null;
            var totalRow = await _operationRepository.CountAsync(expression);
            var query = await _operationRepository.GetAll(pageSize, pageIndex, expression);
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Infrastructure.Entities.Operation, bool>> expression2 = x => x.Name.Contains(search);
                query = await _operationRepository.GetAll(pageSize, pageIndex, expression2);
                totalRow = await _operationRepository.CountAsync(expression2);
            }
            //Paging
            var data = _mapper.Map<List<OperationDto>>(query.ToList());
            var pagedResult = new PagedResult<OperationDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data,
                Status = true
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<OperationDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<OperationDto>>(pagedResult);
        }

        public async Task<OperationDto> GetById(int id)
        {
            var obj = await _operationRepository.GetById(id);
            var map = _mapper.Map<OperationDto>(obj);
            return map;
        }

        public async Task<ApiResult<PagedResult<OperationDto>>> GetDeletedOperation(int? pageSize, int? pageIndex, string search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            Expression<Func<Infrastructure.Entities.Operation, bool>> expression = x => x.Code!=null;
            var totalRow = await _operationRepository.CountAsync(expression);
            var query = await _operationRepository.GetAll(pageSize, pageIndex, expression);
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Infrastructure.Entities.Operation, bool>> expression2 = x => x.Name.Contains(search) && x.Code != null;
                query = await _operationRepository.GetAll(pageSize, pageIndex, expression2);
                totalRow = await _operationRepository.CountAsync(expression2);
            }
            //Paging
            var data = _mapper.Map<List<OperationDto>>(query.ToList());
            var pagedResult = new PagedResult<OperationDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data,
                Status = true
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<OperationDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<OperationDto>>(pagedResult);
        }

        public async Task<OperationDto> Update(int id, OperationDto request)
        {
            var map = _mapper.Map<Operation>(request);
            map.Id = id;
            await _operationRepository.UpdateAsync(map);
            return request;
        }
    }
}
