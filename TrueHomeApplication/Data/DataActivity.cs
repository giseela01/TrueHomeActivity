using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueHomeApplication.Data.Interfaces;
using TrueHomeApplication.Models;

namespace TrueHomeApplication.Data
{
    public class DataActivity : IActivity
    {
        private readonly ActivityContext _context;
        private DbSet<Activity> _dbSet;

        public DataActivity(ActivityContext context)
        {
            _context = context;
        }

        public void AddActivity(Activity activity)
        {
            try
            {
                _context.Activity.Add(activity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateActivity(Activity activity)
        {
            try
            {
                _context.Activity.Update(activity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Activity GetActivity(int id)
        {
            try
            {
                return _context.Activity.FirstOrDefault(p => p.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Activity> GetListActivities()
        {
            try
            {
                return _context.Activity.OrderBy(x => x.Id).Include(x => x.property).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
