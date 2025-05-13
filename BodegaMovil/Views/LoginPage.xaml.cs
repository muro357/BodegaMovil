using BodegaMovil.ViewModels;

namespace BodegaMovil.Views;

public partial class LoginPage : ContentPage
{
    private readonly LoginViewModel _loginViewModel;
    public LoginPage(LoginViewModel loginViewModel)
	{
		InitializeComponent();

        this._loginViewModel = loginViewModel;
        this.BindingContext = this._loginViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        this.UsuarioEntry.Text=string.Empty;
        this.PasswordEntry.Text = string.Empty;
        this.UsuarioEntry.Focus();
    }
}