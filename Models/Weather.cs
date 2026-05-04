using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherApp.Models
{
    public class Weather
    {
        public int Id { get; set; }

        public double Temperature { get; set; }

        public string Description { get; set; }

        // Foreign Key
        public int CityId { get; set; }

        // Navigation property
        public City City { get; set; }
    }
}