using FakeWeatherBackend.DAO.Models;

namespace FakeWeatherBackend.DAO.Abstract;

/// <summary>
/// DAO to work with files
/// </summary>
public interface IFilesDao
{
    #region Create / Update
    
    /// <summary>
    /// Save file to database
    /// </summary>
    Task<FileDbo> SaveFileAsync(FileDbo file);
    
    #endregion
}