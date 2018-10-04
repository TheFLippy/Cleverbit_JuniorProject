using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cleverbit.RegionsEmployeesPart2.Models
{
    public class RegionsEmployeesContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Region> Regions { get; set; }

        public RegionsEmployeesContext() : base("Cleverbit.RegionsEmployeesDB")
        {

        }

        //
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}