using FakeWeatherBackend.Models.API.DTOs.Comments;
using FakeWeatherBackend.Models.API.Responses.Comments;
using FakeWeatherBackend.Models.API.Responses.Settings;
using FakeWeatherBackend.Services.Abstract;
using FakeWeatherBackend.Services.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FakeWeatherBackend.Controllers;

/// <summary>
/// Controller for working with weather
/// </summary>
[Authorize]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly ICommentsService _commentsService;

    public CommentsController
    (
        ICommentsService commentsService
    )
    {
        _commentsService = commentsService;
    }
    
    /// <summary>
    /// Get comments
    /// </summary>
    [AllowAnonymous]
    [HttpGet]
    [Route("api/Comments/ByWeatherId/{id}")]
    public async Task<ActionResult<CommentsListResponse>> ByWeatherId(Guid id)
    {
        return Ok
        (
            new CommentsListResponse
            (
                (await _commentsService.GetCommentsByWeatherIdAsync(id))
                .Select(cm => cm.ToDto()) 
                .ToList()
            )
        );
    }
}