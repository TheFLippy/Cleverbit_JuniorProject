using Cleverbit.RegionsEmployeesPart2.Models.Interfaces;
using System;
using System.Diagnostics;

namespace Cleverbit.RegionsEmployeesPart2.Models
{
    [DebuggerDisplay("Name: {Name}, Surname: {Surname}, Region: {Region.RegionId}")]
    public class Employee : IModificationHistory
    {
        public int Id { get; set; }
        public virtual Region Region { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool IsDirty { get; set; }

        public Employee()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;

            if (DateCreated == DateTime.MinValue)
            {
                DateCreated = DateTime.Now;
                DateModified = DateTime.Now;
            }
            else
            {
                DateModified = DateTime.Now;
            }


            var a = new Employee();
            a.DateCreated = DateTime.MaxValue;
        }
    }
}