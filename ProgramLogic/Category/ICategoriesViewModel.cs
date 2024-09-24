using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listifyr.ProgramLogic.Category
{
    interface ICategoriesViewModel
    {
        void AddItemToCategory(string itemName, string status, double rate, string description, string type, int listId, int categoryId);
    }
}
