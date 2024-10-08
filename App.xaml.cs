using Listifyr.databases;

namespace Listifyr
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            MainPage = new AppShell();

        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine($"Необроблена виняткова ситуація: {e.ExceptionObject}");
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Console.WriteLine($"Необроблена виняткова ситуація у Task: {e.Exception}");
        }

        static SQLiteDatabase database;

        public static SQLiteDatabase Database
        {
            get
            {
                if (database == null)
                    database = new SQLiteDatabase();

                return database;
            }
        }
    }
}