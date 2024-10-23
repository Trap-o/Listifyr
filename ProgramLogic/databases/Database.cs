using Listifyr.ItemTypes;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listifyr.ProgramLogic.databases
{
    public static class Database
    {

        public const string databaseName = "database.db";

        public const SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db");

        public static async Task InitializeDatabaseAsync()
        {
            var localDbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), databaseName);

            // Перевіряємо, чи база даних вже є в локальній директорії
            if (!File.Exists(localDbPath))
            {
                using (var stream = await FileSystem.OpenAppPackageFileAsync(databaseName))
                {
                    using (var localStream = File.Create(localDbPath))
                    {
                        await stream.CopyToAsync(localStream); // Копіюємо базу даних з ресурсів проекту в локальну папку
                    }
                }
            }
        }
    }
}