namespace SystemTextJsonIssue.Shared.Services
{
    public interface IWeatherForecastService
    {
        Task<Dtos.WeatherForecast[]> GetWeatherForecasts();
    }
}
