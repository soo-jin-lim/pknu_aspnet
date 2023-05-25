using aspnet02_boardapp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace aspnet02_boardapp.Data
{
    public class ApplicationDbContext : IdentityDbContext // 1. ASP.NET Identity : DbContext -> Identity 결국 DbContext 쓰는 것 하고 동일
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Board> Boards { get; set; }
    }
}
