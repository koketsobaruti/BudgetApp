using BudgetApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp
{
    class Vehicle : ObservableObject
    {
        //get set methods
        public string make { get; set; }
        public string model { get; set; }
        public decimal purchasePrice { get; set; }
        public decimal totalDeposit { get; set; }
        public decimal interestRate { get; set; }
        public int MONTHSTOPAY = 5; 
        public decimal insurance { get; set; }
        public decimal totalVehiclePayment { get; set; }
        public decimal vehicleMonthlyPayment { get; set; }

    }
}
