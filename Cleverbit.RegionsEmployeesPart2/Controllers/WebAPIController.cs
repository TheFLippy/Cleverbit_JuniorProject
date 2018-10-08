using Cleverbit.RegionsEmployeesPart2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Cleverbit.RegionsEmployeesPart2.Controllers
{
    public class WebAPIController : ApiController
    {
        public RegionModel Get(int id)
        {
            using (var context = new RegionsEmployeesContext())
            {
                var query = context.Regions
                    .Where(r => r.RegionId == id)
                    .Select(r => new RegionModel()
                    {
                        RegionId = r.RegionId,
                        Name = r.Name,
                        Employees = r.RegionEmployees
                            .Select(e => new EmployeeModel()
                            {
                                Name = e.Name,
                                Surname = e.Surname
                            })
                    });


                return query.FirstOrDefault();
            }
        }

        public IEnumerable<RegionModel> Get()
        {
            using (var context = new RegionsEmployeesContext())
            {
                var query = context.Regions
                    .Where(r => r.ParentRegionId == -1)
                    .OrderBy(r => r.Name)
                    .Select(r => new RegionModel()
                    {
                        RegionId = r.RegionId,
                        Name = r.Name,
                        Employees = r.RegionEmployees
                        .Select(e => new EmployeeModel()
                        {
                            Name = e.Name,
                            Surname = e.Surname
                        })
                    });
                return query.ToList();
            }
        }
    }
}
