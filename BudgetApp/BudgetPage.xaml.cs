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
using System.Windows.Shapes;

namespace BudgetApp
{
    /// <summary>
    /// Interaction logic for BudgetPage.xaml
    /// </summary>
    public partial class BudgetPage : Window
    {
        /// <summary>
        /// variable decaration
        /// </summary>
        string username;
        Income income;
        public BudgetPage()
        {
            InitializeComponent();
        }
        public BudgetPage(string name)
        {
            InitializeComponent();
            username = name;
        }
        /// <summary>
        /// This method loads the information from the income class onto the budget page and sets the textboxes to disabled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            income = IncomeDB.GetIncome(username);
            txtIncome.Text = income.grossIncome.ToString();
            txtTax.Text = income.tax.ToString();
            txtIncomeAfterTax.Text = income.incomeAfterTax.ToString();
            txtTotalExpenses.Text = income.totalExpenses.ToString();
            txtNetIncome.Text = income.netIncome.ToString();

            //prevent the user from altering the input on load

            txtIncome.IsEnabled = false;
            txtTax.IsEnabled = false;
            txtIncomeAfterTax.IsEnabled = false;
            txtTotalExpenses.IsEnabled = false;
            txtNetIncome.IsEnabled = false;
        }

        /// <summary>
        /// enables the textboxes once the edit button has been clicked on
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            txtIncome.IsEnabled = true;
            txtTax.IsEnabled = true;
            btnEdit.IsEnabled = false;
            btnSave.IsEnabled = true;
        }

        /// <summary>
        /// updates the user's income details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var otherList = new List<decimal>();
            if (string.IsNullOrEmpty(txtIncome.Text) || string.IsNullOrEmpty(txtTax.Text))
            {
                errLbl.Visibility = Visibility.Visible;
                throw new ArgumentException();
            }
            else
            {
                /// calculate the sums of the income after and before expenses and tax
                decimal total = IncomeDB.GetTotalExpense(username);
                decimal afterTax = Calculations.IncomeCalculations.AfterTaxCalculation(Convert.ToDecimal(txtIncome.Text), Convert.ToDecimal(txtTax.Text));
                decimal accommodationCost = GetCost(username);
                //adds accommodation to overall expenses 
                decimal afterExpenses = Calculations.IncomeCalculations.AfterAllExpenses(otherList, Convert.ToDecimal(txtIncome.Text), Convert.ToDecimal(txtTax.Text), accommodationCost);

                //generate new income instance
                income = new Income
                {
                    grossIncome = Convert.ToDecimal(txtIncome.Text),
                    tax = Convert.ToDecimal(txtTax.Text),
                    incomeAfterTax = afterTax,
                    netIncome = afterExpenses,
                    totalExpenses = total
                };
                //ue this to create a thread that will update the user's income values
                Thread updateThread = new Thread(UpdateUserIncome);
                btnSave.IsEnabled = false;
                btnEdit.IsEnabled = true;

            }
            
        }

        public void UpdateUserIncome()
        {
            IncomeDB.UpdateIncome(username, income);
        }
        /// <summary>
        /// method used to get the cost f the user's accommodation.
        /// determines whether they have chosen the rental or home loan option
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static decimal GetCost(string username)
        {
            decimal cost;
            bool state = RentalDB.FindRentalUser(username);
            if (state == true)
                cost = RentalDB.GetRentalAmount(username);
            else
            {
                cost = HomeLoanDB.GetHouseAmount(username);
            }

            return cost;
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            ExpensesPage page = new ExpensesPage(username);
            page.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow page = new MainWindow();
            page.Show();
            this.Hide();
        }
    }
    
}
