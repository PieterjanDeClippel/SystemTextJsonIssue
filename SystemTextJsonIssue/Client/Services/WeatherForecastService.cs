using SystemTextJsonIssue.Shared.Dtos;
using SystemTextJsonIssue.Shared.Services;

namespace SystemTextJsonIssue.Client.Services
{
    internal class WeatherForecastService : IWeatherForecastService
    {
        private readonly HttpClient httpClient;
        public WeatherForecastService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<WeatherForecast[]> GetWeatherForecasts()
        {
            var response = await httpClient.GetAsync($"{httpClient.BaseAddress}WeatherForecast");
            var content = await response.Content.ReadAsStreamAsync();

            var text = new StreamReader(content).ReadToEnd();
            Console.WriteLine("Sent GET-request. Response from server:" + Environment.NewLine + text);
            content.Seek(0, SeekOrigin.Begin);

            // This deserialization fails because System.Text.Json expects properties to start with a capital
            var result = await System.Text.Json.JsonSerializer.DeserializeAsync<WeatherForecast[]>(content);
            return result ?? Array.Empty<WeatherForecast>();
        }
    }
}
