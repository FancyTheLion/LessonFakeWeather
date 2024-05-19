using System.Text.Json.Serialization;
using FakeWeatherBackend.Models.API.DTOs.Users;
using FakeWeatherBackend.Models.Comments;

namespace FakeWeatherBackend.Models.API.DTOs.Comments;

/// <summary>
/// Comment DTO
/// </summary>
public class CommentDto
{
    /// <summary>
    /// Comment ID
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Message author
    /// </summary>
    [JsonPropertyName("author")]
    public UserDto Author { get; set; }
    
    /// <summary>
    /// Weather time
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }
    
    /// <summary>
    /// Content of the comment
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; }

    /// <summary>
    /// Parent weather
    /// </summary>
    [JsonPropertyName("parentWeatherId")]
    public Guid WeatherId { get; set; }

    public CommentDto
    (
        Guid id,
        UserDto author,
        DateTime timestamp,
        string content,
        Guid weatherId
    )
    {
        Id = id;
        Author = author ?? throw new ArgumentNullException(nameof(author), "Author mustn't be null!");
        Timestamp = timestamp;
        Content = content;
        WeatherId = weatherId;
    }
}