using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases;
using BodegaMovil.UseCases.DTO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using BodegaMovil.Views;
using System.Text.Json;

namespace BodegaMovil.ViewModels
{
    public partial class ListaPedidosViewModel : ObservableObject
    {
        #region Campos Privados
        private GetPedidosSurtirUseCase _getPedidos;
        private UsuarioDTO _user;
        private ObservableCollection<Usuarios_Tiendas> tiendasAsignadas;
        private ObservableCollection<Usuarios_Areas> areasAsignadas;
        private int id_tiendaSelected;
        private int id_areaSelected;

        private Usuarios_Tiendas _tiendaSeleccionada;
        private Usuarios_Areas _areaSeleccionada;
        #endregion

        public ListaPedidosViewModel(GetPedidosSurtirUseCase getPedidos)
        {
            _getPedidos = getPedidos;
            ListaPedidos = new ObservableCollection<ShowPedidoDTO>();
            tiendasAsignadas = new ObservableCollection<Usuarios_Tiendas>();
            areasAsignadas = new ObservableCollection<Usuarios_Areas>();
        }

        #region Propiedades Publicas
        public ObservableCollection<ShowPedidoDTO> ListaPedidos { get; set; }

        public ObservableCollection<Usuarios_Tiendas> ListaTiendas
        {
            get => tiendasAsignadas;
        }
        public ObservableCollection<Usuarios_Areas> ListaAreas
        {
            get => areasAsignadas;
        }

        public Usuarios_Tiendas TiendaSeleccionada
        {
            get => _tiendaSeleccionada;
            set
            {
                if (SetProperty(ref _tiendaSeleccionada, value))
                {
                    ID_TiendaSelected = value?.ID_Tienda ?? 0; // Se actualiza automáticamente
                }
            }
        }

        public Usuarios_Areas AreaSeleccionada 
        {
            get => _areaSeleccionada;
            set
            {
                if (SetProperty(ref _areaSeleccionada, value))
                {
                    ID_AreaSelected = value?.ID_Area ?? 0; // Se actualiza automáticamente
                }
            }
        }

        
        public int ID_TiendaSelected { get => id_tiendaSelected; set => SetProperty(ref id_tiendaSelected,value); }

        
        public int ID_AreaSelected { get => id_areaSelected; set => SetProperty(ref id_areaSelected, value); }

        #endregion
        
        public async Task AsignarUsuario(UsuarioDTO user)
        {
            _user = user;
            CargarAreas(_user.ListaAreasAsignadas);
            CargarTiendas(_user.ListaTiendasAsignadas);
        }

        private async void CargarAreas(List<Usuarios_Areas> lista)
        {
            if (lista.Count > 0)
            {
                areasAsignadas.Add(new Usuarios_Areas()
                {
                    ID_Area = 0,
                    DescripcionArea = "Todas",
                });
            }

            foreach(var item in lista)
            {
                areasAsignadas.Add(item);
            }
        }

        private async void CargarTiendas(List<Usuarios_Tiendas> lista)
        {
            if (lista.Count > 0)
            {
                tiendasAsignadas.Add(new Usuarios_Tiendas()
                {
                    ID_Tienda = 0,
                    DescripcionTienda = "Todas",
                });
            }

            foreach (var item in lista)
            {
                tiendasAsignadas.Add(item);
            }
        }

        
        private async void LoadPedidos(int id_tienda, int id_area)
        {
            ListaPedidos.Clear();
            
            var lista = await _getPedidos.ExecuteAsync(_user, id_tienda, id_area);

            if (lista != null && lista.Count > 0)
            {
                foreach (var item in lista)
                {
                    ListaPedidos.Add(item);
                }
            }
        }

        [RelayCommand]
        public async Task Consultar()
        {
            LoadPedidos(id_tiendaSelected, id_areaSelected);
        }

        [RelayCommand]
        public async Task GoToListaArticulos(ShowPedidoDTO pedido)
        {
            if (pedido == null)
                return;

            //var json = JsonSerializer.Serialize(_user);
            //var ped = JsonSerializer.Serialize(pedido);

            await Shell.Current.GoToAsync($"{nameof(ListaArticulosPage)}?folio={pedido.Folio}&id_area_surtir={pedido.ID_Area}");
        }
    }
}
