using BodegaMovil.UseCases.DTO;
using BodegaMovil.ViewModels;
using System.Text.Json;


namespace BodegaMovil.Views;


[QueryProperty(nameof(Pedido), "pedido")]
[QueryProperty(nameof(Linea), "linea")]

public partial class CapturaArticuloPage : ContentPage
{
    private readonly ArticuloViewModel _articuloView;
    private PedidoDTO _pedido;
    private PedidoDetalleDTO _linea;    
    public CapturaArticuloPage(ArticuloViewModel articuloView)
	{
        InitializeComponent();
        _articuloView = articuloView;
        this.BindingContext = _articuloView;
    }

    public PedidoDTO Pedido
    {
        set => _pedido = value;
    }

    public PedidoDetalleDTO Linea
    {
        set => _linea = value;
    }

    private async Task LoadArticulo(PedidoDTO pedido, PedidoDetalleDTO linea)
    {
        await _articuloView.LoadArticulo(pedido,linea);
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();


        await LoadArticulo(_pedido, _linea);
        
    }
}