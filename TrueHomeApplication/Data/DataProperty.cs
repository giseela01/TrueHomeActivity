using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueHomeApplication.Data.Interfaces;
using TrueHomeApplication.Models;

namespace TrueHomeApplication.Data
{
    public class DataProperty : IProperty
    {
        private readonly ActivityContext _context;

        public DataProperty(ActivityContext context)
        {
            _context = context;
        }

        public Property GetProperty(int id)
        {
            try
            {
                Property pop = _context.Property.FirstOrDefault(p => p.Id == id);
                return pop;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Property> GetListProperties()
        {
            try
            {
                return _context.Property.OrderBy(x => x.Id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
