using Microsoft.Extensions.Logging;
using TripleG3.BillPay.Services;
using TripleG3.BillPay.ViewModels;
using TripleG3.BillPay.Views;

namespace TripleG3.BillPay;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		var appDataPath = Path.Combine(FileSystem.AppDataDirectory, "Data");
		builder.Services.AddSingleton<IUserService>(_ => new JsonFileUserService(Path.Combine(appDataPath, "Users")));
		builder.Services.AddSingleton<ISessionService>(_ => new JsonFileSessionService(Path.Combine(appDataPath, "session.json")));

		builder.Services.AddTransient<LoginViewModel>();
		builder.Services.AddTransient<RegisterViewModel>();
		builder.Services.AddTransient<HomeViewModel>();

		builder.Services.AddTransient<LoginPage>();
		builder.Services.AddTransient<RegisterPage>();
		builder.Services.AddTransient<HomePage>();
		builder.Services.AddTransient<AuthHeaderView>();

		return builder.Build();
	}
}
