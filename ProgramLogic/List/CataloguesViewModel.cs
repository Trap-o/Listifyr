using Listifyr.ItemTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listifyr
{
    public class CataloguesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Catalogues> catalogues;

        public ObservableCollection<Catalogues> Catalogues
        {
            get
            {
                return catalogues;
            }
            set
            {
                catalogues = value;
                OnPropertyChanged("Catalogues");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        //public ObservableCollection<Categories> Categories { get; set; }

        public async Task LoadCataloguesAsync()
        {
            var catalogues = await App.Database.GetAsync<Catalogues>();
            Catalogues = new ObservableCollection<Catalogues>(catalogues);
        }

        public async Task AddCatalogue()
        {
            string newCatalogueName = await Shell.Current.DisplayPromptAsync("Новий список", "Введіть назву списку");
            if (!string.IsNullOrEmpty(newCatalogueName))
            {
                var addedCatalogue = new Catalogues { Name = (newCatalogueName).TrimEnd() };
                await App.Database.AddItemAsync<Catalogues>(addedCatalogue);
                
            }
            await LoadCataloguesAsync();
        }
    }
}
