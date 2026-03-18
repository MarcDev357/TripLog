using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyTripLog.Models
{
    public class Accommodation
    {
        public int AccommodationId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        // Optional per assignment
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public ICollection<Trip> Trips { get; set; } = new List<Trip>();
    }
}