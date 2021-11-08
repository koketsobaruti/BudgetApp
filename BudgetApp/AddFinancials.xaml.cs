using BudgetApp.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BudgetApp
{
    /// <summary>
    /// Interaction logic for AddFinancials.xaml
    /// </summary>
    public partial class AddFinancials : Window
    {
        //IDictionary<string, decimal> other = new Dictionary<string, decimal>();
        string user;
        Income income;
        Expenses expenses;
        decimal rentalAmount;
        string propertyType;

        public AddFinancials()
        {
            InitializeComponent();
        }
        public AddFinancials(string u)
        {
            InitializeComponent();
            user = u;
        }

        private void btnAddOther_Click(object sender, RoutedEventArgs e)
        {
            OtherExpenses page = new OtherExpenses(user);
            page.Show();
            
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            decimal total = 0;
            var otherList = new List<decimal>();
            if (string.IsNullOrEmpty(txtTax.Text) || string.IsNullOrEmpty(txtIncome.Text) || string.IsNullOrEmpty(txtGroceries.Text) || string.IsNullOrEmpty(txtUtilities.Text)
                || string.IsNullOrEmpty(txtTravel.Text) || string.IsNullOrEmpty(txtcommunication.Text))
                throw new ArgumentException("Null or empty values are not allowed.");

            else
            {
                expenses = new Expenses
                {
                    groceries = Convert.ToDecimal(txtGroceries.Text),
                    utilities = Convert.ToDecimal(txtUtilities.Text),
                    communication = Convert.ToDecimal(txtcommunication.Text),
                    travel = Convert.ToDecimal(txtTravel.Text)
                };
                bool found = OtherExpensesDB.FindEntry(user);

                if (found == true)
                {
                    otherList = OtherExpensesDB.GetCostList(user);
                    otherList = GetExpenseList(otherList, expenses);

                    total = Calculations.IncomeCalculations.TotalExpensesCalculation(otherList);
                }
                else
                {
                     total = expenses.groceries + expenses.utilities + expenses.communication +
                                       expenses.travel;

                }

                decimal afterTax = Calculations.IncomeCalculations.AfterTaxCalculation(Convert.ToDecimal(txtIncome.Text), Convert.ToDecimal(txtTax.Text));
                decimal accomodationCost = GetCost(user);
                decimal afterExpenses = Calculations.IncomeCalculations.AfterAllExpenses(otherList, Convert.ToDecimal(txtIncome.Text), Convert.ToDecimal(txtTax.Text), accomodationCost);

                income = new Income
                {
                    grossIncome = Convert.ToDecimal(txtIncome.Text),
                    tax = Convert.ToDecimal(txtTax.Text),
                    incomeAfterTax = afterTax,
                    netIncome = afterExpenses,
                    totalExpenses = total
                };

                GeneralExpensesDB.AddGeneralExpenses(user, expenses);
                IncomeDB.AddIncome(user,income);

                BudgetPage page = new BudgetPage(user);
                page.Show();
                this.Hide();
            }
        }

        public static List<decimal> GetExpenseList(List<decimal> otherList, Expenses exp)
        {
            otherList.Add(exp.groceries);
            otherList.Add(exp.utilities);
            otherList.Add(exp.communication);
            otherList.Add(exp.travel);
            return otherList;
        }

        public static decimal GetCost(string username)
        {
            decimal cost;
            bool state=RentalDB.FindRentalUser(username);
            if (state == true)
                cost = RentalDB.GetRentalAmount(username);
            else
            {
                cost = HomeLoanDB.GetHouseAmount(username);
            }

            return cost;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void rdRent_Checked(object sender, RoutedEventArgs e)
        {
            string rent = "";
            InputDialog inputDialog = new InputDialog("Please enter the monthly rental amount in rands.");
            if (inputDialog.ShowDialog() == true)
                rent = inputDialog.Answer;
            else
            {
                rent = "cancel";
            }
            

            if (rent != "cancel")
            {
                propertyType = "rent";
                rentalAmount = Convert.ToDecimal(rent);
                RentalDB.AddRental(user, rentalAmount);
            } 
        }

        public void rdHouse_Checked(object sender, RoutedEventArgs e)
        {
            bool userHouse = HomeLoanDB.FindHouseUser(user);
            if (userHouse == false)
            {
                if (string.IsNullOrEmpty(txtIncome.Text))
                {
                    lblErrorHouse.Visibility = Visibility.Visible;
                    lblErrorHouse.Content = "*Enter your gross income";
                    rdHouse.IsChecked = false;
                }
                else
                {
                    lblErrorHouse.Visibility = Visibility.Hidden;
                    AddPropertyPage page = new AddPropertyPage(user, Convert.ToDecimal(txtIncome.Text));
                    page.Show();
                }
            }
            rdHouse.IsEnabled = false;
            
        }

        private void rdVehichle_Checked(object sender, RoutedEventArgs e)
        {
            AddVehicle page = new AddVehicle(user);
            page.Show();
        }

        private void rdRent_Click(object sender, RoutedEventArgs e)
        {
        }

        private void rdHouse_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Expenses rentExp = new Expenses();
            bool userIncome = IncomeDB.FindUserIncome(user);
            bool userExpenses = GeneralExpensesDB.FindEntry(user);
            bool userRent = RentalDB.FindRentalUser(user);
            bool userHouse = HomeLoanDB.FindHouseUser(user);

            if (userIncome == true)
            {
                //populate
                income = IncomeDB.GetIncome(user);
                txtIncome.Text = income.grossIncome.ToString();
                txtTax.Text = income.tax.ToString();
            }

            if (userExpenses == true)
            {
                //populate
                expenses = GeneralExpensesDB.GetExpenses(user);
                txtGroceries.Text = expenses.groceries.ToString();
                txtUtilities.Text = expenses.utilities.ToString();
                txtcommunication.Text = expenses.communication.ToString();
                txtTravel.Text = expenses.travel.ToString();
            }
                

            if (userRent == true)
            {
                //populate
                rdRent.IsChecked = true;
                rdHouse.IsEnabled = false;
            }


            if (userHouse == true)
            {
                //populate
                rdHouse.IsChecked = true;
                rdRent.IsEnabled = false;
            }
                
        }
    }
}
