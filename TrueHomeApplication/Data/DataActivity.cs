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
        public List<ActivityViewModel> GetListActivitiesWithFilter(DateTime sdate, DateTime edate, string status)
        {
            try
            {
                DateTime _date = DateTime.Now;
                var lst = _context.Activity.Where(x => sdate <= x.Schedule && x.Schedule <= edate && x.Status==status)
                    .Select(c => new ActivityViewModel
                    {
                        Id = c.Id,
                        Schedule = c.Schedule,
                        Title = c.Title,
                        Created_at = c.Created_at,
                        Status = c.Status,
                        Condition = "",
                        Property_Id = c.Property_Id,
                        TitleProperty = c.property.Title,
                        AddressProperty = c.property.Address,
                    })
                    .OrderBy(x => x.Id).Include(x => x.property).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ActivityViewModel> GetListActivities()
        {
            try
            {
                DateTime _date = DateTime.Now;
                var lst = _context.Activity.Where(x => _date.AddDays(-3) <= x.Schedule && x.Schedule <= _date.AddDays(14))
                    .Select(c => new ActivityViewModel {
                        Id = c.Id,
                        Schedule = c.Schedule,
                        Title = c.Title,
                        Created_at = c.Created_at,
                        Status = c.Status,
                        Condition = "",
                        Property_Id = c.Property_Id,
                        TitleProperty = c.property.Title,
                        AddressProperty = c.property.Address,
                    })
                    .OrderBy(x => x.Id).Include(x => x.property).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
