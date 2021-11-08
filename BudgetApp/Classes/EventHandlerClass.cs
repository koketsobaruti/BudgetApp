using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp.Classes
{
    public class EventHandlerClass
    {
        public EventHandler LoanFailed;
        public EventHandler MonthError;
        public void Activate()//ACTIVATES ERROR MESSAGE IF THE USER DOES NOT QUALIFY FOR A HOME LOAN
        {
            OnAlert();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// CHECKS IF THE USER ENTERED THE RIGHT DATA FOR THE MONTHS
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public bool MonthCheck(int month)
        {
            bool cont = false;
            if (month < 240 || month > 360)
            {
                OnErrorMonth();
            }
            else
            {
                cont = true;
            }
            return cont;
        }

        
        protected virtual void OnAlert()//EVENT LISTENER FOR WHEN THE USER DOE NOT QUALIFY FOR A HOUSE LOAN
        {
            LoanFailed?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnErrorMonth()//EVENT LISTENER FOR WHEN THE USER ENTERS THE UNACCEPTABLE MONTH VALUE
        {
            MonthError?.Invoke(this, EventArgs.Empty);
        }
    }
}
