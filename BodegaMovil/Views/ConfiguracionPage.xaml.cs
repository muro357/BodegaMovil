using BodegaMovil.ViewModels;

namespace BodegaMovil.Views;

public partial class ConfiguracionPage : ContentPage
{
    private readonly ConfiguracionViewModel _configuracion;

    public ConfiguracionPage(ConfiguracionViewModel configuracion)
	{
		InitializeComponent();
        _configuracion = configuracion;
        this.BindingContext = _configuracion;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        //await CargarConfiguracion();
    }

    private async Task CargarConfiguracion()
    {
        await _configuracion.CargarConfiguracionGuardada();
    }
}