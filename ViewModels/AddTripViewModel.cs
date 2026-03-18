using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyTripLog.ViewModels
{
    public class AddTripViewModel
    {
        [Required]
        public int DestinationId { get; set; }

        [Required]
        public int AccommodationId { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime EndDate { get; set; }

        public List<int> SelectedActivityIds { get; set; } = new();

        public List<SelectListItem> Destinations { get; set; } = new();
        public List<SelectListItem> Accommodations { get; set; } = new();
        public List<SelectListItem> Activities { get; set; } = new();
    }
}