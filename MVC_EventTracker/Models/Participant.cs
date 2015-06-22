using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_EventTracker.Models
{
    public class Participant
    {
        public int ParticipantID { get; set; }
        public string ParticipantENT { get; set; }

        public virtual ICollection<Registration> Registrations { get; set; }
    }
}