using GalloTube.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GalloTube.Data;
public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Video> Video { get; set; }
    public DbSet<VideoTag> VideoTag { get; set; } 
    public DbSet<Tag> Tag { get; set; }
    protected override void OnModelCreating(ModelBuilder builder) //void não tem retorno
    {
        base.OnModelCreating(builder);
        AppDbSeed appDbSeed = new(builder);

        //FluentAPI
        #region Personalização do Identity
        builder.Entity<IdentityUser>(b => {
            b.ToTable("Users");
        });
        builder.Entity<IdentityUserClaim<string>>(b => {
            b.ToTable("UserClaims");
        });
        builder.Entity<IdentityUserLogin<string>>(b => {
            b.ToTable("UserLogins");
        });
        builder.Entity<IdentityUserToken<string>>(b => {
            b.ToTable("UserTokens");
        });
        builder.Entity<IdentityRole>(b => {
            b.ToTable("Roles");
        });
        builder.Entity<IdentityRoleClaim<string>>(b => {
            b.ToTable("RoleClaims");
        });
        builder.Entity<IdentityUserRole<string>>(b => {
            b.ToTable("UserRoles");
        });
        #endregion

        #region Many To Many - VideoTag
        // Definição de Chave Primária Composta
        builder.Entity<VideoTag>().HasKey(
            mg => new { mg.VideoId, mg.TagId }
        );

        builder.Entity<VideoTag>()
            .HasOne(mg => mg.Video)
            .WithMany(m => m.Tags)
            .HasForeignKey(mg => mg.VideoId);

        builder.Entity<VideoTag>()
            .HasOne(mg => mg.Tag)
            .WithMany(g => g.Videos)
            .HasForeignKey(mg => mg.TagId);
        #endregion

    
    }
}