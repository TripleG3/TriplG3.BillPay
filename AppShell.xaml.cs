using TripleG3.BillPay.Views;

namespace TripleG3.BillPay;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("register", typeof(RegisterPage));
	}
}
