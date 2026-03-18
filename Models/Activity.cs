using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyTripLog.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<TripActivity> TripActivities { get; set; } = new List<TripActivity>();
    }
}