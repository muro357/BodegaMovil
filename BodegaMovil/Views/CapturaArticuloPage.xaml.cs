using BodegaMovil.UseCases.DTO;
using BodegaMovil.ViewModels;
using System.Text.Json;


namespace BodegaMovil.Views;


[QueryProperty(nameof(Pedido), "pedido")]
[QueryProperty(nameof(Linea), "linea")]

public partial class CapturaArticuloPage : ContentPage
{
    private readonly ArticuloViewModel _articuloView;
    private JsonSerializerOptions _serializerOptions;
    private PedidoDTO _pedido;
    private PedidoDetalleDTO _linea;    
    public CapturaArticuloPage(ArticuloViewModel articuloView)
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

    public string Linea
    {
        set
        {
            _linea = JsonSerializer.Deserialize<PedidoDetalleDTO>(value, _serializerOptions); ;
        }
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