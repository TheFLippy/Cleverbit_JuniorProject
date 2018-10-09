using System.Data.Entity;

namespace Cleverbit.RegionsEmployeesPart2.Models
{
    public class RegionsEmployeesContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Region> Regions { get; set; }

        public RegionsEmployeesContext() : base("Cleverbit.RegionsEmployeesDB")
        {

        }
    }
}