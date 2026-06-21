using System.Windows.Input;

namespace TripleG3.BillPay.ViewModels;

public sealed class LoginViewModel(IUserService userService, ISessionService sessionService) : AuthViewModelBase
{
    private string _username = string.Empty;

    public string Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
    }

    public ICommand LoginCommand => new Command(async () => await LoginAsync(), () => !IsBusy);
    public ICommand GoToRegisterCommand => new Command(async () => await Shell.Current.GoToAsync("register"));

    private async Task LoginAsync()
    {
        if (string.IsNullOrWhiteSpace(Username))
        {
            ErrorMessage = "Enter a username.";
            return;
        }

        IsBusy = true;
        ErrorMessage = string.Empty;

        try
        {
            var user = await userService.GetUserAsync(Username);
            if (string.IsNullOrWhiteSpace(user?.Username))
            {
                ErrorMessage = "User not found.";
                return;
            }

            await sessionService.SetCurrentSessionAsync(new UserSession(user.Username, true));
            await Shell.Current.GoToAsync("//home");
        }
        finally
        {
            IsBusy = false;
        }
    }
}