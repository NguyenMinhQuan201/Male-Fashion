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
    public class ImportInvoiceReponsitories : ReponsitoryBase<ImportInvoice>, IImportInvoiceReponsitories
    {
        public ImportInvoiceReponsitories(MaleFashionDbContext dbContext) : base(dbContext)
        {

        }
    }
}
