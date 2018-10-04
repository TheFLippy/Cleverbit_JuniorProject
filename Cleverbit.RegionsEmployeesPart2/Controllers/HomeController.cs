using Cleverbit.RegionsEmployeesPart2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Data.Entity;

namespace Cleverbit.RegionsEmployeesPart2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Controller that handles the search request passed through a text box on the index page
        /// </summary>
        /// <param name="regionId">ID to search by</param>
        /// <returns>Returns a partial view for the Ajax method to display without posting</returns>
        public PartialViewResult SearchRegion(int regionId)
        {
            //this.Server.MapPath(

            using (var context = new RegionsEmployeesContext())
            {
                //Search Region table with region ID
                var query = context.Regions.FirstOrDefault(r => r.RegionId == regionId);

                //If result is null, return nothing
                if (query == null)
                {
                    return default(PartialViewResult);
                }

                //If region exists, instruct EF to explicitly load employees in that region
                var employeeList = GetEmployees(query);

                return PartialView(employeeList);
            }
        }
        /// <summary>
        /// Recursive function that takes a region that an employee belongs to, recurses from their region up to the parent 
        /// region and builds a path string
        /// </summary>
        /// <param name="currentRegion">Region object to keep track of current node</param>
        /// <param name="returnString">String builder object that keeps appending region names untill it hits an exit statement</param>
        /// <returns>Returns full path string from child region up to the parent region</returns>
        public string RecursiveParsingOfRegions(Region currentRegion, StringBuilder returnString)
        {
            //IF -1 means we hit root, EXIT STATEMENT
            if(currentRegion.ParentRegionId == -1)
            {
                //Append current regions name to return string
                returnString.Append($"{currentRegion.Name}.");
                return returnString.ToString();
            }
            //Else recurse by passing the parent region to the function
            else
            {
                //Append current regions name to return string
                returnString.Append($"{currentRegion.Name}, ");
                //Get parent region object
                var parent = GetRegion(currentRegion.ParentRegionId);
                //Recurse
                return RecursiveParsingOfRegions(parent, returnString);
            }
        }
        /// <summary>
        /// Gets the specified region out of DB
        /// </summary>
        /// <param name="regionId">ID of region to retrieve</param>
        /// <returns>Returns the specified Region object</returns>
        public static Region GetRegion(int regionId)
        {
            using (var context = new RegionsEmployeesContext())
            {
                var query = from r in context.Regions
                            where r.RegionId == regionId
                            select r;
                var region = query.First();

                return region;
            }
        }
        /// <summary>
        /// Function which extracts all employees from related regions
        /// </summary>
        /// <param name="parentRegion">Region from which the function has to start with</param>
        /// <returns>Returns a list of all employees related to a region</returns>
        public static List<Employee> GetEmployees(Region parentRegion)
        {
            //Create an empty list, which we will populate and return at the end
            var returnEmployeeList = new List<Employee>();
            //Create a queue for regions (used so we can visit each region once)
            var regionQueue = new Queue<Region>();
            //Kick start the while loop by adding the parent region to queue
            regionQueue.Enqueue(parentRegion);

            using (var context = new RegionsEmployeesContext())
            {
                //While queue is not empty, repeat loop
                while (regionQueue.Count != 0)
                {
                    //Take the first region out of queue
                    var currentRegion = (Region)regionQueue.Dequeue();
                    //Add all employees of current region to the list
                    returnEmployeeList.AddRange(currentRegion.RegionEmployees);

                    //Check if current region has any children
                    var query = context.Regions
                        .Include(d => d.RegionEmployees)
                        .Where(r => r.ParentRegionId == currentRegion.RegionId);

                    foreach (var region in query)
                    {
                        //If it has children, add them to queue
                        regionQueue.Enqueue(region);
                    }
                }
            }

            //When the queue is emptied, return a populated list of all employees
            return returnEmployeeList;
        }

    }
}