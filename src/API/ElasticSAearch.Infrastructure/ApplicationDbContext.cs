using Elastic.Clients.Elasticsearch;
using ElasticSAearch.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElasticSAearch.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }

    public DbSet<RoleEntity> Roles { get; set; }

    public DbSet<UserPasswordEntity> UserPasswords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.SetModelCreation();
    }
}
