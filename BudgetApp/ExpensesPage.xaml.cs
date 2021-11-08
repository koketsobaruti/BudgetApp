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
    /// Interaction logic for ExpensesPage.xaml
    /// </summary>
    public partial class ExpensesPage : Window
    {
        /// <summary>
        /// variable declaration
        /// </summary>
        readonly string user;
        private Expenses expenses;
        Dictionary<string, decimal> otherList = new Dictionary<string, decimal>();
        public ExpensesPage()
        {
            InitializeComponent();
        }
        public ExpensesPage(string name)
        {
            InitializeComponent();
            user = name;
        }


        private void ExpensesPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            bool userRent = RentalDB.FindRentalUser(user);
            bool veh = VehicleDB.FindVehicleUser(user);
            ///if the user is a renter, then the view house loanbutton is disabled
            if (userRent == true)
            {
                btnViewHome.IsEnabled = false;
            }
            //enables the view vehicle loan button if the user chose to own a vehicle
            if (veh == false)
            {
                btnViewVehicle.IsEnabled = false;
            }

            //gets the general expenses and loads them onto the gridview
            expenses = GeneralExpensesDB.GetExpenses(user);
            //checks to see if the user has other expenses and adds them to the gridview if they do
            bool findOther = OtherExpensesDB.FindEntry(user);
            if (findOther == true)
            {
                if (userRent == true)
                {
                    decimal rentAmount = RentalDB.GetRentalAmount(user);
                    btnViewHome.IsEnabled = false;
                    otherList = OtherExpensesDB.GetOtherList(user);
                    otherList.Add("groceries", expenses.groceries);
                    otherList.Add("water and lights", expenses.utilities);
                    otherList.Add("travel", expenses.travel);
                    otherList.Add("telephone and cellphone", expenses.communication);
                    otherList.Add("rent", rentAmount);//adds the monthly rental amount if the user is a renter
                    DataGrid.ItemsSource = otherList;
                }
                //adds normal expennses if the user is not a renter
                otherList = OtherExpensesDB.GetOtherList(user);

                otherList.Add("groceries", expenses.groceries);
                otherList.Add("water and lights", expenses.utilities);
                otherList.Add("travel", expenses.travel);
                otherList.Add("telephone and cellphone", expenses.communication);
                DataGrid.ItemsSource = otherList;

            }

        }

        /// <summary>
        /// adds to a list of other expenses and then uses this to add to the datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string exp = txtName.Text;
            decimal cost = Convert.ToDecimal(txtCost.Text);
            otherList.Add(exp, cost);
            DataGrid.ItemsSource = otherList;
            txtName.Clear();
            txtCost.Clear();
        }

        private void btnViewHome_Click(object sender, RoutedEventArgs e)
        {
            HomeLoan page = new HomeLoan(user);
            page.Show();
            this.Hide();
        }

        private void btnViewVehicle_Click(object sender, RoutedEventArgs e)
        {
            VehicleLoan page = new VehicleLoan(user);
            page.Show();
            this.Hide();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            BudgetPage page = new BudgetPage(user);
            page.Show();
            this.Hide();
        }
    }
}
