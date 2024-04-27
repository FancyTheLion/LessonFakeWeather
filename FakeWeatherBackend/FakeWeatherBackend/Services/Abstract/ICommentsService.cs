using FakeWeatherBackend.Models.Comments;

namespace FakeWeatherBackend.Services.Abstract;

/// <summary>
/// Service to work with comments
/// </summary>
public interface ICommentsService
{
    /// <summary>
    /// Get comments by weather ID
    /// </summary>
    Task<IReadOnlyCollection<Comment>> GetCommentsByWeatherIdAsync(Guid weatherId);
}