using Listifyr.ItemTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listifyr.ProgramLogic.ListItem
{
    internal class MediaItemsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<MediaItems> mediaItems;

        public ObservableCollection<MediaItems> MediaItems
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


    }
}
