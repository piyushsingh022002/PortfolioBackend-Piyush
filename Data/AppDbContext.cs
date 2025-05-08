using backend.Models;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data{
    public class AppDbContext:DbContext{
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}
        public DbSet<QueryForm> Queries{get;set;}
        public DbSet<ContactForm> Contacts{get;set;}
        public DbSet<FeedbackForm> Feedbacks{get;set;}
        public DbSet<Login> Logins { get; set; }
    }
}