using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BodegaMovil.UseCases.DTO;
using BodegaMovil.ViewModels;
using System.Text.Json;

namespace BodegaMovil.Views;

[QueryProperty(nameof(Folio), "folio")]
[QueryProperty(nameof(ID_AreaSurtir), "id_area_surtir")]
public partial class ListaArticulosPage : ContentPage
{
	public ListaArticulosPage(ListaArticulosViewModel listaArticulos)
	{
        InitializeComponent();
        _listaArticulos = listaArticulos;
        this.BindingContext = _listaArticulos;
    }

    private string tara;
    //private ShowPedidoDTO _pedido;
    private readonly ListaArticulosViewModel _listaArticulos;
    private string _folio;
    private int _id_areaSurtir;

    public string Folio
    {
        set
        {
            _folio = value;
        }
    }

    public string ID_AreaSurtir
    {
       
        set
        {
            var id_area = value;
            if (int.TryParse(id_area, out int id))
            {
                _id_areaSurtir = id;
            }
            else
            {
                _id_areaSurtir = 0;
            }
        }
    }

    public string Tara
    {
        get => tara;
        set => tara = value;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await LoadArticulos();
        await AsignarTara();
    }

    private async Task LoadArticulos()
    {
         
        await _listaArticulos.LoadArticulos(_folio, _id_areaSurtir);
    }

    private async Task AsignarTara()
    {
        await _listaArticulos.AsignarTara(tara);
    }

}