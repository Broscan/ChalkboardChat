namespace ChalkboardChat.Data.AppDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<ChalkboardModel> Messages { get; set; }
    }
}
