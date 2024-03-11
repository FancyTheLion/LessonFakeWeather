using FakeWeatherBackend.Services.Abstract;

namespace FakeWeatherBackend.Services.Implementations;

public class ResizeService : IResizeService
{
    public async Task<byte[]> ResizeImageAsync(byte[] content, string type)
    {
        return content;
    }
}