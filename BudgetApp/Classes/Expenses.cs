using BudgetApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp
{
    public class Expenses : House
    {
        //get set methods
        public decimal groceries { get; set; }
        public int MyProperty { get; set; }
        public decimal utilities { get; set; }
        public decimal travel { get; set; }
        public decimal communication { get; set; }

        public IDictionary<string, double> otherExpenses { get; set; }
        
        public decimal rentalAmount { get; set; }

    }

    public abstract class House : ObservableObject
    {
        decimal _purchasePrice, _deposit, _interest, _totalPayment, _monthlyPayment;
        int _monthsToPay;

        //getters and setters
        public decimal purchasePrice
        {
            get { return _purchasePrice; }
            set
            {
                ///Throws an exception if the textBox is empty
                if (string.IsNullOrWhiteSpace(value.ToString()))
                    throw new ArgumentException();

                OnPropertyChanged(ref _purchasePrice, value);
            }
        }
        public decimal deposit
        {
            get { return _deposit; }
            set
            {
                ///Throws an exception if the textBox is empty
                if (string.IsNullOrWhiteSpace(value.ToString()))
                    throw new ArgumentException();

                OnPropertyChanged(ref _deposit, value);
            }
        }
        public decimal interest
        {
            get { return _interest; }
            set
            {
                ///Throws an exception if the textBox is empty
                if (string.IsNullOrWhiteSpace(value.ToString()))
                    throw new ArgumentException();

                OnPropertyChanged(ref _interest, value);
            }
        }
        public int monthsToPay
        {
            get { return _monthsToPay; }
            set
            {
                ///Throws an exception if the textBox is empty
                if (string.IsNullOrWhiteSpace(value.ToString()))
                    throw new ArgumentException();

                OnPropertyChanged(ref _monthsToPay, value);
            }
        }
        public decimal totalHousePayment
        {
            get { return _totalPayment; }
            set
            {
                ///Throws an exception if the textBox is empty
                if (string.IsNullOrWhiteSpace(value.ToString()))
                    throw new ArgumentException("Username cannot be empty.");

                OnPropertyChanged(ref _totalPayment, value);
            }
        }
        public decimal monthlyHousePayment
        {
            get { return _monthlyPayment; }
            set
            {
                ///Throws an exception if the textBox is empty
                if (string.IsNullOrWhiteSpace(value.ToString()))
                    throw new ArgumentException("Username cannot be empty.");

                OnPropertyChanged(ref _monthlyPayment, value);
            }
        }

    }

}
