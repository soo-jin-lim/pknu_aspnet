using Microsoft.EntityFrameworkCore;

namespace TodoApiServer.Models
{
    public class ApplicationDbContext : DbContext // ApplicationDbContext는 DbContext를 상속
    {
        // 생성자 마법사로 만들것!
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
