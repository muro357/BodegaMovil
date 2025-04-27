using BodegaMovil.ViewModels;
namespace BodegaMovil.Views;

[QueryProperty(nameof(Folio), "folio")]
public partial class ListaTarasPage : ContentPage
{
    private readonly ListaTarasViewModel _listaTarasViewModel;
    private string _folio;

    public ListaTarasPage(ListaTarasViewModel listaTarasViewModel)
	{
		InitializeComponent();
        this._listaTarasViewModel = listaTarasViewModel;
        this.BindingContext = _listaTarasViewModel;
    }

    public string Folio
    {
        set => _folio = value;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await LoadTaras();
    }

    private async Task LoadTaras()
    {
        await this._listaTarasViewModel.LoadTaras(_folio);
    }
}