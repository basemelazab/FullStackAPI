using FullStackAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStackAPI.Data
{
    public class FullStackDBContext : DbContext
    {
        public FullStackDBContext(DbContextOptions<FullStackDBContext> options) : base(options)
        {
        }
        public DbSet<Employees> Employees { get; set; }
    }
}
