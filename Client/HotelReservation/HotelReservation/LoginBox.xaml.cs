using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HotelReservation
{
    public partial class LoginBox : Window
    {
        public LoginBox()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsernameBox.Text.Length > 0 && PasswordBox.Password.Length > 0)
            {
                AsyncTaskMessage.Foreground = Brushes.Black;
                AsyncTaskMessage.Text = "Logging in";
                if (await ServiceConnection.GetConnection().Login(UsernameBox.Text, PasswordBox.Password))
                {
                    AsyncTaskMessage.Text = "";
                    new MainWindow().Show();
                    Close();
                }
                AsyncTaskMessage.Text = "Invalid username or password";
            }
            else
            {
                AsyncTaskMessage.Text = "Fill username and password";
            }
            AsyncTaskMessage.Foreground = Brushes.Red;
        }

        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                LoginButton_Click(sender, e);
        }
    }
}
