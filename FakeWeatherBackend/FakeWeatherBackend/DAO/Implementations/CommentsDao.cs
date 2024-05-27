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
        _ = commentToAdd ?? throw new ArgumentNullException(nameof(commentToAdd), "Comment can't be null!");

        commentToAdd.Author = await _dbContext.Users.SingleAsync(a => a.Id == commentToAdd.Author.Id);
        commentToAdd.Weather = await _dbContext.Weathers.SingleAsync(w => w.Id == commentToAdd.Weather.Id);
        
        await _dbContext
            .Comments
            .AddAsync(commentToAdd);
            
        await _dbContext.SaveChangesAsync();

        return commentToAdd;
    }

    public async Task<IReadOnlyCollection<CommentDbo>> GetCommentsByWeatherIdAsync(Guid weatherId)
    {
        return await _dbContext
            .Comments
                
            .Include(c => c.Author)
            .Include(c => c.Weather)
                
            .Where(c => c.WeatherId == weatherId)
            
            .ToListAsync();
    }
    
}