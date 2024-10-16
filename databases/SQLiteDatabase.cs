using Listifyr.ItemTypes;
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
            InitializeDatabaseAsync<Categories>().ConfigureAwait(false);
            InitializeDatabaseAsync<Catalogues>().ConfigureAwait(false);
            InitializeDatabaseAsync<MediaItems>().ConfigureAwait(false);
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

        private async Task InitializeDatabaseAsync<T>() where T : class, new()
        {
            await _database.CreateTableAsync<T>();
        }

        public async Task<List<T>> GetAsync<T>() where T : class, new()
        {
            return await _database.Table<T>().ToListAsync();
        }
        //TODO поміняти на дженерік
        public async Task<T> GetItemByIDAsync<T>(int id) where T : class, IDatabaseItem, new()
        {
            return await _database.Table<T>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> AddItemAsync<T>(T item) where T : class, new()
        {
            return await _database.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync<T>(T item) where T : class, new()
        {
            return _database.DeleteAsync(item);
        }

        public Task<int> UpdateItemAsync<T>(T item) where T : class, IDatabaseItem, new()
        {
            if (item.Id != 0)
                return _database.UpdateAsync(item);
            else
                return _database.InsertAsync(item);
        }

        public static async Task PopulateDB<T>(List<T> items) where T : class, IDatabaseItem, new()
        {
            foreach (T item in items)
            {
                var existingItem = await App.Database.GetItemByIDAsync<T>(item.Id);
                if (existingItem == null)
                {
                    await App.Database.AddItemAsync(item);
                }
            }
        }
    }
}