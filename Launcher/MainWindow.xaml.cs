using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Brushes = System.Windows.Media.Brushes;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private System.Windows.Forms.NotifyIcon MyNotifyIcon;

        private List<char> _emailAllowedSymbols;
        private List<char> _usernameAllowedSymbols;
        private List<char> _digits;

        private bool _regUsernameGood;
        private bool _regEmailGood;
        private bool _regPasswordGood;

        public MainWindow()
        {
            InitializeComponent();
            MyNotifyIcon = new System.Windows.Forms.NotifyIcon
            {
                Icon = new Icon("launcher.ico")
            };
            MyNotifyIcon.MouseClick += MyNotifyIcon_OnMouseClick;

            _emailAllowedSymbols = new List<char>();
            for (var c = 'A'; c <= 'Z'; c++)
            {
                _emailAllowedSymbols.Add(c);
            }
            for (var c = 'a'; c <= 'z'; c++)
            {
                _emailAllowedSymbols.Add(c);
            }
            _digits = new List<char>();
            for (var c = '0'; c <= '9'; c++)
            {
                _emailAllowedSymbols.Add(c);
                _digits.Add(c);
            }
            _emailAllowedSymbols.Add('@');
            _emailAllowedSymbols.Add('.');

            _usernameAllowedSymbols = new List<char>(_emailAllowedSymbols);
            _usernameAllowedSymbols.Remove('@');
            _usernameAllowedSymbols.Remove('.');

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

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CollapseButton_OnClick(object sender, RoutedEventArgs e)
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

        private void LoginGrid_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (UsernameTextBox.Text.Length > 0 && UserPasswordBox.Password.Length > 0)
            {
                SignInButton.IsEnabled = true;
            }
            else
            {
                SignInButton.IsEnabled = false;
            }
        }

        private void SignInButton_OnClick(object sender, RoutedEventArgs e)
        {
            SignInButton.Content = "";
        }

        private void RegisterButton_OnClick(object sender, RoutedEventArgs e)
        {
            LoginGrid.Visibility = Visibility.Collapsed;
            LoginGrid.IsEnabled = false;
            RegisterGrid.Visibility = Visibility.Visible;
            RegisterGrid.IsEnabled = true;
        }

        private void RegisterGrid_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (_regEmailGood && _regPasswordGood && _regUsernameGood)
            {
                SignUpButton.IsEnabled = true;
            }
            else
            {
                SignUpButton.IsEnabled = false;
            }
        }

        private bool RegPasswordCheck()
        {
            if (RegUserPasswordBox.Password.Length < 6)
            {
                _regPasswordGood = false;
                return false;
            }
            _regPasswordGood = RegUserPasswordBox.Password == RegUserPasswordCheckBox.Password;
            return _regPasswordGood;
        }

        private void RegPasswordCheckColor(bool check)
        {
            if (!check)
            {
                var brush = Brushes.Red.CloneCurrentValue();
                brush.Opacity = 0.5;
                RegUserPasswordBox.Background = brush;
                RegUserPasswordCheckBox.Background = brush;
            }
            else
            {
                RegUserPasswordBox.Background = UsernameTextBox.Background;
                RegUserPasswordCheckBox.Background = UsernameTextBox.Background;
            }
        }

        private void RegUserPasswordCheckBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            RegPasswordCheckWatermark.Visibility = RegUserPasswordCheckBox.Password == "" ? Visibility.Visible : Visibility.Collapsed;
            RegPasswordCheckColor(RegPasswordCheck());
        }

        private void RegUserPasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            RegPasswordWatermark.Visibility = RegUserPasswordBox.Password == "" ? Visibility.Visible : Visibility.Collapsed;
            RegPasswordCheckColor(RegPasswordCheck());
        }

        private void SignUpButton_OnClick(object sender, RoutedEventArgs e)
        {
            SignUpButton.Content = "";
        }

        private void RegisterBackButton_OnClick(object sender, RoutedEventArgs e)
        {
            RegisterGrid.Visibility = Visibility.Collapsed;
            RegisterGrid.IsEnabled = false;
            LoginGrid.Visibility = Visibility.Visible;
            LoginGrid.IsEnabled = true;
        }

        private bool RegCheckEmail()
        {
            var email = RegEmailTextBox.Text;
            if (email == "")
            {
                _regEmailGood = false;
                return true;
            }
            var check = email.Contains('@') && email.Contains('.');
            if (check)
            {
                foreach (var c in email)
                {
                    if (_emailAllowedSymbols.Contains(c)) continue;
                    check = false;
                    break;
                }
            }
            _regEmailGood = check;
            return check;
        }

        private void RegCheckEmailColor(bool check)
        {
            if (check)
            {
                RegEmailTextBox.Background = UsernameTextBox.Background;
            }
            else
            {
                var brush = Brushes.Red.CloneCurrentValue();
                brush.Opacity = 0.5;
                RegEmailTextBox.Background = brush;
            }
        }

        private void RegEmailTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            RegCheckEmailColor(RegCheckEmail());
        }

        private bool RegUsernameCheck()
        {
            var username = RegUsernameTextBox.Text;
            if (username == "")
            {
                _regUsernameGood = false;
                return true;
            }

            if (_digits.Contains(username[0]))
            {
                _regUsernameGood = false;
                return false;
            }
            var check = username.Length >= 3;
            if (check)
            {
                foreach (var c in username)
                {
                    if (_usernameAllowedSymbols.Contains(c)) continue;
                    check = false;
                    break;
                }
            }
            _regUsernameGood = check;
            return check;
        }

        private void RegUsernameCheckColor(bool check)
        {
            if (check)
            {
                RegUsernameTextBox.Background = UsernameTextBox.Background;
            }
            else
            {
                var brush = Brushes.Red.CloneCurrentValue();
                brush.Opacity = 0.5;
                RegUsernameTextBox.Background = brush;
            }
        }

        private void RegUsernameTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            RegUsernameCheckColor(RegUsernameCheck());
        }
    }
}