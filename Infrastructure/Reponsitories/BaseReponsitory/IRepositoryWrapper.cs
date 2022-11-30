using Infrastructure.Reponsitories.SupplierReponsitories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.BaseReponsitory
{
    public interface IRepositoryWrapper
    {
        ISupplierReponsitories Supplier { get; }
        void Save();
    }
}
