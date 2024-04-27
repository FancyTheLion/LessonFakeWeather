using FakeWeatherBackend.DAO.Abstract;
using FakeWeatherBackend.Mappers.Abstract;
using FakeWeatherBackend.Models.Comments;
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
}