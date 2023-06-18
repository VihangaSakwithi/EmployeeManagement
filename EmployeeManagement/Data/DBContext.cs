using EmployeeManagement.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
