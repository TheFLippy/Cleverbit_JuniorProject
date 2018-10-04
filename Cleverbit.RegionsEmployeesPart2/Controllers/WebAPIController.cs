using Cleverbit.RegionsEmployeesPart2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cleverbit.RegionsEmployeesPart2.Controllers
{
    public class WebAPIController : ApiController
    {
        //public IEnumerable<RegionModel> Get(int parentId)
        //{
        //    using(var context = new RegionsEmployeesContext())
        //    {
        //        //var query = context.Regions.Where(r => r.ParentRegionId == -1).AsNoTracking().OrderBy(r => r.Name).Include(e => e.RegionEmployees).ToList();
        //        var query = context.Regions
        //            .Where(r => r.ParentRegionId == parentId)
        //            .OrderBy(r => r.Name)
        //            //.ToList()
        //            .Select(r => new RegionModel()
        //            {
        //                RegionId = r.RegionId,
        //                Name = r.Name,
        //                Employees = r.RegionEmployees
        //                .Select(e => new EmployeeModel()
        //                {
        //                    Name = e.Name,
        //                    Surname = e.Surname
        //                })
        //            });
        //        return query.ToList();
        //    }         
        //}

        public Region Get(int Id)
        {
            using (var context = new RegionsEmployeesContext())
            {
                //var query = context.Regions.Where(r => r.ParentRegionId == -1).AsNoTracking().OrderBy(r => r.Name).Include(e => e.RegionEmployees).ToList();
                var query = context.Regions
                    .Include(e => e.RegionEmployees)
                    .FirstOrDefault(r => r.RegionId == Id);
                return query;
            }
        }

        public IEnumerable<RegionModel> Get()
        {
            using (var context = new RegionsEmployeesContext())
            {
                //var query = context.Regions.Where(r => r.ParentRegionId == -1).AsNoTracking().OrderBy(r => r.Name).Include(e => e.RegionEmployees).ToList();
                var query = context.Regions
                    .Where(r => r.ParentRegionId == -1)
                    .OrderBy(r => r.Name)
                    //.ToList()
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
