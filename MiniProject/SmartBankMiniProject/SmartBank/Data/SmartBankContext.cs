using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartBank.Models;

namespace SmartBank.Data
{
    public class SmartBankContext : DbContext
    {
        public SmartBankContext (DbContextOptions<SmartBankContext> options)
            : base(options)
        {
        }

        public DbSet<SmartBank.Models.Account> Account { get; set; } = default!;
    }
}
