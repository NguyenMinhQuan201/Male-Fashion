﻿using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.BaseReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.ImportInvoiceDetailsReponsitories
{
    public class ImportInvoiceDetailsReponsitories : ReponsitoryBase<ImportInvoiceDetails>, IImportInvoiceDetailsReponsitories
    {
        public ImportInvoiceDetailsReponsitories(MaleFashionDbContext dbContext) : base(dbContext)
        {

        }
    }
}
