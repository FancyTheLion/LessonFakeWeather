using FakeWeatherBackend.DAO.Models;
using File = FakeWeatherBackend.Models.Files.File;

namespace FakeWeatherBackend.Mappers.Abstract;

public interface IFilesMapper
{
    /// <summary>
    /// Take the file obtained from the database and transform it into a file that the rest of the program uses
    /// </summary>
    File Map(FileDbo file);
    
    /// <summary>
    /// Multiple weathers DBOs to list of models(Not used but needed)
    /// </summary>
    IList<File> Map(IReadOnlyCollection<FileDbo> files);
    
    /// <summary>
    /// We take the file that the program uses and turn it into a file that can be put into the database
    /// </summary>
    FileDbo Map(File file);
    
    /// <summary>
    /// Multiple weather models to list of DBOs(Not used but needed)
    /// </summary>
    IList<FileDbo> Map(IReadOnlyCollection<File> files);
}