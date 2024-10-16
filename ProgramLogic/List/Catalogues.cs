using Listifyr.databases;
using SQLite;
using System.ComponentModel;

namespace Listifyr
{
    public class Catalogues : IDatabaseItem
    {
        private string? name;

        [PrimaryKey, AutoIncrement]
        //[Display(AutoGenerateField = false)]
        public int CatalogueID { get; set; }
        public int Id => CatalogueID;

        public string? Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                RaisePropertyChanged("Name");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string value)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(value));
        }
    }
}