using Listifyr.ItemTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listifyr
{
    class CatalogueViewModel : ICatalogueViewModel
    {
        public ObservableCollection<Catalogue> Catalogues {  get; set; }
        public ObservableCollection<Category> Categories { get; set; }

        public void AddListItem(int itemId, int catalogueId, int categoryId)
        {
            MediaItem newItem = new()
            {
                CategoryID = categoryId,
                CatalogueID = catalogueId
            };
            var category = Categories.FirstOrDefault(c => c.ID == categoryId);
            var list = Catalogues.FirstOrDefault(l => l.Id == catalogueId);
            if (list != null)
            {
                //Lists.Add(newItem);
            }
           
        }

        public void AddListPanel(Catalogue list)
        {
            throw new NotImplementedException();
        }

        public void RemoveListItem(MediaItem item)
        {
            throw new NotImplementedException();
        }

        public void RemoveListPanel(Catalogue list)
        {
            throw new NotImplementedException();
        }
    }
}
