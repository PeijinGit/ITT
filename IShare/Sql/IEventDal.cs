using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IEventDAL
    {
        IEnumerable<Models.Event> ListEventsById(int id);
        IEnumerable<Models.Event> ListEvents();
        Models.Event AddEvent(int creatorId, string eventName);
        Models.Event UpdateEvent(string id, string eventName);
        int DeleteEvent(string id);
    }
}
