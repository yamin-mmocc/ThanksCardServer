using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThanksCardServer.Model;

namespace ThanksCardServer.Model
{
    public class ApplicationContext : DbContext //YME created
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<LogReceives> LogReceives { get; set; }
        public DbSet<LogSends> LogSends { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<ThanksCardServer.Model.Cards> Cards { get; set; }
    }
}