namespace Rigor.HAR.API.Data
{
    using Microsoft.EntityFrameworkCore;
    using Rigor.HAR.API.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        public DbSet<HarFile> HarFiles { get; set; }
    }
}
