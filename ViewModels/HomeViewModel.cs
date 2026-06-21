using System.Windows.Input;

namespace TripleG3.BillPay.ViewModels;

public sealed class HomeViewModel(ISessionService sessionService) : AuthViewModelBase
{
    private string _welcomeMessage = "Welcome";

    public string WelcomeMessage
    {
        get => _welcomeMessage;
        set => SetProperty(ref _welcomeMessage, value);
    }

    public ICommand LogoutCommand => new Command(async () => await LogoutAsync());

    public async Task InitializeAsync()
    {
        var session = await sessionService.GetCurrentSessionAsync();
        WelcomeMessage = session is null ? "Welcome" : $"Welcome, {session.Username}";
    }

    private async Task LogoutAsync()
    {
        await sessionService.ClearCurrentSessionAsync();
        await Shell.Current.GoToAsync("//login");
    }
}