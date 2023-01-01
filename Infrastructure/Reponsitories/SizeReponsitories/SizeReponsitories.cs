using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.SizeReponsitories
{
    public class SizeReponsitories : ReponsitoryBase<Size>, ISizeReponsitories
    {
        public SizeReponsitories(MaleFashionDbContext dbContext) : base(dbContext)
        {

        }
    }
}
