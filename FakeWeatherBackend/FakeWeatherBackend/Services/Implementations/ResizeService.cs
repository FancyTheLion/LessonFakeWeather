using FakeWeatherBackend.Models.Settings;
using FakeWeatherBackend.Services.Abstract;
using ImageMagick;
using Microsoft.Extensions.Options;

namespace FakeWeatherBackend.Services.Implementations;

public class ResizeService : IResizeService
{
    private readonly PhotoSizeSettings _photoSizeSettings;

    public ResizeService
    (
        IOptions<PhotoSizeSettings> photoSizeSettingsRef
    )
    {
        _photoSizeSettings = photoSizeSettingsRef.Value;
    }

    public async Task<byte[]> ResizeImageAsync(byte[] content, string type)
    {
        var imagesCollection = new MagickImageCollection();
        imagesCollection.Read(content);
        
        foreach (var image in imagesCollection)
        {
            image.Resize(_photoSizeSettings.Width, _photoSizeSettings.Height);
        }
        
        using (var saveStream = new MemoryStream())
        {
            await imagesCollection.WriteAsync(saveStream);

            return saveStream.ToArray();
        }
    }
}