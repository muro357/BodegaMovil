using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases;
using BodegaMovil.UseCases.DTO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using BodegaMovil.Views;
using BodegaMovil.CoreBusiness.Enums;
using System.Text.Json;
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
        //private PedidoDTO _pedido;
        private List<string> _listaOrdenarPor;
        private bool _mostrarArtsSurtidos;
        private bool _mostrarSurtidosCeros;
        private OrdenarPor _ordenarPor;
        private string _filtro;
        private string _tara;
        [ObservableProperty] private string _placeholder;
        [ObservableProperty] private string _placeholdercolor;
        [ObservableProperty] private string _user;

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

                TituloListaArts = _mostrarArtsSurtidos ? "Surtidos" : "Por Surtir";


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

        [ObservableProperty]
        private PedidoDTO _pedido;

        [ObservableProperty]
        private string tituloListaArts = "Por Surtir";

        

        public string Tara { get => _tara; }
        public async Task AsignarTara(string tara)
        {
            _tara = tara == "0" ? "Tara" : tara;
        }
        
        public async Task LoadArticulos(string folio, int id_area_surtir)
        {
            var id_cedis = int.Parse(await _settings.GetStoreIdAsync());
            Pedido = await _getPedido.ExecuteAsync(id_cedis, folio, id_area_surtir);   

            //Se asignó el dato en LoginViewModel
            _user = await SecureStorage.Default.GetAsync("user");

            await ShowArticulos();
        }

        public async Task ShowArticulos(string criterio = "")
        {
            if (Pedido == null)
                return;

            if (Pedido.PedidoDetalle == null && Pedido.PedidoDetalle.Count == 0)
                return;

            _pedidosDetalle.Clear();

            Pedido.SurtidoPor = _user; //Se asignó en LoadArticulos
            Pedido.Ordenar = _ordenarPor;
            Pedido.MostrarSurtidosEnCeros = _mostrarSurtidosCeros;

            var lista = _mostrarArtsSurtidos ? Pedido.ArticulosSurtidos : Pedido.ArticulosPorSurtir;

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
            var res = await _surtir.ExecuteAsync(Pedido, item);

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
            try
            {
                var parametros = new Dictionary<string, object>
                {
                    { "pedido", Pedido },
                    { "linea", item }
                };

                await Shell.Current.GoToAsync($"{nameof(CapturaArticuloPage)}",parametros);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al capturar artículo: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        public async Task BuscarArticulo()
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(BuscarArticuloPage)}", 
                    new Dictionary<string, object>
                    {
                        {"Pedido",Pedido }
                    });
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al buscar artículo: {ex.Message}", "OK");
            }
        }


        [RelayCommand]
        public async Task Depurar()
        {
            var seleccionados = _pedidosDetalle.Where(p => p.Elegido).Select(p => p.SKU).ToList();

            if (seleccionados.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Depurar", "No hay productos seleccionados", "OK");
                return;
            }

            bool confirmar = await Application.Current.MainPage.DisplayAlert(
               "Aviso", "¿Esta seguro de depurar?",
               "Sí", "No");

            if (confirmar)
            {
                try
                {
                    await _depurar.ExecuteAsync(Pedido);
                    await ShowArticulos();
                    await Application.Current.MainPage.DisplayAlert("Depurar", "Se ha depurado", "OK");
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"Error al depurar: {ex.Message}", "OK");
                }
            }  
        }

        [RelayCommand]
        public async Task Contemplar()
        {
            var porSurtir = Pedido.ArticulosPorSurtir.Count();
            if (porSurtir == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Contemplar", "No hay artículos por surtir", "OK");
                return;
            }

            bool confirmar = await Application.Current.MainPage.DisplayAlert(
                "Aviso", "¿Esta seguro de contemplar existencias?",
                "Sí", "No");

            if (confirmar)
            {
                try
                {
                    await _contemplar.ExecuteAsync(Pedido);
                    await ShowArticulos();
                    await Application.Current.MainPage.DisplayAlert("Aviso", "Se ha contemplado", "OK");
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"Error al contemplar existencias: {ex.Message}", "OK");
                }
            }
        }

        
    }
}
