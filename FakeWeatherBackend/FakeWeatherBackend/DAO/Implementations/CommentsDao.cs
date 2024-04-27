using FakeWeatherBackend.DAO.Abstract;
using FakeWeatherBackend.DAO.Models;
using FakeWeatherBackend.DAO.Models.Comments;
using Microsoft.EntityFrameworkCore;

namespace FakeWeatherBackend.DAO.Implementations;

public class CommentsDao : ICommentsDao
{
    private readonly MainDbContext _dbContext;

    public CommentsDao
    (
        MainDbContext dbContext
    )
    {
        _dbContext = dbContext;
    }

    public async Task<CommentDbo> AddCommentAsync(CommentDbo commentToAdd)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyCollection<CommentDbo>> GetCommentsByWeatherIdAsync(Guid weatherId)
    {
        return await _dbContext
            .Comments
                
            .Include(c => c.Author)
            .Include(c => c.Weather)
                
            .Where(c => c.WeatherId == weatherId)
                
            .OrderByDescending(c => c.Timestamp)
            
            .ToListAsync();
    }
}