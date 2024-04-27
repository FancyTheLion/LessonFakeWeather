using System.ComponentModel.DataAnnotations;
using FakeWeatherBackend.DAO.Models.Authentification;

namespace FakeWeatherBackend.DAO.Models.Comments;

public class CommentDbo
{
    /// <summary>
    /// Comment ID
    /// </summary>
    [Key]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Comment author
    /// </summary>
    public UserDbo Author { get; set; }
    
    /// <summary>
    /// Comment posting time
    /// </summary>
    public DateTime Timestamp { get; set; }
    
    /// <summary>
    /// Content of the comment
    /// </summary>
    public string Content { get; set; }
    
    /// <summary>
    /// Foreign key to parent weather
    /// </summary>
    public Guid WeatherId  { get; set; }
    
    /// <summary>
    /// Parent weather
    /// </summary>
    public WeatherDbo Weather { get; set; }
}