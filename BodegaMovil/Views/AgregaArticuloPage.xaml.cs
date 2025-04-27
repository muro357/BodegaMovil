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
    private JsonSerializerOptions _serializerOptions;
    public AgregaArticuloPage(ArticuloViewModel articuloView)
    {
        _serializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };

        InitializeComponent();
        _articuloView = articuloView;
        this.BindingContext = _articuloView;
    }

    public string Pedido
    {
        set
        {
            _pedido = JsonSerializer.Deserialize<PedidoDTO>(value, _serializerOptions);
        }
    }

    public string Art
    {
        set
        {
            _art = JsonSerializer.Deserialize<ArticuloDTO>(value, _serializerOptions); ;
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