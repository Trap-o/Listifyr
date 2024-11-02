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
        private static void CopyDatabaseFromResource(string dbPath)
        {
            var assembly = typeof(SQLiteDatabase).Assembly;
            var resourceName = "Listifyr.database.db";

            using Stream? resource = assembly.GetManifestResourceStream(resourceName);

            using var fileStream = new FileStream(dbPath, FileMode.Create, FileAccess.Write);
            resource?.CopyTo(fileStream);
        }

        private async Task InitializeDatabaseAsync<T>() where T : class, new() => await _database.CreateTableAsync<T>();

        public async Task<List<T>> LoadTableByIDAsync<T>(int id, string parameter) where T : class, new() =>
            await _database.QueryAsync<T>($"SELECT * FROM {typeof(T).Name} WHERE {parameter} = ?", id);

        public async Task<int?> GetIDByNameAsync<T>(string parameter, string tableName, string columnName, string value) where T : class, IDatabaseItem, new()
        {
            return await _database.ExecuteScalarAsync<int?>($"SELECT {parameter} FROM {tableName} WHERE {columnName} = ?", value);
        }

        public async Task<List<T>> GetAsync<T>() where T : class, new() => await _database.Table<T>().ToListAsync();

        public async Task<List<string?>> GetListOfCataloguesNamesAsync()
        {
            var cataloguesList = await _database.Table<Catalogues>().ToListAsync();
            return cataloguesList.Select(static cl => cl.Name).Where(static name => name != null).ToList();
        }

        public async Task<T> GetItemByIDAsync<T>(int id) where T : class, IDatabaseItem, new()
        {
            var item = await _database.Table<T>().Where(i => i.Id == id).FirstOrDefaultAsync() ?? throw new Exception($"Item with ID {id} not found.");
            return item;
        }
        public async Task UpdateItemAsync(string parameter, string tableName, string columnName, string newValue, int valueForColumnName)
        {
            await _database.ExecuteAsync($"UPDATE {tableName} SET {parameter} = ? WHERE {columnName} = ?", newValue, valueForColumnName);
        }

        public async Task<int> AddItemAsync<T>(T item) where T : class, new() => await _database.InsertAsync(item);

        public async Task DeleteItemAsync(string tableName, string parameter, string value) 
        {
            await _database.ExecuteAsync($"DELETE FROM {tableName} WHERE {parameter} = ?", value);
            await _database.ExecuteAsync($"DELETE FROM sqlite_sequence WHERE name = ?", tableName);
        }
    }
}