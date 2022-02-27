using Microsoft.EntityFrameworkCore;
using Shop.Data.Base;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.Services
{
    public class SingersService : EntityBaseRepository<Singer>, ISingersService
    {
        public SingersService(AppDbContext context) : base(context){ }
    }
}
