﻿using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.CategoryReponsitories
{
    public class ModuleRepository : RepositoryBase<Category>, IModuleRepository
    {
        public ModuleRepository(MaleFashionDbContext dbContext) : base(dbContext)
        {

        }
    }
}
