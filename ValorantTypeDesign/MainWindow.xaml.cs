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
using System.Windows.Media.Animation;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ValorantTypeDesign
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Warning warning = new Warning();
        public MainWindow()
        {
            InitializeComponent();
            RefreshDataContext();
        }

        private void btnCloseApp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimizeApp_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ToolBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btnLostFocus.Focus();
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void tbLoginField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbLoginField.Text))
            {
                ThicknessAnimation textMarginAnimation = new ThicknessAnimation();
                DoubleAnimation textSizeAnimation = new DoubleAnimation();
                textSizeAnimation.From = 14;
                textSizeAnimation.To = 10;
                textSizeAnimation.Duration = TimeSpan.FromSeconds(0.2);

                textMarginAnimation.From = new Thickness(18, 17, 0, 0);
                textMarginAnimation.To = new Thickness(8, 6, 0, 0);
                textMarginAnimation.Duration = TimeSpan.FromSeconds(0.2);


                tblockLoginField.BeginAnimation(MarginProperty, textMarginAnimation);
                tblockLoginField.BeginAnimation(FontSizeProperty, textSizeAnimation);
            }

        }

        private void tbLoginField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbLoginField.Text))
            {
                ThicknessAnimation textMarginAnimation = new ThicknessAnimation();
                DoubleAnimation textSizeAnimation = new DoubleAnimation();
                textSizeAnimation.From = 10;
                textSizeAnimation.To = 14;
                textSizeAnimation.Duration = TimeSpan.FromSeconds(0.2);

                textMarginAnimation.From = new Thickness(8, 6, 0, 0);
                textMarginAnimation.To = new Thickness(18, 17, 0, 0);
                textMarginAnimation.Duration = TimeSpan.FromSeconds(0.2);


                tblockLoginField.BeginAnimation(MarginProperty, textMarginAnimation);
                tblockLoginField.BeginAnimation(FontSizeProperty, textSizeAnimation);
            }


            RefreshDataContext();

        }

        private void tbPasswordField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbPasswordField.Password))
            {
                ThicknessAnimation textMarginAnimation = new ThicknessAnimation();
                DoubleAnimation textSizeAnimation = new DoubleAnimation();
                textSizeAnimation.From = 14;
                textSizeAnimation.To = 10;
                textSizeAnimation.Duration = TimeSpan.FromSeconds(0.2);

                textMarginAnimation.From = new Thickness(18, 30, 0, 0);
                textMarginAnimation.To = new Thickness(8, 20, 0, 0);
                textMarginAnimation.Duration = TimeSpan.FromSeconds(0.2);


                tblockPasswordField.BeginAnimation(MarginProperty, textMarginAnimation);
                tblockPasswordField.BeginAnimation(FontSizeProperty, textSizeAnimation);
            }
        }

        private void tbPasswordField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbPasswordField.Password))
            {
                ThicknessAnimation textMarginAnimation = new ThicknessAnimation();
                DoubleAnimation textSizeAnimation = new DoubleAnimation();
                textSizeAnimation.From = 10;
                textSizeAnimation.To = 14;
                textSizeAnimation.Duration = TimeSpan.FromSeconds(0.2);

                textMarginAnimation.From = new Thickness(8, 20, 0, 0);
                textMarginAnimation.To = new Thickness(18, 30, 0, 0);
                textMarginAnimation.Duration = TimeSpan.FromSeconds(0.2);


                tblockPasswordField.BeginAnimation(MarginProperty, textMarginAnimation);
                tblockPasswordField.BeginAnimation(FontSizeProperty, textSizeAnimation);
            }
            RefreshDataContext();
        }

        private void tbLoginField_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool ActivateLogInButton = true;
            if (tbLoginField.Text.Length < 2 || string.IsNullOrEmpty(tbPasswordField.Password))
            {
                ActivateLogInButton = false;
            }
            if (tbLoginField.Text == "аа")
            {
                ActivateLogInButton = false;
            }
            btnLogIn.IsEnabled = ActivateLogInButton;
        }
        public class Warning
        {
            public string Message { get; set; }
            public bool HasWarning { get; set; }
        }
        void RefreshDataContext()
        {
            if (tbLoginField.Text.Length == 1)
            {
                warning.Message = "Должно быть не менее 2 символов ";
                warning.HasWarning = true;
                btnLogIn.IsEnabled = false;
            }
            else
            {
                warning.Message = "";
                warning.HasWarning = false;
            }

            if (tbLoginField.Text.Length > 1 && !Regex.Match(tbLoginField.Text, @"^[a-zA-Z0-9]+$").Success)
            {
                warning.Message = "Специальные символы в имени\nпользователя недопустимы";
                warning.HasWarning = true;
                btnLogIn.IsEnabled = false;
            }

            tblockWarningLogin.DataContext = null;
            tbLoginField.DataContext = null;
            spLoginWarning.DataContext = null;
            tblockWarningLogin.DataContext = warning;
            tbLoginField.DataContext = warning;
            spLoginWarning.DataContext = warning;
        }

        private void btnGoToAccount_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://update-account.riotgames.com/?locale=ru_RU");
        }

        private void btnHelpWeb_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://support.riotgames.com/hc/ru");
        }

        private void tbPasswordField_PasswordChanged(object sender, RoutedEventArgs e)
        {
            bool ActivateLogInButton = true;
            if (tbLoginField.Text.Length < 2 || string.IsNullOrEmpty(tbPasswordField.Password))
            {
                ActivateLogInButton = false;
            }
            if (new Regex(@"^([A-za-z0-9]|[\.\^\$\*\+\?\{\}\[\]\\\|\(\)=])*$", RegexOptions.IgnoreCase).IsMatch(tblockLoginField.Text))
            {
                MessageBox.Show("Лол");
                ActivateLogInButton = false;
            }
            btnLogIn.IsEnabled = ActivateLogInButton;
        }
    }
}
