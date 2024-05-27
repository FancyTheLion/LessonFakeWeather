using FakeWeatherBackend.DAO.Abstract;
using FakeWeatherBackend.DAO.Models;
using FakeWeatherBackend.DAO.Models.Authentification;
using FakeWeatherBackend.DAO.Models.Comments;
using FakeWeatherBackend.Mappers.Abstract;
using FakeWeatherBackend.Models.API.DTOs.Comments;
using FakeWeatherBackend.Models.Comments;
using FakeWeatherBackend.Models.Users;
using FakeWeatherBackend.Services.Abstract;

namespace FakeWeatherBackend.Services.Implementations;

/// <summary>
/// Service to work with comments
/// </summary>
public class CommentsService : ICommentsService
{
    private readonly ICommentsDao _commentsDao;
    private readonly ICommentsMapper _commentsMapper;

    public CommentsService
    (
        ICommentsDao commentsDao,
        ICommentsMapper commentsMapper
    )
    {
        _commentsDao = commentsDao;
        _commentsMapper = commentsMapper;
    }
    
    public async Task<IReadOnlyCollection<Comment>> GetCommentsByWeatherIdAsync(Guid weatherId)
    {
        return _commentsMapper.Map(await _commentsDao.GetCommentsByWeatherIdAsync(weatherId));
    }

    public async Task<Comment> AddCommentAsync(CommentToAdd commentToAdd)
    {
        var commentDbo = new CommentDbo()
        {
            Id = Guid.Empty,
            Author = new UserDbo() { Id = commentToAdd.AuthorId },
            Content = commentToAdd.Content,
            Timestamp = DateTime.UtcNow,
            WeatherId = commentToAdd.WeatherId,
            Weather = new WeatherDbo() { Id = commentToAdd.WeatherId }
        };

        return _commentsMapper.Map(await _commentsDao.AddCommentAsync(commentDbo));
    }
}