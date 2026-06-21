namespace TripleG3.BillPay.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}