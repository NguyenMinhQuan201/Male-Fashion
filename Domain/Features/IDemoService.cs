using Domain.Modles.Demo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features
{
    public interface IDemoService
    {
        public Task<bool> Create(DemoCreateRequest request);
        public Task<bool> Update(int id, DemoUpdateRequest request);
        public Task<bool> Delete(int id);
        public Task<int> GetById(int id);
    }
}
