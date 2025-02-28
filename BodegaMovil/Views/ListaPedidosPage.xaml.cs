using BodegaMovil.UseCases.DTO;
using BodegaMovil.ViewModels;
using System.Text.Json;

namespace BodegaMovil.Views;

[QueryProperty(nameof(UserJSON), "user")]
public partial class ListaPedidosPage : ContentPage
{
    private readonly ListaPedidosViewModel listaPedidosView;
    private UsuarioDTO _userAutorized;
    private JsonSerializerOptions _serializerOptions;

    public ListaPedidosPage(ListaPedidosViewModel listaPedidosView)
	{
        _serializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };

        InitializeComponent();
        
        this.listaPedidosView = listaPedidosView;
        this.BindingContext = listaPedidosView;
    }

    public string UserJSON
    { 
		set
		{
            _userAutorized = JsonSerializer.Deserialize<UsuarioDTO>(value, _serializerOptions);
            LoadPedidos(_userAutorized);
        }
	}

    private async void LoadPedidos(UsuarioDTO user)
    {
        await this.listaPedidosView.AsignarUsuario(user);
    }
}