using FakeWeatherBackend.DAO.Models;
using FakeWeatherBackend.DAO.Models.Comments;
using FakeWeatherBackend.Mappers.Abstract;
using FakeWeatherBackend.Models;
using FakeWeatherBackend.Models.Comments;

namespace FakeWeatherBackend.Mappers.Implementations;

public class CommentsMapper : ICommentsMapper
{
    private readonly IUsersMapper _usersMapper;
    
    public CommentsMapper
    (
        IUsersMapper usersMapper
    )
    {
        _usersMapper = usersMapper;
    }

    public Comment Map(CommentDbo comment)
    {
        if (comment == null)
        {
            return null;
        }
        
        return new Comment
        (
            comment.Id,
            _usersMapper.Map(comment.Author),
            comment.Timestamp,
            comment.Content,
            new Weather(comment.Id)
        );
    }

    public IReadOnlyCollection<Comment> Map(IEnumerable<CommentDbo> comments)
    {
        if (comments == null)
        {
            return null;
        }
        
        return comments
            .Select(c => Map(c))
            .ToList();
    }

    public CommentDbo Map(Comment comment)
    {
        if (comment == null)
        {
            return null;
        }

        return new CommentDbo()
        {
            Id = comment.Id,
            Author = _usersMapper.Map(comment.Author),
            Timestamp = comment.Timestamp,
            Content = comment.Content,
            WeatherId = comment.Weather.Id,
            Weather = new WeatherDbo() { Id = comment.Weather.Id }
        };
    }

    public IReadOnlyCollection<CommentDbo> Map(IEnumerable<Comment> comments)
    {
        if (comments == null)
        {
            return null;
        }
        
        return comments
            .Select(c => Map(c))
            .ToList();
    }
}