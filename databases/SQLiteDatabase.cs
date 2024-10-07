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
            InitializeDatabaseAsync().ConfigureAwait(false);
        }

        private async Task InitializeDatabaseAsync()
        {
            await _database.CreateTableAsync<Categories>();
            await AddInitialDataAsync();
        }

        private async Task AddInitialDataAsync()
        {

            var existingCategories = await _database.Table<Categories>().CountAsync();

            if (existingCategories == 0)
            {

                var initialCategories = new List<Categories>
                {
                    new Categories { Name = "Фільми", ImagePath = "movie.png" },
                    new Categories { Name = "Книги", ImagePath = "book.png" },
                    new Categories { Name = "Ігри", ImagePath = "game.png" }
                };

                foreach (var category in initialCategories)
                {
                    await _database.InsertAsync(category);
                }
            }
        }

        public async Task<List<Categories>> GetCategoriesAsync()
        {
            return await _database.Table<Categories>().ToListAsync();
        }

        public async Task<Categories> GetCategoryAsync(Categories category)
        {
            return await _database.Table<Categories>().Where(c => c.CategoryID == category.CategoryID).FirstOrDefaultAsync();
        }

        public async Task<int> AddCategoryAsync(Categories category)
        {
            return await _database.InsertAsync(category);
        }

        public Task<int> DeleteCategoryAsync(Categories category)
        {
            return _database.DeleteAsync(category);
        }

        public Task<int> UpdateCategoryAsync(Categories category)
        {
            if (category.CategoryID != 0)
                return _database.UpdateAsync(category);
            else
                return _database.InsertAsync(category);
        }
    }
}