using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.ImportInvoiceReponsitories
{
    public class ImportInvoiceRepository : RepositoryBase<ImportInvoice>, IImportInvoiceRepository
    {
        public ImportInvoiceRepository(MaleFashionDbContext dbContext) : base(dbContext)
        {

        }
    }
}
