using Listifyr.ProgramLogic.databases;

namespace Listifyr
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        static SQLiteDatabase? database;

        public static SQLiteDatabase Database
        {
            get
            {
                database ??= new SQLiteDatabase();

                return database;
            }
        }
    }
}