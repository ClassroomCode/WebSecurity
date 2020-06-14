using EComm.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComm.DataAccess
{
    public class EFRepository : DbContext
    {
        public EFRepository(DbContextOptions options)
          : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
