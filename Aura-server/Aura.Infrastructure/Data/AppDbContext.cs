using Aura.Domain.Entities;
using Aura.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Aura.Infrastructure.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new LikeConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new FavoriteConfiguration());
        modelBuilder.ApplyConfiguration(new ReportConfiguration());
        modelBuilder.ApplyConfiguration(new PrivateMessageConfiguration());
        modelBuilder.ApplyConfiguration(new FollowConfiguration());


        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Follow> Follows { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<Repost> Reposts { get; set; }
    public DbSet<Story> Stories { get; set; }
    public DbSet<PrivateMessage> PrivateMessages { get; set; }
    public DbSet<Image> Images { get; set; }
}
