namespace TripleG3.BillPay.Services;

public interface ISessionService
{
    Task<UserSession?> GetCurrentSessionAsync();
    Task SetCurrentSessionAsync(UserSession session);
    Task ClearCurrentSessionAsync();
}