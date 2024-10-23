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
    internal class ItemsViewModel : INotifyPropertyChanged
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

        public void ChangeFieldForAPI(string API)
        {
            switch (API)
            {
                case "TMDB":
                    //mediaItems;
                    break;

                default:
                    break;
            }
        }
    }
}
