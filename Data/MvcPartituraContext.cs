using Microsoft.EntityFrameworkCore;
using Partituras.Models;

namespace Partituras.Data
{
    public class MvcPartituraContext : DbContext
    {
        public MvcPartituraContext (DbContextOptions<MvcPartituraContext> options)
            : base(options)
        {
        }

        public DbSet<Partitura> Partitura { get; set; }
    }
}