using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueHomeApplication.Data.Interfaces;
using TrueHomeApplication.Models;

namespace TrueHomeApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivity _dataactivity;
        private readonly IProperty _dataproperty;

        public ActivityController(IActivity dataactivity, IProperty dataproperty)
        {
            _dataactivity = dataactivity;
            _dataproperty = dataproperty;
        }

        [HttpGet("GetAllActivities")]
        public IEnumerable<ActivityViewModel> GetActivities()
        {
            DateTime _date = DateTime.Now;
            var lstActivities = _dataactivity.GetListActivities();
            foreach (ActivityViewModel item in lstActivities)
            {
                if (item.Status == "Active" && item.Schedule >= _date)
                { item.Condition = "Pendiente a realizar"; }
                else if (item.Status == "Active" && item.Schedule < _date)
                { item.Condition = "Atrasada"; }
                else if (item.Status == "Done")
                { item.Condition = "Finalizada"; }
            }
            return lstActivities;
        }

        [HttpPost("CreateActivity")]
        public IActionResult CreateActivity([FromBody]ActivityRequest _activity)
        {
            if (ModelState.IsValid)
            {
                Activity activity = new Activity();
                //validate if the property is disabled
                var prop = _dataproperty.GetProperty(_activity.Property_Id);
                if (prop != null)
                {
                    if (prop.Disabled_at == null)
                    {
                        activity.Created_at = _activity.Created_at;
                        activity.Property_Id = prop.Id;
                        activity.Schedule = _activity.Schedule;
                        activity.Status = _activity.Status;
                        activity.Title = _activity.Title;
                        activity.Updated_at = _activity.Updated_at;

                        //validate if the activity schedule is not between schedules activities from the same property
                        var lstActivities = _dataactivity.GetListActivities().Where(x => x.Property_Id == activity.Property_Id
                        && activity.Schedule > x.Schedule && activity.Schedule < x.Schedule.AddHours(1)).ToList();

                        if (lstActivities.Count == 0)
                        {
                            _dataactivity.AddActivity(activity);
                            return Ok();
                        }
                        else
                        {
                            var message = "La actividad se encuentra en un horario no disponible";
                            return BadRequest(message);
                        }
                    }
                    else
                    {
                        var message = "La actividad tiene una propiedad en estatus desactivada";
                        return BadRequest(message);
                    }
                }
                else
                {
                    var message = "La actividad tiene una propiedad que no esta dentro del catálogo";
                    return BadRequest(message);
                }
            }
            return BadRequest();
        }

        [HttpGet("GetAllActivitiesFilter")]
        public IEnumerable<ActivityViewModel> GetActivityWithFilter([FromQuery] ActivityFiltersRequest _activity)
        {
            DateTime _date = DateTime.Now;
            var lstActivities = _dataactivity.GetListActivitiesWithFilter(_activity.StartDate, _activity.EndDate, _activity.Status);
            foreach (ActivityViewModel item in lstActivities)
            {
                if (item.Status == "Active" && item.Schedule >= _date)
                { item.Condition = "Pendiente a realizar"; }
                else if (item.Status == "Active" && item.Schedule < _date)
                { item.Condition = "Atrasada"; }
                else if (item.Status == "Done")
                { item.Condition = "Finalizada"; }
            }
            return lstActivities;
        

        }

        [HttpPut("ModifyActivity")]
        public IActionResult EditActivity([FromBody] ModifyActivityRequest _activity)
        {
            if (ModelState.IsValid)
            {
                Activity activity = new Activity();

                activity.Id = _activity.Id;
                activity.Schedule = _activity.Schedule;
                activity.Status = _activity.Status;

                var act = _dataactivity.GetActivity(activity.Id);

                if (act != null && act.Status != "Cancelled")
                {
                    var lstActivities = _dataactivity.GetListActivities().Where(x => x.Property_Id == activity.Property_Id
                        && activity.Schedule > x.Schedule && activity.Schedule < x.Schedule.AddHours(1)).ToList();
                    if (lstActivities.Count == 0)
                    {
                        activity.Created_at = act.Created_at;
                        activity.Property_Id = act.Property_Id;
                        activity.Title = act.Title;
                        activity.Updated_at = DateTime.Now;

                        _dataactivity.UpdateActivity(activity);
                        return Ok();
                    }
                    else
                    {
                        var message = "La actividad se encuentra en un horario no disponible";
                        return BadRequest(message);
                    }

                }
            }
            return BadRequest();
        }

    }
}
