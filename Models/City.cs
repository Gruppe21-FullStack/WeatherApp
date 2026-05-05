using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public ICollection<Weather>? Weathers { get; set; }
    }
}