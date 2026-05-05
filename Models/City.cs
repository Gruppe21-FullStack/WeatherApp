using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "City name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }

        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90")]
        public double Latitude { get; set; }

        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180")]
        public double Longitude { get; set; }

        public ICollection<Weather>? Weathers { get; set; }
    }
}