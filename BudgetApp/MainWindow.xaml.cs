using BudgetApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace BudgetApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Varible declaration 
        /// </summary>
        string name, password;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateUser page = new CreateUser();
            page.Show();
            this.Hide();
        }

        /// <summary>
        /// Checks the validity of the login details upon clicking the login button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            name = txtName.Text;
            password = pass.Password;

            //check the password input against the stored password
                string hashPass = Calculations.HashClass.GetHash(password);
                bool passValid = UserDatabase.FindAccount(txtName.Text, hashPass);

            //confirm the username and password
            bool userFound = UserDatabase.FindUsername(name);
            if (userFound == false)
            {
                lblMessage.Visibility = Visibility.Visible;
                lblMessage.Content = "Enter the correct username.";
            }
            if (passValid == false)
            {
                lblMessage.Visibility = Visibility.Visible;
                lblMessage.Content = "Enter the correct password.";
            }
            else
            {
                //when found with all of thei information completed, the user is directed to viewing their balances
                bool found = GeneralExpensesDB.FindEntry(name);
                if (found == true)
                {
                    BudgetPage page = new BudgetPage(name);
                    page.Show();
                    this.Hide();
                }
                //when not, the user is redirected to the expense page to contiue flling out their form
                else
                {
                    AddFinancials page = new AddFinancials(name);
                    page.Show();
                    this.Hide();
                }
            }

        }
        
    }
}
