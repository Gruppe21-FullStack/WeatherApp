using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(double temp, string description, string time)> GetWeatherParsed(double lat, double lon)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://api.met.no/weatherapi/locationforecast/2.0/compact?lat={lat}&lon={lon}"
            );

            request.Headers.Add("User-Agent", "WeatherApp");

            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);

            var timeseries = doc.RootElement
                .GetProperty("properties")
                .GetProperty("timeseries")[0];

            var time = timeseries.GetProperty("time").GetString();

            var details = timeseries
                .GetProperty("data")
                .GetProperty("instant")
                .GetProperty("details");

            var temp = details.GetProperty("air_temperature").GetDouble();

            var description = timeseries
                .GetProperty("data")
                .GetProperty("next_1_hours")
                .GetProperty("summary")
                .GetProperty("symbol_code")
                .GetString();

            return (temp, description, time);
        }
    }
}