using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public interface IEventBLL
    {
        IEnumerable<Models.Event> ListEvents();
        IEnumerable<Models.Event> ListEventsById(int id);
        Models.Event AddEvent(Models.Event newEvent);
        Models.Event UpdateEvent(Models.Event newEvent);
        int DeleteEvent(string id);
    }
}
