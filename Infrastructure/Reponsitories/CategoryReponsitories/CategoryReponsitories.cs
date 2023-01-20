using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.CategoryReponsitories
{
    public class CategoryReponsitories : ReponsitoryBase<Category>, ICategoryReponsitories
    {
        public CategoryReponsitories(MaleFashionDbContext dbContext) : base(dbContext)
        {

        }
    }
}
