using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listifyr.databases
{
    public class SQLiteDatabase
    {
        readonly SQLiteAsyncConnection _database;
        public SQLiteDatabase()
        {
            _database = new SQLiteAsyncConnection(Database.DatabasePath, Database.Flags);
            //InitializeDatabaseAsync();
        }

        private async Task InitializeDatabaseAsync()
        {
            await _database.CreateTableAsync<Category>();
            await AddInitialDataAsync();
        }

        private async Task AddInitialDataAsync()
        {

            var existingCategories = await _database.Table<Category>().CountAsync();

            if (existingCategories == 0)
            {

                var initialCategories = new List<Category>
                {
                    new Category { Name = "Фільми", ImagePath = "movie.png" },
                    new Category { Name = "Книги", ImagePath = "book.png" },
                    new Category { Name = "Ігри", ImagePath = "game.png" },
                    new Category { Name = "Манга", ImagePath = "manga.png" }
                };

                // Додавання початкових категорій до таблиці
                foreach (var category in initialCategories)
                {
                    await _database.InsertAsync(category);
                }
            }
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _database.Table<Category>().ToListAsync();
        }

        public async Task<Category> GetCategoryAsync(Category category)
        {
            return await _database.Table<Category>().Where(c => c.ID == category.ID).FirstOrDefaultAsync();
        }

        public async Task<int> AddCategoryAsync(Category category)
        {
            return await _database.InsertAsync(category);
        }

        public Task<int> DeleteCategoryAsync(Category category)
        {
            return _database.DeleteAsync(category);
        }

        public Task<int> UpdateCategoryAsync(Category category)
        {
            if(category.ID != 0)
                return _database.UpdateAsync(category);
            else
                return _database.InsertAsync(category);
        }
    }
}
