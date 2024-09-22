using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listifyr
{
    public class CategoriesViewModel
    {
        public ObservableCollection<Category> Categories { get; set; }

        public CategoriesViewModel()
        {
            Categories = new ObservableCollection<Category>
            {
                new() { Name = "Фільми", Image = "movie.png" },
                new() { Name = "Серіали", Image = "video.png" },
                new() { Name = "Ігри", Image = "joystick.png" },
                new() { Name = "Аніме", Image = "cloud.png" },
                new() { Name = "Книги", Image = "book.png" },
                new() { Name = "Ранобе", Image = "manga.png" },
                new() { Name = "Манга", Image = "noun_manga_5011813.png" },
                new() { Name = "Комікси", Image = "comic.png" }
            };
        }
    }
}
