using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Connection;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {
    }

    public DbSet<UserModel>? Users { get; set; }
    public DbSet<ContactModel>? Contacts { get; set; }
    public DbSet<ContentModel>? Contents { get; set; }
    public DbSet<ImageLibraryModel>? Images { get; set; }
}