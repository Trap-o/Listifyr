using Listifyr.ItemTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listifyr
{
    class CataloguesViewModel
    {
        public ObservableCollection<Catalogues> Catalogues { get; set; }
        public ObservableCollection<Categories> Categories { get; set; }

        public async Task LoadCataloguesAsync()
        {
            var catalogues = await App.Database.GetAsync<Catalogues>();
        }
    }
}
