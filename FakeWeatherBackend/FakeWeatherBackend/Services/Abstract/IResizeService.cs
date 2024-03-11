using Microsoft.EntityFrameworkCore;

namespace FakeWeatherBackend.Services.Abstract;

/// <summary>
/// Allows you to resize photos
/// </summary>
public interface IResizeService
{
    /// <summary>
    /// Method that changes the file size
    /// </summary>
    Task<byte[]> ResizeImageAsync(byte[] content, string type);
}