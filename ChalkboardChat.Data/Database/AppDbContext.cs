using ChalkboardChat.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace ChalkboardChat.Data.Database;

public class AppDbContext : DbContext
{

    public DbSet<MessageModel> Message { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }




}
