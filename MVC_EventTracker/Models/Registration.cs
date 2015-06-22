using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_EventTracker.Models
{
    public class Registration
    {
        public int RegistrationID { get; set; }
        public int EventID { get; set; }
        public int ParticipantID { get; set; }
        public int BlockID { get; set; }

        public virtual Event Event { get; set; }
        public virtual Participant Participant { get; set; }
    }
}