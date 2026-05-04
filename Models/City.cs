using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Weather> Weathers { get; set; }
    }
}