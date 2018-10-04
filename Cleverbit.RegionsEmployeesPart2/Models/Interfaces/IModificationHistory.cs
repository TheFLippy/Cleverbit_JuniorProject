using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cleverbit.RegionsEmployeesPart2.Models.Interfaces
{
    public interface IModificationHistory 
    {
        DateTime DateModified { get; set; }
        DateTime DateCreated { get; set; }
        bool IsDirty { get; set; }
    }
}