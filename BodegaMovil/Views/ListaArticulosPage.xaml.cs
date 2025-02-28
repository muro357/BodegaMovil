using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BodegaMovil.UseCases.DTO;
using BodegaMovil.ViewModels;

namespace BodegaMovil.Views;

[QueryProperty(nameof(Folio),"folio")]
//[QueryProperty(nameof(ID_tienda), "id_tienda")]
public partial class ListaArticulosPage : ContentPage
{
	public ListaArticulosPage(ListaArticulosViewModel listaArticulos)
	{
		InitializeComponent();
        this.listaArticulos = listaArticulos;
        this.BindingContext = listaArticulos;
        this.ID_tienda = 90;
    }


    private string folio;
    private int id_tienda;
    private readonly ListaArticulosViewModel listaArticulos;


    public string Folio
    {
        set => folio = value;
    }

    public int ID_tienda
    {
        set => id_tienda = value;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await LoadArticulos();
    }

    private async Task LoadArticulos()
    {
        await this.listaArticulos.LoadArticulos(folio,id_tienda);
    }
}