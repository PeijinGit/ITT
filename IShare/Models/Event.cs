using System;

namespace Models
{
    public class Event
    {
        /// <summary>
        /// Use Guid as event Id
        /// </summary>
        public string Id { get; set; }
        public string EventName { get; set; }
        public int UserId { get; set; }
    }
}
