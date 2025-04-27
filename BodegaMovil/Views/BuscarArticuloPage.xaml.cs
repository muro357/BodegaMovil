using BodegaMovil.UseCases.DTO;
using BodegaMovil.ViewModels;
using System.Text.Json;

namespace BodegaMovil.Views;

[QueryProperty(nameof(Pedido), "pedido")]
public partial class BuscarArticuloPage : ContentPage
{
    private readonly BuscarArticuloViewModel _buscarView;
	private JsonSerializerOptions _serializerOptions;
	private PedidoDTO _pedido;
	private PedidoDetalleDTO _linea;

    public BuscarArticuloPage(BuscarArticuloViewModel buscarView)
	{
        _serializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };

        InitializeComponent();
        _buscarView = buscarView;
        this.BindingContext = _buscarView;
    }

    public string Pedido
    {
        set
        {
            _pedido = JsonSerializer.Deserialize<PedidoDTO>(value, _serializerOptions);

        }
    }

    private async Task Load(PedidoDTO pedido)
    {
        
        await _buscarView.Iniciar(pedido);
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Load(_pedido);

    }

}