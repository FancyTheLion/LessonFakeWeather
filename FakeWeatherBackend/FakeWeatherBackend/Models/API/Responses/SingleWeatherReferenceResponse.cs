using System.Text.Json.Serialization;
using FakeWeatherBackend.Models.API.DTOs;
using Microsoft.Win32.SafeHandles;

namespace FakeWeatherBackend.Models.API.Responses;

/// <summary>
/// Response with only weather reference
/// </summary>
public class SingleWeatherReferenceResponse
{
    /// <summary>
    /// Single weather reference
    /// </summary>
    [JsonPropertyName("weatherReference")]
    public WeatherReferenceDto WeatherReference { get; private set; }

    [JsonPropertyName("isNoReference")]
    public bool IsNoReference { get; private set; }

    public SingleWeatherReferenceResponse
    (
        WeatherReferenceDto weatherReference
    )
    {
        IsNoReference = weatherReference == null;
        WeatherReference = weatherReference;
    }
}