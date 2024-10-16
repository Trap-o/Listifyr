using Listifyr.databases;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listifyr.ItemTypes
{
    public class MediaItems : IDatabaseItem//IMediaItem
    {
        private string? name;
        private string? status;
        private double? rate;
        private string? description;
        private string? type;
        public int CategoryID { get; set; }
        public int CatalogueID { get; set; }

        [PrimaryKey, AutoIncrement]
        //[Display(AutoGenerateField = false)]
        public int MediaItemID { get; set; }
        public int Id => MediaItemID;

        public string? Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                RaisePropertyChanged("Name");
            }
        }
        public string? Status
        {
            get { return this.status; }
            set
            {
                this.status = value;
                RaisePropertyChanged("Status");
            }
        }
        public double? Rate
        {
            get { return this.rate; }
            set
            {
                this.rate = value;
                RaisePropertyChanged("Rate");
            }
        }
        public string? Description
        {
            get { return this.description; }
            set
            {
                this.description = value;
                RaisePropertyChanged("Description");
            }
        }
        public string? Type
        {
            get { return this.type; }
            set
            {
                this.type = value;
                RaisePropertyChanged("Type");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(String value)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(value));
        }

    }
}
