using Infrastructure.Reponsitories.BaseReponsitory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        public ValuesController(IRepositoryWrapper repository)
        {
            _repository = repository; 
        }
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            /*var domesticAccounts = _repository.Supplier.FindByCondition(x => x.AccountType.Equals("Domestic"));*/
            var GetAll = await _repository.Supplier.GetAll();
            var GetByCondition = await _repository.Supplier.GetByCondition(x=>x.IsEnable==true);
            return new string[] { "value1", "value2" };
        }
    }
}
