using FakeWeatherBackend.Models.API.DTOs.Comments;
using FakeWeatherBackend.Models.API.DTOs.Users;
using FakeWeatherBackend.Models.API.Requests.Comments;
using FakeWeatherBackend.Models.API.Responses.Comments;
using FakeWeatherBackend.Models.API.Responses.Settings;
using FakeWeatherBackend.Models.Comments;
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
    private readonly IAccountsService _accountsService;

    public CommentsController
    (
        ICommentsService commentsService,
        IAccountsService accountsService
    )
    {
        _commentsService = commentsService;
        _accountsService = accountsService;
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
    
    /// <summary>
    /// Add new comment to backend
    /// </summary>
    [HttpPost]
    [Route("api/Comments/Add")]
    public async Task<ActionResult<AddCommentResponse>> AddCommentAsync([FromBody]AddCommentRequest addCommentRequest)
    {
        var loggedInUser = await _accountsService.GetUserByLoginAsync(User.Identity.Name);

        var commentToAdd = new CommentToAdd
        (
            addCommentRequest.CommentToAdd.Content,
            addCommentRequest.CommentToAdd.WeatherId,
            loggedInUser.Id
        );
        
        return Ok
        (
            new AddCommentResponse
            (
                (await _commentsService.AddCommentAsync(commentToAdd)).ToDto()
            )
        );
    }
}