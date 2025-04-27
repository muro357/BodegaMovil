using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases;
using BodegaMovil.UseCases.DTO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using BodegaMovil.Views;
using BodegaMovil.CoreBusiness.Enums;
using BodegaMovil.UseCases.Interfaces.Services;

namespace BodegaMovil.ViewModels
{
    public partial class ArticuloViewModel : ObservableObject
    {
        private readonly AddNewArtSurtirUseCase _addNewArtSurtir;
        private readonly SurtirUseCase _surtir;
        private readonly IMapa _mapa;
        private ArticuloDTO _articulo;
        private PedidoDTO _pedido;
        
        [ObservableProperty]
        private PedidoDetalleDTO linea;
        
        public ArticuloViewModel(AddNewArtSurtirUseCase addNewArtSurtir, SurtirUseCase surtir, IMapa mapa)
        {
            _addNewArtSurtir = addNewArtSurtir;
            _surtir = surtir;
            _mapa = mapa;
        }

        //Agregar Articulo Page
        public async Task LoadArticulo(PedidoDTO pedido, ArticuloDTO art)
        {
            _pedido = pedido;
            _articulo = art;

            Linea = new PedidoDetalleDTO()
            {
                SKU = _articulo.SKU,
                Descripcion = _articulo.Descripcion,
                ID_Tienda = _pedido.ID_Tienda,
                ID_Area = _pedido.ID_AreaSurtir,
                DescripcionArea = _pedido.PedidoDetalle[0].DescripcionArea,
                Unidad = _articulo.UnidadVenta,
                CantidadPedida = 0,
                Tipo = _pedido.Tipo,
            };
        }

        //Capturar Articulo Page
        public async Task LoadArticulo(PedidoDTO pedido, PedidoDetalleDTO linea)
        {
            _pedido = pedido;
            Linea = linea;
        }

        [RelayCommand]
        public async Task AddNewArt()
        {
            if (await ValidateArticulo())
            {
                var art = _mapa.GetEntity<ArticuloDTO, Articulo>(_articulo);

                try
                {
                    await _addNewArtSurtir.ExecuteAsync(_pedido, art, Linea.CantidadSurtida, FormaDeCalculo.Articulo_Agregado);
                    await GoToListaArticulos();
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }

        [RelayCommand]
        public async Task SurtirArt()
        {
            if (await ValidateArticulo())
            {
                await _surtir.ExecuteAsync(_pedido, Linea);
                await GoToListaArticulos();
            }
        }
        
        [RelayCommand]
        public async Task GoToListaArticulos()
        {
            await Shell.Current.GoToAsync($"{nameof(ListaArticulosPage)}?folio={_pedido.Folio}&id_area_surtir={_pedido.ID_AreaSurtir}");
        }

        #region Validaciones
        public bool IsCantidadSurtidaProvided { get; set; }

        public bool IsContenedorProvided { get; set; }

        private async Task<bool> ValidateArticulo()
        {
            if (!this.IsCantidadSurtidaProvided)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "La cantidad surtida es requerida.", "OK");
                return false;
            }
            if (!this.IsContenedorProvided)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El contenedor es requerido.", "OK");
                return false;
            }
            
            return true;
        }

        #endregion

    }
}
