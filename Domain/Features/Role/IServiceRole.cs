﻿using Domain.Models.Dto.RoleDto;

namespace Domain.Features.Role
{
    public interface IServiceRole
    {
        public Task<bool> Register(RoleRequestDto request);
        public Task<bool> Remove(RemoveRoleRequestDto request);
        public Task<List<RoleVmDto>> GetAllRole();

    }
}
