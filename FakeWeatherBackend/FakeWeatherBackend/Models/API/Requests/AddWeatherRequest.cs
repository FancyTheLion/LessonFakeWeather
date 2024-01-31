using System.Text.Json.Serialization;
using FakeWeatherBackend.Models.API.DTOs;

namespace FakeWeatherBackend.Models.API.Requests;

/// <summary>
/// Request to add weather to backend
/// </summary>
public class AddWeatherRequest
{
    [JsonPropertyName("weatherToAdd")]
    public WeatherDto WeatherToAdd { get; set; }
}