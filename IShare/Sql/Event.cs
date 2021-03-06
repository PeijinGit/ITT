using Microsoft.Extensions.Options;
using Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
    public class Event : BaseDAL, IEventDAL
    {

        public Event(IOptions<AppSettingModels> appSettings) : base(appSettings)
        {
        }

        public int DeleteEvent(string id)
        {
            var events = new List<Models.Event>();
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("DeleteEvent", connection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    connection.Open();
                    command.Parameters.Add(new SqlParameter("@id", id));
                    using (var reader = command.ExecuteReader())
                    {
                        int count = 0;
                        while (reader.Read()) 
                        {
                            count= (int)reader["rowNum"];
                        }
                        if (count == 0)
                        {
                            return -1;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Add event function
        /// Insert single event with eventId CreatorId and eventName 
        /// </summary>
        /// <param name="creatorId"></param>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public Models.Event AddEvent(int creatorId, string eventName)
        {
            var events = new List<Models.Event>();
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("AddEvents", connection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    connection.Open();
                    string id = System.Guid.NewGuid().ToString("N");
                    command.Parameters.Add(new SqlParameter("@creatorId", creatorId));
                    command.Parameters.Add(new SqlParameter("@eventName", eventName));
                    command.Parameters.Add(new SqlParameter("@id", id));
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader == null)
                        {
                            return null;
                        }
                        else
                        {
                            return new Models.Event {Id = id,EventName = eventName };
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Update event function
        /// Update event with eventId and new eventName 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public Models.Event UpdateEvent(string id, string eventName)
        {
            var events = new List<Models.Event>();
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("UpdateEvent", connection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    connection.Open();
                    command.Parameters.Add(new SqlParameter("@eventName", eventName));
                    command.Parameters.Add(new SqlParameter("@id", id));
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader == null)
                        {
                            return null;
                        }
                        else
                        {
                            return new Models.Event { Id = id, EventName = eventName };
                        }
                    }
                }
            }
        }

        public IEnumerable<Models.Event> ListEvents()
        {
            var events = new List<Models.Event>();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("ListEvents", connection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            events.Add(new Models.Event { Id = (string)reader["Id"], EventName = (string)reader["EventName"], UserId = (int)reader["CreatorId"] });
                        }
                    }
                }
            }
            return events;
        }

        public IEnumerable<Models.Event> ListEventsById(int id)
        {
            var events = new List<Models.Event>();
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("ShowEventByUser", connection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    connection.Open();
                    command.Parameters.Add(new SqlParameter("@id", id));
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            events.Add(new Models.Event { Id = (string)reader["Id"], EventName = (string)reader["EventName"] });
                        }
                    }
                }
            }
            return events;
        }
    }
}
