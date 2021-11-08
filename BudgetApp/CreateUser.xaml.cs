using BudgetApp.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace BudgetApp
{
    /// <summary>
    /// Interaction logic for CreateUser.xaml
    /// </summary>
    public partial class CreateUser : Window
    {
        /// <summary>
        /// variable declaration
        /// </summary>
        UserClass user;
        IDictionary<string, decimal> other = new Dictionary<string, decimal>();
        private string _hashedPassword;
        private string _name, _surname, _username, _passA, _passB;

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow page = new MainWindow();
            page.Show();
            this.Hide();
        }

        public CreateUser()
        {
            InitializeComponent();
        }
        /// <summary>
        /// creates a new instance of the user class and writes it to the user database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            bool found = false;
            _name = txtName.Text;
            _surname = txtSurname.Text;
            _username = txtUsername.Text;
            _passA = txtPass.Password;
            _passB = txtConfirm.Password;

            if (_passA.Equals(_passB))
            {
                found = UserDatabase.FindAccount(_name, _passA);
                if(found==false)
                {
                    //get the calculation of the hashed pasword from the calculations library
                    _hashedPassword = Calculations.HashClass.GetHash(_passA);
                    user = new UserClass
                    {
                        Name = _name,
                        Surname = _surname,
                        Username = _username,
                        Password = _hashedPassword
                    };
                    //create a thread to write to the database with
                    Thread writeThread = new Thread(DoWriteToUserDB);
                    writeThread.Start();
                    //open automatically to the next page
                    AddFinancials page = new AddFinancials(user.Username);
                    page.Show();
                    this.Hide();
                }
            }

        }
        /// <summary>
        /// method to write to the user database
        /// </summary>
        private void DoWriteToUserDB()
        {
            UserDatabase.AddUser(user);
        }
    }
}
