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

        public async Task<(double temp, string description, string time)> GetWeather(double lat, double lon)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://api.met.no/weatherapi/locationforecast/2.0/compact?lat={lat}&lon={lon}"
            );

            request.Headers.Add("User-Agent", "WeatherApp");

            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);

            var root = doc.RootElement;

            var timeseries = root
                .GetProperty("properties")
                .GetProperty("timeseries");

            var first = timeseries[0];

            var time = first.GetProperty("time").GetString();

            var details = first
                .GetProperty("data")
                .GetProperty("instant")
                .GetProperty("details");

            var temp = details.GetProperty("air_temperature").GetDouble();

            var description = first
                .GetProperty("data")
                .GetProperty("next_1_hours")
                .GetProperty("summary")
                .GetProperty("symbol_code")
                .GetString();

            return (temp, description ?? "unknown", time ?? "unknown");
        }
    }
}