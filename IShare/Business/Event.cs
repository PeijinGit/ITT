using DAL;
using System.Collections.Generic;

namespace Business
{
    public class Event : IEventBLL
    {
        IEventDAL dal;

        public Event(IEventDAL eventDAL) 
        {
            dal = eventDAL;
        }

        public IEnumerable<Models.Event> ListEvents()
        {
            return dal.ListEvents();
        }

        public IEnumerable<Models.Event> ListEventsById(int id)
        {
            return dal.ListEventsById( id);
        }

        public Models.Event AddEvent(Models.Event newEvent) 
        {
            return dal.AddEvent(newEvent.UserId,newEvent.EventName);
        }

        public Models.Event UpdateEvent(Models.Event newEvent)
        {
            return dal.UpdateEvent(newEvent.Id, newEvent.EventName);
        }

        public int DeleteEvent(string id)
        {
            return dal.DeleteEvent(id);
        }
    }
}
