using SQLite;

namespace Listifyr.ProgramLogic.databases
{
    public static class Database
    {
        public const string databaseName = "database.db";

        public const SQLiteOpenFlags Flags =
                     SQLiteOpenFlags.ReadWrite |
                     SQLiteOpenFlags.Create |
                     SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db");

        public static async Task InitializeDatabaseAsync()
        {
            var localDbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), databaseName);

            if (!File.Exists(localDbPath))
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync(databaseName);
                using var localStream = File.Create(localDbPath);
                await stream.CopyToAsync(localStream);
            }
        }
    }
}