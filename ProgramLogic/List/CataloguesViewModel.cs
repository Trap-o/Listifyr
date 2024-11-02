using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Listifyr
{
    public partial class CataloguesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Catalogues>? catalogues;

        public ObservableCollection<Catalogues> Catalogues
        {
            get => catalogues;
            set
            {
                catalogues = value;
                OnPropertyChanged("Catalogues");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public async Task LoadCataloguesAsync()
        {
            var catalogues = await App.Database.GetAsync<Catalogues>();
            Catalogues = new ObservableCollection<Catalogues>(catalogues);
        }

        public async Task AddCatalogue()
        {
            string newCatalogueName = await Shell.Current.DisplayPromptAsync("New catalogue", "Enter catalogue name (max. 15 characters):", maxLength: 15);
            if (!string.IsNullOrEmpty(newCatalogueName))
            {
                var addedCatalogue = new Catalogues { Name = (newCatalogueName).TrimEnd() };
                await App.Database.AddItemAsync<Catalogues>(addedCatalogue);
            }
            await LoadCataloguesAsync();
        }
    }
}
