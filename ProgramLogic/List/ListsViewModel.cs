using Listifyr.ItemTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listifyr
{
    class ListsViewModel : IListsViewModel
    {
        public ObservableCollection<List> Lists {  get; set; }
        public ObservableCollection<Category> Categories { get; set; }

        public void AddListItem(int itemId, int listId, int categoryId)
        {
            ListItem newItem = new()
            {
                CategoryID = categoryId,
                ListID = listId
            };
            var category = Categories.FirstOrDefault(c => c.Id == categoryId);
            var list = Lists.FirstOrDefault(l => l.Id == listId);
            if (list != null)
            {
                //Lists.Add(newItem);
            }
           
        }

        public void AddListPanel(List list)
        {
            throw new NotImplementedException();
        }

        public void RemoveListItem(ListItem item)
        {
            throw new NotImplementedException();
        }

        public void RemoveListPanel(List list)
        {
            throw new NotImplementedException();
        }
    }
}
