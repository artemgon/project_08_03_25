using core_08_03_25.Repository.AuthRepository;
using data_08_03_25.DBProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using data_08_03_25.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;
using data_08_03_25.Models;
using wpf_08_03_25;

namespace desktop_08_03_25.Pages
{
    public partial class LoginPage : UserControl
    {
        private AuthRepository<AccountModel?>? _authRepository;
        public LoginPage(DatabaseProvider? _databaseProvider)
        {
            InitializeComponent();
            if (_databaseProvider != null) _authRepository = new AuthRepositoryImpl(_databaseProvider);
        }

        private async void SignUpButtonClick(object sender, RoutedEventArgs e)
        {
            var email = SignInTextBox.Text;
            var password = PasswordBox.Password;

            Console.WriteLine($"Attempting registration with email: {email}");

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Email and password cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_authRepository == null)
            {
                MessageBox.Show("Authentication service is unavailable.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                bool isRegistered = await _authRepository.RegisterAsync(email, password);
                if (isRegistered)
                {
                    MessageBox.Show("Registration successful! You can now sign in.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    PasswordBox.Password = "";
                }
                else
                {
                    MessageBox.Show("Registration failed. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Registration error: {ex.Message}\n\nDetails: {ex.InnerException?.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async void SignInButtonClick(object sender, RoutedEventArgs e)
        {
            var email = SignInTextBox.Text;
            var password = PasswordBox.Password;

            Console.WriteLine($"Attempting login with email: {email}"); // Debug line

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Email and password cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_authRepository == null)
            {
                MessageBox.Show("Authentication service is unavailable.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var account = await _authRepository.LoginAsync(email, password);
                if (account == null)
                {
                    MessageBox.Show("Invalid email or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    var result = MessageBox.Show("Login successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (result == MessageBoxResult.OK)
                    {
                        var mainWindow = (MainWindow)Application.Current.MainWindow;
                        mainWindow?.MainWindowFrame.Navigate(new HomePage(account));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
