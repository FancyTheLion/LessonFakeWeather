using FakeWeatherBackend.Models.API.Responses.Files;
using FakeWeatherBackend.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace FakeWeatherBackend.Controllers;

/// <summary>
/// Controller to work with files
/// </summary>
[ApiController]
public class FilesController : ControllerBase
{
    private readonly IFilesService _filesService;

    public FilesController
    (
        IFilesService filesService
    )
    {
        _filesService = filesService;
    }
    
    /// <summary>
    /// File upload
    /// </summary>
    [Route("api/Files/Upload")]
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<FileUploadedResponse>> UploadAsync(IFormFile file)
    {
        try
        {
            return Ok
            (
                new FileUploadedResponse
                (
                    (await _filesService.UploadFileAsync(file)).ToDto()
                )
            );
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}