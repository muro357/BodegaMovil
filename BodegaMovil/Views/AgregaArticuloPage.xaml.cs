using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases.DTO;
using BodegaMovil.ViewModels;
using System.Text.Json;

namespace BodegaMovil.Views;

[QueryProperty(nameof(Pedido), "pedido")]
[QueryProperty(nameof(Art), "art")]
public partial class AgregaArticuloPage : ContentPage
{
    private readonly ArticuloViewModel _articuloView;
        
    private PedidoDTO _pedido;
    private ArticuloDTO _art;
    
    public AgregaArticuloPage(ArticuloViewModel articuloView)
    {
        InitializeComponent();
        _articuloView = articuloView;
        this.BindingContext = _articuloView;
    }

    public PedidoDTO Pedido
    {
        set
        {
            _pedido = value;
        }
    }

    public ArticuloDTO Art
    {
        set
        {
            _art = value; ;
        }
    }

    private async Task LoadArticulo(PedidoDTO pedido, ArticuloDTO art)
    {
        await _articuloView.LoadArticulo(pedido, art);
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await LoadArticulo(_pedido, _art);
    }
}