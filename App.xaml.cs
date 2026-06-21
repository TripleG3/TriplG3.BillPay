namespace TripleG3.BillPay;

public partial class App : Application
{
	private readonly ISessionService _sessionService;

	public App(ISessionService sessionService)
	{
		_sessionService = sessionService;
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		var shell = new AppShell();
		var window = new Window(shell);

		window.Created += async (_, _) =>
		{
			var session = await _sessionService.GetCurrentSessionAsync();
			await shell.GoToAsync(session?.IsLoggedIn is true ? "//home" : "//login");
		};

		return window;
	}
}