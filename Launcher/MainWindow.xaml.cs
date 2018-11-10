using System.Windows;
using System.Windows.Input;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        protected System.Windows.Forms.NotifyIcon MyNotifyIcon;

        public MainWindow()
        {
            InitializeComponent();
            MyNotifyIcon = new System.Windows.Forms.NotifyIcon
            {
                Icon = new System.Drawing.Icon("launcher.ico")
            };
            MyNotifyIcon.MouseClick += MyNotifyIcon_OnMouseClick;
        }

        private void MyNotifyIcon_OnMouseClick(object sender, MouseEventArgs e)
        {
            MyNotifyIcon.Visible = false;
            Show();
        }

        private void Window_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CollapseButton_Click(object sender, RoutedEventArgs e)
        {
            MyNotifyIcon.Visible = true;
            Hide();
        }

        private void UserPasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordWatermark.Visibility = UserPasswordBox.Password == "" ? Visibility.Visible : Visibility.Collapsed;
        }

        private void RememberMeCheckBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                RememberMeCheckBox.IsChecked = RememberMeCheckBox.IsChecked == false;
            }
        }

        private void LoginGrid_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !RememberMeCheckBox.IsFocused)
            {
                SignInButton.Content = "";
            }
        }

        private void SignInButton_OnClick(object sender, RoutedEventArgs e)
        {
            SignInButton.Content = "";
        }
    }
}