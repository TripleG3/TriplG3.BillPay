using System.Windows.Input;

namespace TripleG3.BillPay.ViewModels;

public sealed class RegisterViewModel(IUserService userService, ISessionService sessionService) : AuthViewModelBase
{
    private string _username = string.Empty;
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;
    private string _email = string.Empty;

    public string Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
    }

    public string FirstName
    {
        get => _firstName;
        set => SetProperty(ref _firstName, value);
    }

    public string LastName
    {
        get => _lastName;
        set => SetProperty(ref _lastName, value);
    }

    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    public ICommand RegisterCommand => new Command(async () => await RegisterAsync(), () => !IsBusy);
    public ICommand GoToLoginCommand => new Command(async () => await Shell.Current.GoToAsync("login"));

    private async Task RegisterAsync()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(FirstName))
        {
            ErrorMessage = "Username and first name are required.";
            return;
        }

        IsBusy = true;
        ErrorMessage = string.Empty;

        try
        {
            var user = new User(Username, FirstName, LastName, Email, string.Empty, null, new List<BillOrPay>(), new List<string>());
            await userService.SaveUserAsync(user);
            await sessionService.SetCurrentSessionAsync(new UserSession(user.Username, true));
            await Shell.Current.GoToAsync("//home");
        }
        finally
        {
            IsBusy = false;
        }
    }
}