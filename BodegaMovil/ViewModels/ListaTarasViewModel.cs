using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BodegaMovil.CoreBusiness;
using BodegaMovil.UseCases;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BodegaMovil.Views;

namespace BodegaMovil.ViewModels
{
    public partial class ListaTarasViewModel : ObservableObject
    {
        private readonly InsertTaraUseCase _insert;
        private readonly UpdateTaraUseCase _update;
        private readonly DeleteTaraUseCase _delete;
        private readonly GetTarasUseCase _getTaras;

        private string _folio;
        private Contenedor _tara;
        

        public ListaTarasViewModel
        (InsertTaraUseCase insert, UpdateTaraUseCase update, DeleteTaraUseCase delete, 
            GetTarasUseCase getTaras)
        {
            _insert = insert;
            _update = update;
            _delete = delete;
            _getTaras = getTaras;

            this.ListaTaras = new ObservableCollection<Contenedor>();
        }

        public ObservableCollection<Contenedor> ListaTaras { get; set; }

        public Contenedor Tara
        {
            get => _tara;
            set => SetProperty (ref _tara, value);
        }

        

        public async Task LoadTaras(string folio)
        {
            ListaTaras.Clear();

            var lista = await _getTaras.ExecuteAsync(folio);

            if(lista != null)
            {
                foreach(var item in lista)
                {
                    ListaTaras.Add(item);
                }
            }


        }

        [RelayCommand]
        public async Task InsertTara()
        {
            _tara.Folio = _folio;
            _tara.Estado = "Abierto";
            var res = await _insert.ExecuteAsync(_tara);
            if (res > 0)
                await LoadTaras(_folio);
        }

        [RelayCommand]
        public async Task UpdateTara()
        {
            var res = await _update.ExecuteAsync(_tara);
            if (res > 0)
                await LoadTaras(_folio);
        }

        [RelayCommand]
        public async Task DeleteTara()
        {
            var res = await _delete.ExecuteAsync(_tara);
            if (res > 0)
                await LoadTaras(_folio);
        }

        [RelayCommand]
        public async Task GoToListaArticulos(int tara)
        {
            if (string.IsNullOrEmpty(_folio))
                return;

            await Shell.Current.GoToAsync($"{nameof(ListaArticulosPage)}?folio={_folio}");
        }
    }
}
