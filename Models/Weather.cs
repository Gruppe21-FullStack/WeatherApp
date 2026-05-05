using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherApp.Models
{
    public class Weather
    {
        public int Id { get; set; }

        [Required]
        public double Temperature { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        // Foreign Key
        public int CityId { get; set; }

        // Navigation Property
        public City City { get; set; }
    }
}