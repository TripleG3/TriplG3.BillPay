using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripleG3.BillPay.Models;

namespace TripleG3.BillPay.Services;

public interface IUserService
{
    Task<User> GetUserAsync(string username);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task SaveUserAsync(User user);
    Task AddBillOrPayAsync(string username, BillOrPay billOrPay);
    Task UpdateBillOrPayAsync(string username, BillOrPay billOrPay);
    Task DeleteBillOrPayAsync(string username, string billId);
}
