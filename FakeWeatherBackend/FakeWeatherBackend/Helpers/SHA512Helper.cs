using System.Security.Cryptography;

namespace FakeWeatherBackend.Helpers;

/// <summary>
/// Helper to work with SHA512 hashes
/// TODO: Remove me, test
/// </summary>
public static class SHA512Helper
{
    /// <summary>
    /// Calculate SHA512 hash of file contents
    /// </summary>
    public static string CalculateSHA512(byte[] data)
    {
        var calculator = SHA512.Create();
        var resultBytes = calculator.ComputeHash(data);

        return Convert.ToBase64String(resultBytes, Base64FormattingOptions.None);
    }
}