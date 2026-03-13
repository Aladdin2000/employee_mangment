using System.Data.Entity;

namespace employees_mangment.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<employees> Employees { get; set; }
        public DbSet<PropertyDefinition> PropertyDefinitions { get; set; }
        public DbSet<EmployeePropertyValue> EmployeePropertyValues { get; set; }
    }
}