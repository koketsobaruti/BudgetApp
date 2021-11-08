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
    /// Interaction logic for AddPropertyPage.xaml
    /// </summary>
    public partial class AddPropertyPage : Window
    {
        string username;
        decimal price, deposit, interest,grossIncome;
        int months;
        EventHandlerClass ev = new EventHandlerClass();
        LoanRefusal loanRefusal = new LoanRefusal();
        ErrorMonths errorMonths = new ErrorMonths();
        public AddPropertyPage()
        {
            InitializeComponent();
        }
        public AddPropertyPage(string name,decimal income)
        {
            InitializeComponent();

            ///SUBSCRIBING EVENTS
            ev.LoanFailed += loanRefusal.OnLoanRefusal;
            ev.MonthError += errorMonths.OnWrongMonthEntry;
            username = name;
            grossIncome = income;
        }
        /// <summary>
        /// method used to check the validity of user input and and store iit into the database upon validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            price = Convert.ToDecimal(txtPrice.Text);
            deposit = Convert.ToDecimal(txtDeposit.Text);
            interest = Convert.ToDecimal(txtInterest.Text);
            months = Convert.ToInt32(txtMonths.Text);

            if (string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(txtDeposit.Text) ||
                string.IsNullOrEmpty(txtInterest.Text) || string.IsNullOrEmpty(txtMonths.Text))
            {
                //displays error message if the input is empty
                lblmessage.Visibility = Visibility.Visible;
            }
            else
            {
                //checks if the user is eligable for a home loan and throws an exception if they are not
                bool eligeable = Calculations.PaymentsCalculations.HomeLoanEligability(grossIncome, price, interest, months, deposit);
                if (eligeable == true)
                {
                    //checks if the user input the correct range of months throws an exception if not
                    bool monthState = ev.MonthCheck(months);
                    if (monthState == true)
                    {
                        //uses the calculations library to get the totals of the home loan
                        decimal monthlyPayment = Calculations.PaymentsCalculations.MonthlyPaymentCalculation(months, price, interest, deposit);
                        decimal total = Calculations.PaymentsCalculations.TotalPaymentCalculation(months, price, interest, deposit);
                        var home = new Expenses //creates an object
                        {
                            purchasePrice = price,
                            deposit = deposit,
                            interest = interest,
                            monthsToPay = months,
                            monthlyHousePayment = monthlyPayment,
                            totalHousePayment = total
                        };

                        Thread addHomeThread = new Thread(()=>AddHome(home));
                        addHomeThread.Start();
                        this.Close();
                    }

                }
                else
                {
                    //signals an error message evet handler if the user does not qualify and automatically exists the window
                    ev.Activate();
                    this.Close();
                }
            }
            
            
        }

        /// <summary>
        /// thread to add an instance of home into the database
        /// </summary>
        /// <param name="home"></param>
        private void AddHome(Expenses home)
        {
            HomeLoanDB.AddHomeLoan(username, home);
        }


    }

    public class LoanRefusal
    {
        /// <summary>
        /// event listener for once the user has not qualified for a loan approval
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        public void OnLoanRefusal(object source, EventArgs args)
        {
            //
            // Dialog box with exclamation icon.
            //
            MessageBox.Show("Your home loan approval is unlikely\nas it is more than a third of your grossly income.\nPick a rental option.",
                "Loan Denied",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
    /// <summary>
    /// EVENT LISTENER WITH ERROR MESSAGE IF THE USER INPUT INVALID MONTHS
    /// </summary>
    

    public class ErrorMonths
    {
        /// <summary>
        /// event listener for once the user has entered more or less than 240 or 360 months
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void OnWrongMonthEntry(object source, EventArgs e)
        {
            //
            // Dialog box with exclamation icon.
            //
            MessageBox.Show("Enter months between 240 and 360.",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Exclamation);
        }
    }
}
