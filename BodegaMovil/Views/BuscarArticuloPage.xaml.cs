using BodegaMovil.UseCases.DTO;
using BodegaMovil.ViewModels;
using System.Text.Json;

namespace BodegaMovil.Views;

[QueryProperty(nameof(Pedido), "pedido")]
public partial class BuscarArticuloPage : ContentPage
{
    private readonly BuscarArticuloViewModel _buscarView;
	private PedidoDTO _pedido;
	private PedidoDetalleDTO _linea;

    public BuscarArticuloPage(BuscarArticuloViewModel buscarView)
	{
        InitializeComponent();
        _buscarView = buscarView;
        this.BindingContext = _buscarView;
    }

    public PedidoDTO Pedido
    {
        set => _pedido = value;
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