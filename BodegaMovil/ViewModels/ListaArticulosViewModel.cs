using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases;
using BodegaMovil.UseCases.DTO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using BodegaMovil.Views;

namespace BodegaMovil.ViewModels
{
    public partial class ListaArticulosViewModel : ObservableObject
    {
        private readonly GetPedidoSurtirUseCase getPedido;
        private readonly DepurarUseCase depurar;
        private readonly GetArticulosUseCase getArticulos;
        private readonly ContemplarExistenciaUseCase contemplar;
        private ObservableCollection<PedidoDetalleDTO> pedidosDetalle;
        private PedidoDTO pedido;
        private List<string> ordenarPor;

        public ListaArticulosViewModel(GetPedidoSurtirUseCase getPedido, DepurarUseCase depurar, GetArticulosUseCase getArticulos, ContemplarExistenciaUseCase contemplar)
        {
            this.getPedido = getPedido;
            this.depurar = depurar;
            this.getArticulos = getArticulos;
            this.contemplar = contemplar;
            this.pedidosDetalle = new ObservableCollection<PedidoDetalleDTO>();

            this.ordenarPor = new List<string>() { "SKU", "Descripcion", "Ubicacion", "Cantidad Pedida" };
        }

        public ObservableCollection<PedidoDetalleDTO> ListaArticulos
        {
            get => pedidosDetalle;
        }

        public List<string>OrdenarPor
        {
            get => ordenarPor;
            set { SetProperty(ref ordenarPor, value); }
        }

        private string _ordenar;
        public string Ordenar
        {
            get => _ordenar;
            set => SetProperty(ref _ordenar, value);
        }

        public async Task LoadArticulos(string folio, int id_tienda)
        {
            this.pedido = await getPedido.ExecuteAsync(id_tienda, folio);

            foreach(var item in pedido.PedidoDetalle)
            {
                pedidosDetalle.Add(item);
            }
        }

        [RelayCommand]
        public async Task Depurar()
        {
            //await depurar.ExecuteAsync(pedido);
            var seleccionados = pedidosDetalle.Where(p => p.Elegido).Select(p => p.SKU).ToList();

            string mensaje = seleccionados.Any()
                ? $"Seleccionados:\n{string.Join("\n", seleccionados)}"
                : "Ningún producto seleccionado.";

            await Application.Current.MainPage.DisplayAlert("Lista", mensaje, "OK");
        }

        [RelayCommand]
        public async Task Contemplar()
        {
            await contemplar.ExecuteAsync(pedido);
        }

        [RelayCommand]
        public async Task BuscarArticulos()
        {
            
        }

    }
}
