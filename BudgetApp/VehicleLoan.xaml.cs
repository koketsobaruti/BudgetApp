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
    /// Interaction logic for VehicleLoan.xaml
    /// </summary>
    public partial class VehicleLoan : Window
    {
        Vehicle vehicle = new Vehicle();
        private string user;
        public VehicleLoan()
        {
            InitializeComponent();
        }
        public VehicleLoan(string name)
        {
            InitializeComponent();
            user = name;
        }
        /// <summary>
        /// gets the vehicle details from the vehicle database and populates them into th designated textboxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VehicleLoan_OnLoaded(object sender, RoutedEventArgs e)
        {
            vehicle = VehicleDB.GetVehicleLoan(user);
            txtMake.Text = vehicle.make;
            txtModel.Text = vehicle.model;
            txtPrice.Text = vehicle.purchasePrice.ToString();
            txtDeposit.Text = vehicle.totalDeposit.ToString();
            txtInterest.Text = vehicle.interestRate.ToString();
            txtInsurance.Text = vehicle.insurance.ToString();

            txtMake.IsEnabled = false;
            txtModel.IsEnabled = false;
            txtPrice.IsEnabled = false;
            txtDeposit.IsEnabled = false;
            txtInterest.IsEnabled = false;
            txtInsurance.IsEnabled = false;
        }

       
        private void BtnBack_OnClick(object sender, RoutedEventArgs e)
        {
            ExpensesPage page = new ExpensesPage(user);
            page.Show();
            this.Hide();
        }

    }
}
