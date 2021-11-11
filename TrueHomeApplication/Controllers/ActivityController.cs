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
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivity _dataactivity;

        public ActivityController(IActivity dataactivity)
        {
            _dataactivity = dataactivity;
        }

        [HttpGet]
        public IEnumerable<Activity> GetActivities()
        {
            return _dataactivity.GetListActivities();
        }

        [HttpPost]
        public IActionResult CreateActivity([FromBody] Activity activity)
        {
            if (ModelState.IsValid)
            {
                _dataactivity.AddActivity(activity);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public Activity GetActivity(int id)
        {
            return _dataactivity.GetActivity(id);
        }

        [HttpPut]
        public IActionResult EditActivity([FromBody] Activity activity)
        {
            if (ModelState.IsValid)
            {
                _dataactivity.UpdateActivity(activity);
                return Ok();
            }
            return BadRequest();
        }

    }
}
