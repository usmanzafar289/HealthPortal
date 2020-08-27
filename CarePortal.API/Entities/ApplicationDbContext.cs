using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using CarePortal.Data.Models;
using CarePortal.Data.ViewModels;

namespace CarePortal.API.Entities
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>//IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Department> Department { get; set; }
        public DbSet<UserDepartment> UserDepartment { get; set; }
        public DbSet<Feed> Feed { get; set; }
        public DbSet<FeedResponse> FeedResponse { get; set; }
        public DbSet<Conversation> Conversation { get; set; }
        public DbSet<Calendar> Calendar { get; set; }
        public DbSet<Finance> Finance { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Email> Email { get; set; }

        public DbSet<FeedViewModel> FeedViewModel { get; set; }
        public DbSet<ConversationViewModel> ConversationViewModel { get; set; }
        public DbSet<CommentsViewModel> CommentsViewModel { get; set; }
        public DbSet<CalendarEventModel> CalendarEventModel { get; set; }
        public DbSet<EmailViewModel> EmailViewModel { get; set; }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);

            return new ApplicationDbContext(builder.Options);
        }
    }
}