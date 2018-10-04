using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cleverbit.RegionsEmployeesPart2.Models
{
    public class RegionModel
    {
        public int RegionId { get; set; }
        public string Name { get; set; }
        public IEnumerable<EmployeeModel> Employees { get; set; }
    }
}