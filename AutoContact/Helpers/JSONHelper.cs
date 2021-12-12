using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoContact.Helpers
{
    public static class JSONHelper
    {
        public static string GetAppointmentListJSONString(List<Models.Appointment> appointments)
        {
            var eventList = new List<Event>();
            foreach (var appt in appointments)
            {
                var myEvent = new Event()
                {
                    id = (int)appt.AppointmentId,
                    start = appt.AppointmentStartTime,
                    end = appt.AppointmentStartTime.AddHours(2),
                    title = $"{appt.Car} Appointment: " + appt.Message
                    //description = appt.Car.ToString()
                    //resourceId = appt.location.id
                };
                eventList.Add(myEvent);
            }
            return System.Text.Json.JsonSerializer.Serialize(eventList);
        }

        // PRELIMINARY LOCATION PROTOTYPE
        //public static string GetResourceListJSONString(List<Models.Location> locations)
        //{
        //    var resourceList = new List<Resource>();
        //    foreach (var loc in locations)
        //    {
        //        var resource = new Resource()
        //        {
        //            id = loc.id,
        //            title = loc.Name
        //        };
        //        resourceList.Add(resource);
        //    }
        //    return System.Text.Json.JsonSerializer.Serialize(resourceList);
        //}
    }

    // Appointment Display Class for Full Calendar
    public class Event
    {
        public int id { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public string title { get; set; }
        //public string description { get; set; }
        //public int resourceId { get; set; }
    }

    // Location Display Class for Full Calendar
    public class Resource
    {
        public int id { get; set; }
        public string title { get; set; }
    }
}
