using ApiClient.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TeendokLista.MAUI.Models;
using TeendokLista.MAUI.Views;

namespace TeendokLista.MAUI.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private IGenericRepository<Feladat> _repository;

        public MainViewModel(IGenericRepository<Feladat> repository)
        {
            _repository = repository;
            LoadData();
            NewCommandAsync = new AsyncRelayCommand(AddItem);
            SelectCommandAsync = new AsyncRelayCommand<Feladat>(f => ShowItem(f));
            LogoutCommandAsync = new AsyncRelayCommand(Logout);
            RegisterUpdate();
        }

        private ObservableCollection<Feladat> _feladatok = new();
        public ObservableCollection<Feladat> Feladatok
        {
            get { return _feladatok; }
            set { SetProperty(ref _feladatok, value); }
        }

        public IAsyncRelayCommand<Feladat> SelectCommandAsync { get; set; }
        public IAsyncRelayCommand NewCommandAsync { get; set; }
        public IAsyncRelayCommand LogoutCommandAsync { get; set; }

        private async Task LoadData()
        {
            var result = await _repository.GetAllAsync();
            Feladatok = new ObservableCollection<Feladat>(result);
        }

        // Regisztrálás az üzenetközpont üzenetire
        // Ha jön üzenet a DetailViewModeltől, pl. egy Feladat objektum, akkor frissítse a meglévő listát
        private void RegisterUpdate()
        {
            MessagingCenter.Subscribe<DetailViewModel, Feladat>(this, "UpdateView", async (sender, feladat) =>
            {
                await LoadData();
            });
        }

        private async Task ShowItem(Feladat feladat)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Feladat", feladat }
            };
            // Navigáció a másik Page-re
            await Shell.Current.GoToAsync(nameof(DetailPage), navigationParameter);
        }

        private async Task AddItem()
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Feladat", new Feladat() }
            };
            await Shell.Current.GoToAsync(nameof(DetailPage), navigationParameter);
        }

        private async Task Logout()
        {
            // await Shell.Current.GoToAsync("..");
            // Hibajavítás miatt:
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
