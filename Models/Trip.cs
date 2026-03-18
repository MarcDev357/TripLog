using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyTripLog.Models
{
    public class Trip
    {
        public int TripId { get; set; }

        [Required]
        public int DestinationId { get; set; }
        public Destination Destination { get; set; } = null!;

        [Required]
        public int AccommodationId { get; set; }
        public Accommodation Accommodation { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public ICollection<TripActivity> TripActivities { get; set; } = new List<TripActivity>();
    }
}