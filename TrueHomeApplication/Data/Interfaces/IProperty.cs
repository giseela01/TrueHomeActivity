using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueHomeApplication.Models;

namespace TrueHomeApplication.Data.Interfaces
{
    public interface IProperty
    {
        Property GetProperty(int id);
        List<Property> GetListProperties();
    }
}
