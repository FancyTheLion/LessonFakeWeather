namespace FakeWeatherBackend.Models.Comments;

/// <summary>
/// Information, required to add a comment
/// </summary>
public class CommentToAdd
{
    /// <summary>
    /// Comment content
    /// </summary>
    public string Content { get; private set; }

    /// <summary>
    /// Comment belongs to this weather
    /// </summary>
    public Guid WeatherId { get; private set; }

    /// <summary>
    /// Comment's author ID
    /// </summary>
    public Guid AuthorId { get; private set; }

    public CommentToAdd
    (
        string content,
        Guid weatherId,
        Guid authorId
    )
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ArgumentException("Content must not be empty!", nameof(content));
        }
        Content = content;

        WeatherId = weatherId;
        AuthorId = authorId;
    }
}