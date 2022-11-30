using Infrastructure.EF;
using Infrastructure.Reponsitories.SupplierReponsitories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.BaseReponsitory
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private MaleFashionDbContext _dbContext;
        private ISupplierReponsitories _supplierReponsitories;
        public RepositoryWrapper(MaleFashionDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ISupplierReponsitories Supplier
        {
            get
            {
                if (_supplierReponsitories == null)
                {
                    _supplierReponsitories = new Infrastructure.Reponsitories.SupplierReponsitories.SupplierReponsitories(_dbContext);
                }
                return _supplierReponsitories;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
