using System.Text.Json.Serialization;
using FakeWeatherBackend.Models.API.DTOs;
using FakeWeatherBackend.Models.API.DTOs.Comments;

namespace FakeWeatherBackend.Models.API.Responses.Comments;

/// <summary>
/// Comments list response
/// </summary>
public class CommentsListResponse
{
    /// <summary>
    /// Comments collection
    /// </summary>
    [JsonPropertyName("comments")]
    public IReadOnlyCollection<CommentDto> Comments { get; private set; }

    public CommentsListResponse
    (
        IReadOnlyCollection<CommentDto> comments
    )
    {
        Comments = comments ?? throw new ArgumentNullException(nameof(comments), "Comments collection can't be null!");
    }
}