using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ServiceManagerFire.Models
{
    public class StatusUserViewModel
    {
        public List<Incident>? Incidents { get; set; }
        public SelectList? Users { get; set; }
        public SelectList? Statuses { get; set; }
        public string? IncidentUser { get; set; }
        public string? St { get; set; }

    }
}
