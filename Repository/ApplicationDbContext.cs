using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext<CustomUser>(options)
    {
        
        DbSet<Product> Products { get; set; }
        DbSet<FileModel> FileModels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Product>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();
            
            builder.Entity<FileModel>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();
        }
    }
}