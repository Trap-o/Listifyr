using Listifyr.ProgramLogic.databases;
using SQLite;
using System.ComponentModel;

namespace Listifyr
{
    public class Catalogues : IDatabaseItem
    {
        private string? name;

        [PrimaryKey, AutoIncrement]
        public int CatalogueID { get; set; }
        public int Id => CatalogueID;

        public string? Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged(string value)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(value));
        }
    }
}