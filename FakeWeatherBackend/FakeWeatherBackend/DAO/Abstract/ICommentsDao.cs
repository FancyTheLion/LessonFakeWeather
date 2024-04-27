using FakeWeatherBackend.DAO.Implementations;
using FakeWeatherBackend.DAO.Models;
using FakeWeatherBackend.DAO.Models.Comments;

namespace FakeWeatherBackend.DAO.Abstract;

public interface ICommentsDao
{
    /// <summary>
    /// Add new comment
    /// </summary>
    Task<CommentDbo> AddCommentAsync(CommentDbo comment);
    
    /// <summary>
    /// Get all comments for weather
    /// </summary>
    Task<IReadOnlyCollection<CommentDbo>> GetCommentsByWeatherIdAsync(Guid weatherId);
}