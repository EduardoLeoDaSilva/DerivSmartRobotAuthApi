using AuthControl.Entities;
using AuthControl.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AuthControl
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserBase> User { get; set; }
        public DbSet<RetryQueue> RetryQueues { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserBase>(x =>
            {
                x.ToTable("users");
                x.HasKey(m => m.Id);
                x.Property(m => m.Email).IsRequired(true).HasMaxLength(100);

                    x.HasIndex(u => u.Email)
                    .IsUnique();
                x.Property(m => m.Password).IsRequired(true).HasMaxLength(100);
                x.Property(m => m.TokensOAuth).IsRequired(false).HasMaxLength(100);




                x.Property(m => m.Robots);
            });

            modelBuilder.Entity<RetryQueue>(x =>
            {
                x.ToTable("retries");
                x.HasKey(m => m.Id);
                x.HasIndex(u => u.Email)
                    .IsUnique();
                x.Property(m => m.Email).IsRequired(true).HasMaxLength(100);


                x.Property(m => m.Password).IsRequired(true);
            });


            modelBuilder.Entity<Plans>(x =>
            {
                x.ToTable("plans");
                x.HasKey(m => m.Id);
                x.Property(m => m.Name).IsRequired(true).HasMaxLength(100);
                x.Property(s => s.Robots)
                    .HasConversion(v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                        v => JsonConvert.DeserializeObject<List<string>>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }))
                    .HasColumnName("Robots");
            });
        }
    }
}
