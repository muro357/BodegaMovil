using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases;
using BodegaMovil.UseCases.DTO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using BodegaMovil.Views;
using BodegaMovil.CoreBusiness.Enums;

namespace BodegaMovil.ViewModels
{
    public partial class ArticuloViewModel : ObservableObject
    {
        private readonly GetArticuloUseCase getArticulo;
        private readonly AddNewArtSurtirUseCase addNewArtSurtir;
        private readonly SurtirUseCase surtir;
        private ArticuloDTO articuloDTO;
        private PedidoDetalleDTO pedidoDetalle;
        public ArticuloDTO ArticuloDTO 
        { 
            get => articuloDTO; 
            set => SetProperty(ref articuloDTO, value); 
        }

        public PedidoDetalleDTO PedidoDetalleDTO
        {
            get => pedidoDetalle;
            set => SetProperty(ref pedidoDetalle, value);
        }

        public ArticuloViewModel(GetArticuloUseCase getArticulo, AddNewArtSurtirUseCase addNewArtSurtir, SurtirUseCase surtir)
        {
            this.getArticulo = getArticulo;
            this.addNewArtSurtir = addNewArtSurtir;
            this.surtir = surtir;
        }

        

        public async Task LoadArticulo(string sku, int id_tienda)
        {
            this.articuloDTO = await getArticulo.ExecuteAsync(sku, id_tienda);
            
        }

        [RelayCommand]
        public async Task AddNewArt()
        {
            if (await ValidateArticulo())
            {
                await this.addNewArtSurtir.ExecuteAsync(new PedidoDTO(),new Articulo(), 0, FormaDeCalculo.Articulo_Agregado);
                await Shell.Current.GoToAsync($"{nameof(ListaArticulosPage)}");
            }
        }

        [RelayCommand]
        public async Task SurtirArt()
        {
            if (await ValidateArticulo())
            {
                await this.surtir.ExecuteAsync(new PedidoDTO(), new PedidoDetalleDTO());
                await Shell.Current.GoToAsync($"{nameof(ListaArticulosPage)}");
            }
        }
        
        [RelayCommand]
        public async Task GoToListaArticulos()
        {
            await Shell.Current.GoToAsync($"{nameof(ListaArticulosPage)}");
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
