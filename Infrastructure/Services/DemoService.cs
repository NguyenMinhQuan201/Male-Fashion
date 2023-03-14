using Domain.IServices.User;
using Domain.Modles.Demo;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.User
{
    public class DemoService
    {
        private readonly MaleFashionDbContext _dbContext;

        public DemoService(MaleFashionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /*public Task<bool> Create(DemoCreateRequest request)
        {

            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetById(int id)
        {
            try
            {
                var checkDemoExit = await _dbContext.Demos.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (checkDemoExit == null)
                {
                    return 0;
                }
                else
                {
                    return checkDemoExit.Id;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return 0;

        }

        public Task<bool> Update(int id, DemoUpdateRequest request)
        {
            throw new NotImplementedException();
        }*/
    }
}
