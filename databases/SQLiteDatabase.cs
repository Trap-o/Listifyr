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
            var dbPath = Database.DatabasePath;

            // Перевіряємо, чи існує база даних у кеші пристрою
            if (!File.Exists(dbPath))
            {
                // Копіюємо базу даних з вбудованого ресурсу до локальної директорії додатка
                CopyDatabaseFromResource(dbPath);
            }

            // Ініціалізуємо підключення до бази даних
            _database = new SQLiteAsyncConnection(dbPath, Database.Flags);

            // Ініціалізуємо базу даних
            InitializeDatabaseAsync().ConfigureAwait(false);
        }
        private void CopyDatabaseFromResource(string dbPath)
        {
            // Отримуємо назву простору імен проекту
            var assembly = typeof(SQLiteDatabase).Assembly;
            var resourceName = "Listifyr.database.db"; // Вкажіть повний шлях до файлу бази даних у вашому проекті

            using (Stream resource = assembly.GetManifestResourceStream(resourceName))
            {
                if (resource == null)
                {
                    throw new Exception($"Не вдалося знайти ресурс: {resourceName}");
                }

                using (var fileStream = new FileStream(dbPath, FileMode.Create, FileAccess.Write))
                {
                    resource.CopyTo(fileStream);
                }
            }
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