using Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BusinessTestProject
{
    [TestClass]
    public class EventTest
    {
        [TestMethod]
        public void ListEventsTest()
        {
            var mock = new Mock<DAL.IEventDAL>();
            mock.Setup(foo => foo.ListEvents()).Returns(new List<Models.Event>(
                new Models.Event[]
                {
                    new Models.Event{Id="1", EventName="eve1"},
                    new Models.Event{Id="2", EventName="eve1"},
                    new Models.Event{Id="3", EventName="eve1"},
                    new Models.Event{Id="4", EventName="eve1"},
                    new Models.Event{Id="5", EventName="eve1"},
                }
            ));
            IEventBLL _eventBus = new Business.Event(mock.Object);
            int expected = 5;
            int actual = _eventBus.ListEvents().Count();
            Assert.AreEqual(expected, actual, "Counts of events error");
        }
    }
}
