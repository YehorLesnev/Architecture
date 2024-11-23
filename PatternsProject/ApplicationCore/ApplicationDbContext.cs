using ApplicationCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore;

public class ApplicationDbContext : IdentityDbContext<UserModel, IdentityRole<Guid>, Guid>
{
    public DbSet<RequestModel> Requests { get; set; }
    public DbSet<CommentModel> Comments { get; set; }
    public DbSet<FileModel> Files { get; set; }
    public DbSet<NotificationModel> Notifications { get; set; }
    public DbSet<BalanceModel> Balances { get; set; }

    public ApplicationDbContext() : base() { }
	
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<UserModel>(entity =>
	    {
		  entity.Property(u => u.ProfilePicture)
				.HasColumnType("varbinary(max)");
	    });

		// User Relationships
		modelBuilder.Entity<UserModel>()
            .HasOne(u => u.Manager)
            .WithMany(m => m.Subordinates)
            .HasForeignKey(u => u.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Request Relationships
        modelBuilder.Entity<RequestModel>()
            .HasOne(r => r.User)
            .WithMany(u => u.Requests)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RequestModel>()
            .HasOne(r => r.Manager)
            .WithMany()
            .HasForeignKey(r => r.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Comment Relationships
        modelBuilder.Entity<CommentModel>()
            .HasOne(c => c.Request)
            .WithMany(r => r.Comments)
            .HasForeignKey(c => c.RequestId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CommentModel>()
            .HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // File Relationships
        modelBuilder.Entity<FileModel>()
            .HasOne(f => f.Request)
            .WithMany(r => r.Files)
            .HasForeignKey(f => f.RequestId)
            .OnDelete(DeleteBehavior.Cascade);

        // Notification Relationships
        modelBuilder.Entity<NotificationModel>()
            .HasOne(n => n.User)
            .WithMany(u => u.Notifications)
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<NotificationModel>()
            .HasOne(n => n.Sender)
            .WithMany()
            .HasForeignKey(n => n.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        // Balance Relationships
        modelBuilder.Entity<BalanceModel>()
            .HasOne(b => b.User)
            .WithMany()
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
