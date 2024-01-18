using DemoG01.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoG01.Entities
{
    public class CompanyContext : DbContext 
    {
        public CompanyContext() { }
        public CompanyContext(DbContextOptions options) : base(options) { }
        public DbSet<Department> Departments { get; set; }   
    }
}