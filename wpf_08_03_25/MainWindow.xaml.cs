using data_08_03_25.DBProvider;
using System.Data.SqlTypes;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;


namespace wpf_08_03_25
{ 
    public partial class MainWindow : Window
    {
        private DatabaseProvider _provider;

        public MainWindow()
        {
            InitializeComponent();
            var connectionString = "Data Source=DESKTOP-F044K8I\\SQLEXPRESS;Initial Catalog=Games_Store_and_Launcher;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;";
            _provider = new DatabaseProvider(connectionString);
            MainWindowFrame.Navigate(new desktop_08_03_25.Pages.LoginPage(_provider));
        }



        private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {   
                await _provider.InitializeDatabaseAsync();
                var accountCount = await _provider.GetAccountCountAsync();
                Console.WriteLine($"Number of accounts in database: {accountCount}");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                await _provider.ResetDatabaseAsync();
                await _provider.InitializeDatabaseAsync();
            }
            
        }

        //private async Task CreateDatabase()
        //{
        //    var connectionString = "Data Source=localhost;Initial Catalog=master;Integrated Security=True";
        //    DatabaseProvider databaseProvider = new(connectionString);
        //    await databaseProvider.InitializeDatabaseAsync();
        //    await databaseProvider.ResetDatabaseAsync();
        //}
    }
}