using Cleverbit.RegionsEmployeesPart2.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Cleverbit.RegionsEmployeesPart2.Models
{
    [DebuggerDisplay("Region: {Name}, ID: {RegionId}, Parent ID: {ParentRegionId}, Employees: {RegionEmployees.Count}")]
    public class Region : IModificationHistory
    {
        public int Id { get; set; }
        public int RegionId { get; set; }
        public string Name { get; set; }
        public int ParentRegionId { get; set; }
        public virtual ICollection<Employee> RegionEmployees { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool IsDirty { get; set; }

        public Region()
        {
            RegionEmployees = new List<Employee>();

            if(DateCreated == DateTime.MinValue)
            {
                DateCreated = DateTime.Now;
                DateModified = DateTime.Now;
            }
            else
            {
                DateModified = DateTime.Now;
            }
        }
    }
}
