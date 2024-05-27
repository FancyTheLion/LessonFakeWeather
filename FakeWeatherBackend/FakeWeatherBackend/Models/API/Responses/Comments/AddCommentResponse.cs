using System.Text.Json.Serialization;
using FakeWeatherBackend.Models.API.DTOs;
using FakeWeatherBackend.Models.API.DTOs.Comments;

namespace FakeWeatherBackend.Models.API.Responses.Comments;

public class AddCommentResponse
{
    /// <summary>
    /// Added comment
    /// </summary>
    [JsonPropertyName("addedComment")]
    public CommentDto AddedComment { get; private set; }

    public AddCommentResponse
    (
        CommentDto addedComment
    )
    {
        AddedComment = addedComment ??
                       throw new ArgumentNullException(nameof(addedComment), "Comment mustn't be null!");
    }
}