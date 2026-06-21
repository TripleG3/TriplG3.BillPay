using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using TripleG3.BillPay.Models;
using TripleG3.BillPay.Services;

namespace TripleG3.BillPay.Services;

public class JsonFileUserService : IUserService
{
    private readonly string _basePath;
    public JsonFileUserService(string basePath)
    {
        _basePath = basePath;
        Directory.CreateDirectory(_basePath);
    }

    private string UserPath(string username) => Path.Combine(_basePath, username + ".json");

    public async Task<User> GetUserAsync(string username)
    {
        var path = UserPath(username);
        if (!File.Exists(path)) return new User { Username = username };
        var json = await File.ReadAllTextAsync(path);
        return JsonSerializer.Deserialize<User>(json) ?? new User { Username = username };
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var users = new List<User>();
        foreach (var file in Directory.GetFiles(_basePath, "*.json"))
        {
            var json = await File.ReadAllTextAsync(file);
            var user = JsonSerializer.Deserialize<User>(json);
            if (user != null) users.Add(user);
        }
        return users;
    }

    public async Task SaveUserAsync(User user)
    {
        var path = UserPath(user.Username);
        var json = JsonSerializer.Serialize(user, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(path, json);
    }

    public async Task AddBillOrPayAsync(string username, BillOrPay billOrPay)
    {
        var user = await GetUserAsync(username);
        user.BillOrPays.Add(billOrPay);
        await SaveUserAsync(user);
    }

    public async Task UpdateBillOrPayAsync(string username, BillOrPay billOrPay)
    {
        var user = await GetUserAsync(username);
        var idx = user.BillOrPays.FindIndex(b => b.Id == billOrPay.Id);
        if (idx >= 0) user.BillOrPays[idx] = billOrPay;
        await SaveUserAsync(user);
    }

    public async Task DeleteBillOrPayAsync(string username, string billId)
    {
        var user = await GetUserAsync(username);
        user.BillOrPays.RemoveAll(b => b.Id == billId);
        await SaveUserAsync(user);
    }
}
