using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_EventTracker.Models;

namespace MVC_EventTracker.ViewModel
{
    public class RegistrationViewModel
    {
        public Event RegEvent { get; set; }
        public Participant RegParticipant { get; set; }

    }

}