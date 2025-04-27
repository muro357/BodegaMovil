using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases;
using BodegaMovil.UseCases.DTO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using BodegaMovil.Views;
//using MetalPerformanceShadersGraph;
using BodegaMovil.CoreBusiness.Enums;
using Microsoft.Maui;
using System.Text.Json;
using Microsoft.Maui.Controls.PlatformConfiguration;
using BodegaMovil.Services.Settings.Preferences;
using BodegaMovil.UseCases.Interfaces.Services;

namespace BodegaMovil.ViewModels
{
    public partial class ListaArticulosViewModel : ObservableObject
    {
        private readonly GetPedidoSurtirUseCase _getPedido;
        private readonly DepurarUseCase _depurar;
        private readonly GetArticulosUseCase _getArticulos;
        private readonly ContemplarExistenciaUseCase _contemplar;
        private readonly SurtirUseCase _surtir;
        private readonly ISetting _settings;
        private readonly FinalizarSurtidoUseCase _finalizar;
        private ObservableCollection<PedidoDetalleDTO> _pedidosDetalle;
        private PedidoDTO _pedido;
        private List<string> _listaOrdenarPor;
        private bool _mostrarArtsSurtidos;
        private bool _mostrarSurtidosCeros;
        private OrdenarPor _ordenarPor;
        private string _filtro;
        private string _tara;
        [ObservableProperty] private string _placeholder;
        [ObservableProperty] private string _placeholdercolor;
        [ObservableProperty] private string _user;
        private JsonSerializerOptions _serializerOptions;

        public ListaArticulosViewModel(GetPedidoSurtirUseCase getPedido, DepurarUseCase depurar, GetArticulosUseCase getArticulos, ContemplarExistenciaUseCase contemplar, SurtirUseCase surtir, FinalizarSurtidoUseCase finalizar, ISetting settings)
        {
            _getPedido = getPedido;
            _depurar = depurar;
            _getArticulos = getArticulos;
            _contemplar = contemplar;
            _surtir = surtir;
            _settings = settings;
            _pedidosDetalle = new ObservableCollection<PedidoDetalleDTO>();

            _listaOrdenarPor = new List<string>(Enum.GetNames(typeof(OrdenarPor))); 
            _tara = "Tara";
            _mostrarArtsSurtidos = false;
            _mostrarSurtidosCeros = false;

            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }

        
        public ObservableCollection<PedidoDetalleDTO> ListaArticulos
        {
            get => _pedidosDetalle;
        }

        public List<string>ListaOrdenarPor
        {
            get => _listaOrdenarPor;
        }

        
        private string _ordenar;
        public string Ordenar
        {
            get => _ordenar;
            set 
            {
                SetProperty(ref _ordenar, value);
                _ordenarPor = (OrdenarPor)Enum.Parse(typeof(OrdenarPor), _ordenar);
                ShowArticulos();
            }
        }

        public bool MostrarArtsSurtidos
        {
            get => _mostrarArtsSurtidos;
            set 
            { 
                SetProperty(ref _mostrarArtsSurtidos, value);
                ShowArticulos();
            }
        }

        public bool MostrarSurtidosEnCeros
        {
            get => _mostrarSurtidosCeros;
            set
            {
                SetProperty(ref _mostrarSurtidosCeros, value);
                ShowArticulos();
            }
        }

        
        public string Filtro
        {
            get => _filtro;
            set
            {
                SetProperty(ref _filtro, value);
                ShowArticulos(_filtro.ToUpper());
            }
        }
        public string Tara { get => _tara; }
        public async Task AsignarTara(string tara)
        {
            _tara = tara == "0" ? "Tara" : tara;
        }
        
        public async Task LoadArticulos(string folio, int id_area_surtir)
        {
            var id_cedis = int.Parse(await _settings.GetStoreIdAsync());
            _pedido = await _getPedido.ExecuteAsync(id_cedis, folio, id_area_surtir);     
            
            //Se asignó el dato en LoginViewModel
            _user = await SecureStorage.Default.GetAsync("user");

            await ShowArticulos();
        }

