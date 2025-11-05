using BusinessLayer.Services;
using DataAccessLayer;
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
using System.Windows.Shapes;

namespace ResearchProjectManagement_SE123456
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly UserAccountService _userAccountRepo = new UserAccountService();
        public Login()
        {

            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;

            MessageBox.Show($"Username: {email}\nPassword: {password}", "Login Info", MessageBoxButton.OK, MessageBoxImage.Information);

            // TODO: Get user from database
            var _userAccountService = new UserAccountService();
            var loginUser = _userAccountService.GetByEmail(email);

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter username and password"); // Theo de bai
            }
            else if (loginUser.Email.Equals(email) && loginUser.Password.Equals(password))
            {
                if (loginUser.Role.Value == 2 || loginUser.Role.Value == 3)
                {
                    ResearchList researchListWindow = new ResearchList(loginUser);

                    researchListWindow.Show();

                }
                else
                {
                    MessageBox.Show("You have no permission to access this function.");
                }

            }

        }
    }
}
