using Listifyr.ItemTypes;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Listifyr.ProgramLogic.Category
{
    internal class CategoryViewModel
    {
        private ObservableCollection<Items>? mediaItems;

        public ObservableCollection<Items> MediaItems
        {
            get => mediaItems;
            set
            {
                mediaItems = value;
                OnPropertyChanged("MediaItems");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public async Task LoadMediaItemsAsync()
        {
            var mediaItems = await App.Database.GetAsync<Items>();
            MediaItems = new ObservableCollection<Items>(mediaItems);
        }
    }
}
