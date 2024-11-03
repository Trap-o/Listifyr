using Listifyr.ProgramLogic.databases;
using SQLite;
using System.ComponentModel;

namespace Listifyr.ItemTypes
{
    public partial class Items : IDatabaseItem, INotifyPropertyChanged
    {
        private string? itemname;
        private string? status;
        private string? description;
        private string? poster;
        private string? release_date;
        public int? id_catalogue;
        public int ID_Category { get; set; }

        [PrimaryKey, AutoIncrement]
        public int ItemID { get; set; }
        public int Id => ItemID;

        public string? ItemName
        {
            get { return itemname; }
            set
            {
                itemname = value;
                RaisePropertyChanged("ItemName");
            }
        }
        public string? Status
        {
            get { return status; }
            set
            {
                status = value;
                RaisePropertyChanged("Status");
            }
        }

        public string? Description
        {
            get { return description; }
            set
            {
                description = value;
                RaisePropertyChanged("Description");
            }
        }

        public string? Poster
        {
            get { return poster; }
            set
            {
                poster = value;
                RaisePropertyChanged("Poster");
            }
        }
        public string? Release_Date
        {
            get { return release_date; }
            set
            {
                release_date = value;
                RaisePropertyChanged("Release_Date");
            }
        }

        public int? ID_Catalogue
        {
            get { return id_catalogue; }
            set
            {
                id_catalogue = value;
                RaisePropertyChanged("ID_Catalogue");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged(string value)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(value));
        }

    }
}
