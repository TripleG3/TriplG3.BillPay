namespace TripleG3.BillPay.Services;

public sealed class JsonFileSessionService(string filePath) : ISessionService
{
    public async Task<UserSession?> GetCurrentSessionAsync()
    {
        if (!File.Exists(filePath))
        {
            return null;
        }

        var json = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<UserSession>(json);
    }

    public async Task SetCurrentSessionAsync(UserSession session)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
        var json = JsonSerializer.Serialize(session, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(filePath, json);
    }

    public Task ClearCurrentSessionAsync()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        return Task.CompletedTask;
    }
}