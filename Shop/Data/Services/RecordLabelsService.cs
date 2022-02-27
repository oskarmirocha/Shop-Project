using Shop.Data.Base;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.Services
{
    public class RecordLabelsService:EntityBaseRepository<RecordLabel>, IRecordLabelsService
    {
        public RecordLabelsService(AppDbContext context) : base(context)
        {

        }
    }
}
