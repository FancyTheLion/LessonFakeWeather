using System.Text.Json.Serialization;

namespace FakeWeatherBackend.Models.API.DTOs.Comments;

public class AddCommentDto
{
    /// <summary>
    /// Content of the comment
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; }
    
    /// <summary>
    /// Comment belongs to this weather
    /// </summary>
    [JsonPropertyName("weatherId")]
    public Guid WeatherId { get; set; }
}