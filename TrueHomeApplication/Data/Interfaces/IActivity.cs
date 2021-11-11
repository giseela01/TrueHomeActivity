using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueHomeApplication.Models;

namespace TrueHomeApplication.Data.Interfaces
{
    public interface IActivity
    {
        void AddActivity(Activity activity);
        void UpdateActivity(Activity activity);
        Activity GetActivity(int id);
        List<ActivityViewModel> GetListActivitiesWithFilter(DateTime sdate, DateTime edate, string status);
        List<ActivityViewModel> GetListActivities();
    }
}
