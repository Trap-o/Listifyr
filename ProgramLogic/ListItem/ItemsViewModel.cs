using Listifyr.ItemTypes;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Listifyr.ProgramLogic.ListItem
{
    internal partial class ItemsViewModel : INotifyPropertyChanged
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
    }
}
