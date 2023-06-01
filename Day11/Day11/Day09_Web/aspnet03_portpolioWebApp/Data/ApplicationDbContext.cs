using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using aspnet02_boardapp.Models;
using Microsoft.EntityFrameworkCore;
using aspnet03_portpolioWebApp.Models;

namespace aspnet02_boardapp.Data
{
    public class ApplicationDbContext : IdentityDbContext // 1. ASP.NET Identity : DbContext -> IdentityContext 결국 DbContext 쓰는것하고 동일
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Board> Boards { get; set; }

        public DbSet<PortfolioModel> Portfolios { get; set; }
    }
}
