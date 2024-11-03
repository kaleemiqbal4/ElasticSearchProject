using ElasticSAearch.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElasticSAearch.Infrastructure;

public static class EntityExtender
{
    public static void SetModelCreation(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>()
        .HasIndex(e => e.Email)
        .IsUnique();
        modelBuilder.Entity<UserEntity>()
            .HasIndex(e => e.Contact)
            .IsUnique();

    }
}
