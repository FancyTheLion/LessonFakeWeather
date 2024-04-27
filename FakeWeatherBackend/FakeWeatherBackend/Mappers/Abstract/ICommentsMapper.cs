using FakeWeatherBackend.DAO.Models;
using FakeWeatherBackend.DAO.Models.Comments;
using FakeWeatherBackend.Models;
using FakeWeatherBackend.Models.Comments;

namespace FakeWeatherBackend.Mappers.Abstract;

public interface ICommentsMapper
{
    /// <summary>
    /// Take the comment that came from the database and make it into the commnt that the rest of the program uses
    /// </summary>
    Comment Map(CommentDbo comment);
    
    /// <summary>
    /// Multiple comments DBOs to list of models
    /// </summary>
    IReadOnlyCollection<Comment> Map(IEnumerable<CommentDbo> comments);
    
    /// <summary>
    /// Take the comment that the program uses and make it into comment that can be put into the database
    /// </summary>
    CommentDbo Map(Comment comment);

    /// <summary>
    /// Multiple comments models to list of DBOs
    /// </summary>
    IReadOnlyCollection<CommentDbo> Map(IEnumerable<Comment> comments);
}