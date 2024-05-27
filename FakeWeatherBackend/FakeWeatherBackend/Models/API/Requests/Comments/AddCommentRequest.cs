using System.Text.Json.Serialization;
using FakeWeatherBackend.Models.API.DTOs.Comments;

namespace FakeWeatherBackend.Models.API.Requests.Comments;

/// <summary>
/// Request to add comment
/// </summary>
public class AddCommentRequest
{
    /// <summary>
    /// Comment itself
    /// </summary>
    [JsonPropertyName("commentToAdd")]
    public AddCommentDto CommentToAdd { get; set; }
}