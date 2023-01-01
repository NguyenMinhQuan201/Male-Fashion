using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.ColorReponsitories
{
    public class ColorReponsitories : ReponsitoryBase<Color>, IColorReponsitories
    {
        public ColorReponsitories(MaleFashionDbContext dbContext) : base(dbContext)
        {

        }
    }
}
