using TeendokLista.MAUI.ViewModels;

namespace TeendokLista.MAUI.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }
}