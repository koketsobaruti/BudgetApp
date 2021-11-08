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
using System.Windows.Shapes;
using BudgetApp.Classes;

namespace BudgetApp
{
    /// <summary>
    /// Interaction logic for HomeLoan.xaml
    /// </summary>
    public partial class HomeLoan : Window
    {
        Expenses home = new Expenses();
        private string user;
        public HomeLoan()
        {
            InitializeComponent();
        }
        public HomeLoan(string name)
        {
            InitializeComponent();
            user = name;
        }
        /// <summary>
        /// gets the loan values and populates them into the textboxes. Disables them so the uer cannot change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HomeLoan_OnLoaded(object sender, RoutedEventArgs e)
        {
            home = HomeLoanDB.GetHomeLoan(user);
            Txtprice.Text = home.purchasePrice.ToString();
            txtDeposit.Text = home.deposit.ToString();
            txtInterest.Text = home.interest.ToString();
            txtMonths.Text = home.monthsToPay.ToString();

            Txtprice.IsEnabled = false;
            txtDeposit.IsEnabled = false;
            txtInterest.IsEnabled = false;
            txtMonths.IsEnabled = false;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ExpensesPage page = new ExpensesPage(user);
            page.Show();
            this.Hide();
        }

    }
}
