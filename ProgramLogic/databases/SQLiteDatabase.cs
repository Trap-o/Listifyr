using Listifyr.ItemTypes;
using SQLite;

namespace Listifyr.ProgramLogic.databases
{
    public class SQLiteDatabase
    {
        readonly SQLiteAsyncConnection _database;
        public SQLiteDatabase()
        {
            var dbPath = Database.DatabasePath;

            if (!File.Exists(dbPath))
            {
                CopyDatabaseFromResource(dbPath);
            }

            _database = new SQLiteAsyncConnection(dbPath, Database.Flags);

            InitializeDatabaseAsync<Categories>().ConfigureAwait(false);
            InitializeDatabaseAsync<Catalogues>().ConfigureAwait(false);
            InitializeDatabaseAsync<Items>().ConfigureAwait(false);
        }
        private void CopyDatabaseFromResource(string dbPath)
        {
            var assembly = typeof(SQLiteDatabase).Assembly;
            var resourceName = "Listifyr.database.db";

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

        public async Task<List<T>> LoadTableByIDAsync<T>(int id, string parameter) where T : class, new()
        {
            var query = $"SELECT * FROM {typeof(T).Name} WHERE {parameter} = ?";
            var table = await _database.QueryAsync<T>(query, id);
            return table.ToList();
        }

        public async Task<int?> GetIDByNameAsync<T>(string parameter, string tableName, string columnName, string value) where T : class, IDatabaseItem, new()
        {
            return await _database.ExecuteScalarAsync<int?>($"SELECT {parameter} FROM {tableName} WHERE {columnName} = ?", value);
        }

        public async Task<List<T>> GetAsync<T>() where T : class, new()
        {
            return await _database.Table<T>().ToListAsync();
        }

        public async Task<T> GetItemByParameterAsync<T>(int id) where T : class, IDatabaseItem, new()
        {
            var item = await _database.Table<T>().Where(i => i.Id == id).FirstOrDefaultAsync();
            if (item == null)
                throw new Exception($"Item with ID {id} not found.");

            return item;
        }
        public async Task UpdateItemAsync(string parameter, string tableName, string columnName, string newValue, int valueForColumnName)
        {
            await _database.ExecuteAsync($"UPDATE {tableName} SET {parameter} = ? WHERE {columnName} = ?", newValue, valueForColumnName);
            //if (item.Id != 0)
            //    return _database.UpdateAsync(item);
            //else
            //    return _database.InsertAsync(item);
        }

        public async Task<int> AddItemAsync<T>(T item) where T : class, new()
        {
            return await _database.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync<T>(T item) where T : class, new()
        {
            return _database.DeleteAsync(item);
        }



        //public static async Task PopulateDB<T>(List<T> items) where T : class, IDatabaseItem, new()
        //{
        //    foreach (T item in items)
        //    {
        //        var existingItem = await App.Database.GetItemByIDAsync<T>(item.Id);
        //        if (existingItem == null)
        //            await App.Database.AddItemAsync(item);
        //    }
        //}
    }
}