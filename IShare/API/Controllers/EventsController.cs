using System.Collections.Generic;
using API.Filters;
using Business;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [EnableCors("any")]
    [ApiController]
    [Route("[controller]/[action]")]
    public class EventsController : ControllerBase
    {
        IEventBLL business;

        public EventsController(IEventBLL eventBLL) 
        {
            business = eventBLL;
        }

        [HttpGet]
        public IEnumerable<Models.Event> GetAllEvents()
        {
            return business.ListEvents();
        }

        [HttpGet]
        //[TypeFilter(typeof(ISActionFitler))]
        public IEnumerable<Models.Event> ListEventsById(int id)
        {

            return business.ListEventsById(id);
        }

        [HttpPost]
        //[TypeFilter(typeof(ISActionFitler))]
        public Models.ResResult<Models.Event> AddEvent(Models.Event newEvent)
        {
            Models.Event resultEvent = business.AddEvent(newEvent);
            if (resultEvent == null)
            {
                return new Models.ResResult<Models.Event> { Status = -1, Msg = "Add Fail" };
            }
            else
            {
                Models.ResResult<Models.Event> resResult = new Models.ResResult<Models.Event>();
                resResult.Status = 1;
                resResult.ResultData = new List<Models.Event>() { resultEvent };
                resResult.Msg = "Add Success";

                return resResult;
            }
        }

        [HttpPost]
        //[TypeFilter(typeof(ISActionFitler))]
        public Models.ResResult<Models.Event> UpdateEvent(Models.Event newEvent)
        {
            Models.Event resutEvent = business.UpdateEvent(newEvent);
            if (resutEvent == null)
            {
                return new Models.ResResult<Models.Event> { Status = -1, Msg = "Add Fail" };
            }
            else
            {
                Models.ResResult<Models.Event> resResult = new Models.ResResult<Models.Event>();
                resResult.Status = 1;
                resResult.ResultData = new List<Models.Event>() { resutEvent };
                resResult.Msg = "Add Success";

                return resResult;
            }
        }

        [HttpGet]
        //[TypeFilter(typeof(ISActionFitler))]
        public Models.ResResult<Models.Event> DeleteEvent(string id)
        {
            int res = business.DeleteEvent(id);

            if (res == -1)
            {
                return new Models.ResResult<Models.Event> { Status = -1, Msg = "Add Fail" };
            }
            else
            {
                Models.ResResult<Models.Event> resResult = new Models.ResResult<Models.Event>();
                resResult.Status = 1;
                //resResult.ResultData = new List<Models.Event>() { resutEvent };
                resResult.Msg = "Delete Success";

                return resResult;
            }
        }


        public string Welcome() 
        {
            return "Program start Welcome!";
        }
    }
}
