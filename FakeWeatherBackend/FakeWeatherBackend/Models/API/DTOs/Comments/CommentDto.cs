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
    public Guid Id { get; set; }
    
    /// <summary>
    /// Message author
    /// </summary>
    public UserDto Author { get; set; }
    
    /// <summary>
    /// Weather time
    /// </summary>
    public DateTime Timestamp { get; set; }
    
    /// <summary>
    /// Content of the comment
    /// </summary>
    public string Content { get; set; }
    
    /// <summary>
    /// Parent weather
    /// </summary>
    public WeatherDto Weather { get; set; }

    public CommentDto
    (
        Guid id,
        UserDto author,
        DateTime timestamp,
        string content,
        WeatherDto weather
    )
    {
        Id = id;
        Author = author ?? throw new ArgumentNullException(nameof(author), "Author mustn't be null!");
        Timestamp = timestamp;
        Content = content;
        Weather = weather ?? throw new ArgumentNullException(nameof(weather), "Weather mustn't be null!");
    }
    
    public Comment ToModel()
    {
        return new Comment(Id, Author.ToModel(), Timestamp, Content, Weather.ToModel());
    }
}