        public async Task ShowArticulos(string criterio = "")
        {
            if (_pedido == null)
                return;

            if (_pedido.PedidoDetalle == null && _pedido.PedidoDetalle.Count == 0)
                return;

            _pedidosDetalle.Clear();

            _pedido.SurtidoPor = _user; //Se asignó en LoadArticulos
            _pedido.Ordenar = _ordenarPor;
            _pedido.MostrarSurtidosEnCeros = _mostrarSurtidosCeros;

            var lista = _mostrarArtsSurtidos ? _pedido.ArticulosSurtidos : _pedido.ArticulosPorSurtir;

            if (!string.IsNullOrWhiteSpace(criterio))
            {
                lista = lista.Where(p => p.SKU.ToUpper() == criterio || p.Descripcion.ToUpper().Contains(criterio)).ToList();
            }
            
            foreach (var item in lista)
            {
                _pedidosDetalle.Add(item);
            }
        }

        [RelayCommand]
        public async Task SurtirPedido(PedidoDetalleDTO item)
        {
            item.CantidadSurtida = item.CantidadPedida;
            var res = await _surtir.ExecuteAsync(_pedido, item);

            if (!res)
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "No se puede actualizar", "OK");
            }
            else 
            {
                await ShowArticulos();            
            }
        }

        [RelayCommand]
        public async Task CapturarArticulo(PedidoDetalleDTO item)
        {
            var pedido = JsonSerializer.Serialize(_pedido, _serializerOptions);
            var linea = JsonSerializer.Serialize(item, _serializerOptions);
            await Shell.Current.GoToAsync($"{nameof(CapturaArticuloPage)}?pedido={pedido}&linea={linea}");
        }

        [RelayCommand]
        public async Task BuscarArticulo()
        {
            var pedido = JsonSerializer.Serialize(_pedido, _serializerOptions);
            await Shell.Current.GoToAsync($"{nameof(BuscarArticuloPage)}?pedido={pedido}");
        }


        [RelayCommand]
        public async Task Depurar()
        {
            //await depurar.ExecuteAsync(pedido);
            var seleccionados = _pedidosDetalle.Where(p => p.Elegido).Select(p => p.SKU).ToList();

            if (seleccionados.Count > 0)
            {
                var ok = await _depurar.ExecuteAsync(_pedido);

                if (ok)
                {
                    string mensaje = seleccionados.Any()
                        ? $"Seleccionados:\n{string.Join("\n", seleccionados)}"
                        : "Ningún producto seleccionado.";

                    await Application.Current.MainPage.DisplayAlert("Depurar", mensaje, "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Depurar", "Error al depurar", "OK");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Depurar", "No hay productos seleccionados", "OK");
                return;
            }
                
        }

        [RelayCommand]
        public async Task Contemplar()
        {
            var porSurtir = _pedido.ArticulosPorSurtir.Count();

            if (porSurtir > 0)
            {
                await _contemplar.ExecuteAsync(_pedido);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Contemplar", "No hay artículos por surtir", "OK");
            }
        }

        [RelayCommand]
        public async Task Finalizar()
        {
            var porSurtir = _pedido.ArticulosPorSurtir.Count();

            if(porSurtir > 0)
            {
                var mensaje = $"No se puede finalizar el pedido, hay {porSurtir} artículos por surtir.";
                await Application.Current.MainPage.DisplayAlert("Finalizar", mensaje, "OK");
            }
            else
            {
                var res = await _finalizar.ExecuteAsync(_pedido);
                if (res)
                {
                    await Application.Current.MainPage.DisplayAlert("Finalizar", "Pedido finalizado", "OK");
                    await Shell.Current.GoToAsync($"{nameof(ListaPedidosPage)}?reset=1");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Finalizar", "Error al finalizar el pedido", "OK");
                }
            }


        }
    }
}
