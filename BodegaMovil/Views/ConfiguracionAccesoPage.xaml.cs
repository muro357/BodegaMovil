using BodegaMovil.ViewModels;

namespace BodegaMovil.Views;

public partial class ConfiguracionAccesoPage : ContentPage
{
    private readonly ConfiguracionAccesoViewModel accesoViewModel;

    public ConfiguracionAccesoPage(ConfiguracionAccesoViewModel accesoViewModel)
	{
		InitializeComponent();
        this.accesoViewModel = accesoViewModel;
        this.BindingContext = accesoViewModel;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }
}