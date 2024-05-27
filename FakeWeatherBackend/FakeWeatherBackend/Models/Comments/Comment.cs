using System.ComponentModel.DataAnnotations;
using FakeWeatherBackend.Models.API.DTOs.Comments;
using FakeWeatherBackend.Models.Users;

namespace FakeWeatherBackend.Models.Comments;

public class Comment
{
    /// <summary>
    /// Comment ID
    /// </summary>
    public Guid Id { get; private set; }
    
    /// <summary>
    /// Message author
    /// </summary>
    public User Author { get; private set; }
    
    /// <summary>
    /// Weather time
    /// </summary>
    public DateTime Timestamp { get; private set; }
    
    /// <summary>
    /// Content of the comment
    /// </summary>
    public string Content { get; private set; }

    /// <summary>
    /// Parent weather ID
    /// </summary>
    public Guid WeatherId { get; private set; }

    public Comment
    (
        Guid id,
        User author,
        DateTime timestamp,
        string content,
        Guid weatherId
    )
    {
        Id = id;
        Author = author ?? throw new ArgumentNullException(nameof(author), "Author mustn't be null!");
        Timestamp = timestamp;

        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ArgumentException("Content must have something!", nameof(content));
        }
        Content = content;
        
        WeatherId = weatherId;
    }

    public CommentDto ToDto()
    {
        return new CommentDto(Id, Author.ToDto(), Timestamp, Content, WeatherId);
    }
}