using Listifyr.ItemTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listifyr.ProgramLogic.Category
{
    internal class CategoryViewModel
    {
        private ObservableCollection<Items> mediaItems;

        public ObservableCollection<Items> MediaItems
        {
            get
            {
                return mediaItems;
            }
            set
            {
                mediaItems = value;
                OnPropertyChanged("MediaItems");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public async Task LoadMediaItemsAsync()
        {
            var mediaItems = await App.Database.GetAsync<Items>();
            MediaItems = new ObservableCollection<Items>(mediaItems);
        }
        //public async Task AddMediaItem()
        //{
        //    string newCatalogueName = await Shell.Current.DisplayPromptAsync("Новий список", "Введіть назву списку");
        //    if (!string.IsNullOrEmpty(newCatalogueName))
        //    {
        //        var addedCatalogue = new Catalogues { Name = (newCatalogueName).TrimEnd() };
        //        await App.Database.AddItemAsync<Catalogues>(addedCatalogue);

        //    }
        //    await LoadCataloguesAsync();
        //}
    }
}
