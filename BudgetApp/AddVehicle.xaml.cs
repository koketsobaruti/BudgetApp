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
    /// Interaction logic for AddVehicle.xaml
    /// </summary>
    public partial class AddVehicle : Window
    {
        Vehicle vehicle;
        string user;
        decimal monthlyPayment, total;
        public AddVehicle()
        {
            InitializeComponent();
        }
        public AddVehicle(string u)
        {
            InitializeComponent();
            user = u;
        }
        /// <summary>
        /// method used to check if the data that has been entered is null. it writes to the database upon success
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ///check if any of the input boxes are null
            if (string.IsNullOrEmpty(txtModel.Text) || string.IsNullOrEmpty(txtMake.Text) ||
                string.IsNullOrEmpty(txtPrice.Text)
                || string.IsNullOrEmpty(txtInterest.Text) || string.IsNullOrEmpty(txtInsurance.Text) ||
                string.IsNullOrEmpty(txtDeposit.Text))

                errorLbl.Visibility = Visibility.Visible;
                
            else
            { 
                //use the calculation library class to get the total monthly amount to be paid as will as the total cost of the car
                decimal totalMonthlyPayment = Calculations.PaymentsCalculations.VehicleRepayementCalculation(60, Convert.ToDecimal(txtPrice.Text), Convert.ToDecimal(txtInterest.Text),
                                                                    Convert.ToDecimal(txtDeposit.Text),Convert.ToDecimal(txtInsurance.Text));

                decimal totalCarPayment = Calculations.PaymentsCalculations.TotalPaymentCalculation(60, Convert.ToDecimal(txtPrice.Text),Convert.ToDecimal(txtInterest.Text),
                                                                    Convert.ToDecimal(txtDeposit.Text));
                //create an instance of the vehicle class
                vehicle = new Vehicle
                {
                    model = txtModel.Text,
                    make = txtMake.Text,
                    purchasePrice = Convert.ToDecimal(txtPrice.Text),
                    interestRate = Convert.ToDecimal(txtInterest.Text),
                    insurance = Convert.ToDecimal(txtInsurance.Text),
                    totalDeposit = Convert.ToDecimal(txtDeposit.Text),
                    vehicleMonthlyPayment = totalMonthlyPayment,
                    totalVehiclePayment = totalCarPayment
                };
                Thread writeThread = new Thread(DoWriteToDB);
                writeThread.Start();
                VehicleDB.AddVehicle(user, vehicle);
                this.Close();
            }
                
            
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AddFinancials page = new AddFinancials();
            page.Show();
            this.Hide();
        }

        public static void EmptyException()
        {
            
        }
        private void DoWriteToDB()
        {
            Thread.Sleep(5000); 
            VehicleDB.AddVehicle(user, vehicle);
        }
    }
}
