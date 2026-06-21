namespace TripleG3.BillPay.Services;

public sealed class JsonFileUserService(string basePath) : IUserService
{
    private readonly JsonSerializerOptions _serializerOptions = new() { WriteIndented = true };

    private string UserPath(string username) => Path.Combine(basePath, username + ".json");

    public async Task<User?> GetUserAsync(string username)
    {
        Directory.CreateDirectory(basePath);

        var path = UserPath(username);
        if (!File.Exists(path))
        {
            return null;
        }

        var json = await File.ReadAllTextAsync(path);
        return JsonSerializer.Deserialize<User>(json);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        Directory.CreateDirectory(basePath);

        var users = new List<User>();
        foreach (var file in Directory.GetFiles(basePath, "*.json"))
        {
            var json = await File.ReadAllTextAsync(file);
            var user = JsonSerializer.Deserialize<User>(json);
            if (user is not null)
            {
                users.Add(user);
            }
        }

        return users;
    }

    public async Task SaveUserAsync(User user)
    {
        Directory.CreateDirectory(basePath);

        var path = UserPath(user.Username);
        var json = JsonSerializer.Serialize(user, _serializerOptions);
        await File.WriteAllTextAsync(path, json);
    }

    public async Task AddBillOrPayAsync(string username, BillOrPay billOrPay)
    {
        var user = await GetRequiredUserAsync(username);
        var updatedUser = user with { BillOrPays = new List<BillOrPay>(user.BillOrPays) { billOrPay } };
        await SaveUserAsync(updatedUser);
    }

    public async Task UpdateBillOrPayAsync(string username, BillOrPay billOrPay)
    {
        var user = await GetRequiredUserAsync(username);
        var billOrPays = user.BillOrPays
            .Select(existing => existing.Id == billOrPay.Id ? billOrPay : existing)
            .ToList();

        await SaveUserAsync(user with { BillOrPays = billOrPays });
    }

    public async Task DeleteBillOrPayAsync(string username, string billId)
    {
        var user = await GetRequiredUserAsync(username);
        var billOrPays = user.BillOrPays
            .Where(existing => existing.Id != billId)
            .ToList();

        await SaveUserAsync(user with { BillOrPays = billOrPays });
    }

    private async Task<User> GetRequiredUserAsync(string username) =>
        await GetUserAsync(username) ?? throw new InvalidOperationException($"User '{username}' was not found.");
}
