using FakeWeatherBackend.DAO.Models;
using FakeWeatherBackend.Mappers.Abstract;
using File = FakeWeatherBackend.Models.Files.File;

namespace FakeWeatherBackend.Mappers.Implementations;

public class FilesMapper : IFilesMapper
{
    public File Map(FileDbo file)
    {
        if (file == null)
        {
            return null;
        }

        return new File()
        {
            Id = file.Id,
            Name = file.Name,
            Type = file.Type,
            Content = file.Content,
            Hash = file.Hash,
            LastModifiedTime = file.LastModifiedTime
        };

    }

    public IList<File> Map(IReadOnlyCollection<FileDbo> files)
    {
        if (files == null)
        {
            return null;
        }

        return files
            .Select(f => Map(f))
            .ToList();
    }

    public FileDbo Map(File file)
    {
        if (file == null)
        {
            return null;
        }

        return new FileDbo()
        {
            Id = file.Id,
            Content = file.Content,
            Name = file.Name,
            Type = file.Type,
            LastModifiedTime = file.LastModifiedTime,
            Hash = file.Hash,
            PhotosForWeathers = new List<WeatherDbo>() //It is not specified for what weather conditions this file is registered
        };
    }

    public IList<FileDbo> Map(IReadOnlyCollection<File> files)
    {
        if (files == null)
        {
            return null;
        }

        return files
            .Select(f => Map(f))
            .ToList();
    }
}