using AutoMapper;
using DataDemo.Common;
using Domain.Common;
using Domain.Features;
using Domain.Models.Dto.UserDto;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.ModuleReponsitories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ModuleService : IModuleService
    {
        private readonly IModuleRepository _ModuleRepository;
        private readonly IMapper _mapper;

        public ModuleService(IModuleRepository ModuleRepository, IMapper mapper)
        {
            _ModuleRepository = ModuleRepository;
            _mapper = mapper;
        }
        public async Task<ModuleDto> Create(ModuleDto request)
        {
            var obj = _mapper.Map<Module>(request);
            await _ModuleRepository.CreateAsync(obj);
            request.Id = obj.Id;
            return request;
        }

        public async Task<bool> Delete(int id)
        {
            var obj = await _ModuleRepository.GetById(id);
            if(obj == null)
            {
                return false;
            }
            await _ModuleRepository.DeleteAsync(obj);
            return true;
        }

        public async Task<ApiResult<PagedResult<ModuleDto>>> GetAll(int? pageSize, int? pageIndex, string search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            Expression<Func<Infrastructure.Entities.Module, bool>> expression = x => x.IsShow == true;
            var totalRow = await _ModuleRepository.CountAsync(expression);
            var query = await _ModuleRepository.GetAll(pageSize, pageIndex, expression);
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Infrastructure.Entities.Module, bool>> expression2 = x => x.Name.Contains(search) && x.IsShow == true;
                query = await _ModuleRepository.GetAll(pageSize, pageIndex, expression2);
                totalRow = await _ModuleRepository.CountAsync(expression2);
            }
            //Paging
            var data = _mapper.Map<List<ModuleDto>>(query.ToList());
            var pagedResult = new PagedResult<ModuleDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data,
                Status = true
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<ModuleDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<ModuleDto>>(pagedResult);
        }

        public async Task<ModuleDto> GetById(int id)
        {
            var obj = await _ModuleRepository.GetById(id);
            var map = _mapper.Map<ModuleDto>(obj);
            return map;
        }

        public async Task<ApiResult<PagedResult<ModuleDto>>> GetDeletedModule(int? pageSize, int? pageIndex, string search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            Expression<Func<Infrastructure.Entities.Module, bool>> expression = x => x.IsShow == false;
            var totalRow = await _ModuleRepository.CountAsync(expression);
            var query = await _ModuleRepository.GetAll(pageSize, pageIndex, expression);
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Infrastructure.Entities.Module, bool>> expression2 = x => x.Name.Contains(search) && x.IsShow == false;
                query = await _ModuleRepository.GetAll(pageSize, pageIndex, expression2);
                totalRow = await _ModuleRepository.CountAsync(expression2);
            }
            //Paging
            var data = _mapper.Map<List<ModuleDto>>(query.ToList());
            var pagedResult = new PagedResult<ModuleDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data,
                Status = true
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<ModuleDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<ModuleDto>>(pagedResult);
        }

        public async Task<ModuleDto> Update(int id, ModuleDto request)
        {
            var map = _mapper.Map<Module>(request);
            map.Id = id;
            await _ModuleRepository.UpdateAsync(map);
            return request;
        }
    }
}
