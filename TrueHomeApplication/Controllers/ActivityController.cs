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
        public IActionResult CreateActivity([FromBody] Activity activity)
        {
            DateTime _dtSchedule = new DateTime();
            if (ModelState.IsValid)
            {
                //validate if the property is disabled
                var prop = _dataproperty.GetProperty(activity.Property_Id);
                if (prop != null)
                {
                    if (prop.Disabled_at == null)
                    {
                        activity.Property_Id = prop.Id;

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

        [HttpGet("GetAllActivitiesFilter/{StarDate}/{EndDate}/{status}")]
        public IEnumerable<ActivityViewModel> GetActivityWithFilter(DateTime _StarDate, DateTime _EndDate, string Status )
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

        [HttpPut("ModifyActivity")]
        public IActionResult EditActivity([FromBody] Activity activity)
        {
            if (ModelState.IsValid)
            {
                var act = _dataactivity.GetActivity(activity.Id);

                if (act != null && act.Status != "Cancelled")
                {
                    var lstActivities = _dataactivity.GetListActivities().Where(x => x.Property_Id == activity.Property_Id
                        && activity.Schedule > x.Schedule && activity.Schedule < x.Schedule.AddHours(1)).ToList();
                    if (lstActivities.Count == 0)
                    {
                        activity.Created_at = act.Created_at;
                        activity.Property_Id = act.Property_Id;
                        activity.Schedule = act.Schedule;
                        activity.Status = act.Status;
                        activity.Title = act.Title;
                        activity.Updated_at = act.Updated_at;

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
