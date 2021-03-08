```c#
public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(ac => ac.AddPolicy("any", ap => ap.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            services.AddControllers();
        }
```

```c#
app.UseRouting();
app.UseCors();
app.UseAuthorization();
```

```c#
[ApiController]
[EnableCors("any")]
[Route("[controller]/[action]")]
public class EventController : ControllerBase
```

```c#
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
```

```c#
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
                            return new Models.Event { Id = id, EventName = eventName };
                        }
                    }
                }
            }
        }
```

```c#
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
```

```c#
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
                            count = (int)reader["rowNum"];
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
```

connect Format

Data Source=42.192.76.32;Initial Catalog=IShareData;User ID=SA;Password=***;

