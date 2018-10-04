using Cleverbit.RegionsEmployeesPart2.Models;
using Microsoft.VisualBasic.FileIO;
using System.Linq;
using System;
using System.Web.Hosting;

namespace Cleverbit.RegionsEmployeesPart2.App_Start
{
    public class StartUpConfig
    {
        /// <summary>
        /// This method runs on launch. It checks if the CSV files have been loaded into the DB
        /// </summary>
        public static void Initialize()
        {
            using (RegionsEmployeesContext context = new RegionsEmployeesContext())
            {
                if(!context.Employees.Any() && !context.Regions.Any())
                {
                    GetRegionsFromCSV();
                    GetEmployeesFromCSV(); 
                }
            }
        }

        /// <summary>
        /// Extracts info out of raw CSV and passes the info for object creation
        /// </summary>
        public static void GetRegionsFromCSV()
        {
            var filePath = HostingEnvironment.MapPath("~/App_data/regions.csv");
            using(TextFieldParser parser = new TextFieldParser("C:\\regions.csv"))
            {
                parser.Delimiters = new string[] { "," };

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    int parentId;
                    int regionId = int.Parse(fields[1]);

                    if (!string.IsNullOrWhiteSpace(fields[2]))
                        parentId = Int16.Parse(fields[2]);
                    else
                        parentId = -1;
                    
                    string name = fields[0];
                    InsertRegion(regionId, parentId, name);
                }
            }
        }
        /// <summary>
        /// Extracts info out of raw CSV and passes the info for object creation
        /// </summary>
        public static void GetEmployeesFromCSV()
        {
            using (TextFieldParser parser = new TextFieldParser("C:\\employees.csv"))
            {
                //Set the delimiter for the parser (takes an array of delimiters, we only need comma)
                parser.Delimiters = new string[] { "," };
                while (!parser.EndOfData)
                {
                    //Read csv row into an array (thats what the method returns)
                    string[] fields = parser.ReadFields();

                    //Get the employee details out of the array and create an object
                    int regionID = Int16.Parse(fields[0]);
                    string name = fields[1];
                    string surname = fields[2];

                    InsertEmployee(regionID, name, surname);
                }
            }
        }

        /// <summary>
        /// Searches the DB for a region with the supplied region ID
        /// </summary>
        /// <param name="regionId"> Takes a region ID for searching </param>
        /// <returns></returns>
        public static Region GetRegion(int regionId)
        {
            using (var context = new RegionsEmployeesContext())
            {
                var query = from r in context.Regions
                             where r.RegionId == regionId
                             select r;
                Region region = query.FirstOrDefault();

                return region;         
            }
        }

        /// <summary>
        /// Creates an Employee object and inserts it into the database
        /// </summary>
        /// <param name="regionId"></param>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        public static void InsertEmployee(int regionId, string name, string surname)
        {
            var employee = new Employee
            {
                Name = name,
                Surname = surname,
                Region = GetRegion(regionId)
            };

            using (var context = new RegionsEmployeesContext())
            {
                context.Employees.Add(employee);
                context.Regions.Attach(employee.Region);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Creates a Region object and inserts it into the DB
        /// </summary>
        /// <param name="regionId"></param>
        /// <param name="parentId"></param>
        /// <param name="name"></param>
        public static void InsertRegion(int regionId, int parentId, string name)
        {
            var region = new Region
            {
                Name = name,
                RegionId = regionId,
                ParentRegionId = parentId               
            };

            using (var context = new RegionsEmployeesContext())
            {
                context.Regions.Add(region);
                context.SaveChanges();
            }
        }
    }
}