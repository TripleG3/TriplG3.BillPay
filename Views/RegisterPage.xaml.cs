namespace TripleG3.BillPay.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage(RegisterViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}