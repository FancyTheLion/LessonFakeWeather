using System.Net.Http.Headers;
using FakeWeatherBackend.Models.API.Responses.Files;
using FakeWeatherBackend.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EntityTagHeaderValue = Microsoft.Net.Http.Headers.EntityTagHeaderValue;

namespace FakeWeatherBackend.Controllers;

/// <summary>
/// Controller to work with files
/// </summary>
[Authorize]
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
        if (!await _filesService.IsFileImageAsync(file))
        {
            return BadRequest("Only images are allowed!");
        }

        if (!await _filesService.IsFileSizeCorrectAsync(file))
        {
            return BadRequest("File too big!");
        }
        
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
    
    /// <summary>
    /// Download the file
    /// </summary>
    [AllowAnonymous]
    [Route("api/Files/Download/{fileId}")]
    [HttpGet]
    public async Task<ActionResult> DownloadAsync(Guid fileId)
    {
        try
        {
            var result = await _filesService.GetFileAsync(fileId);

            return File
            (
                result.Content,
                result.Type,
                result.Name,
                result.LastModifiedTime,
                new EntityTagHeaderValue($"\"{ result.Hash }\"")
            );
        }
        catch (Exception)
        {
            return NotFound(); // Treating errors as not found too
        }
    }
}