using BudgetApp.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp
{
    public class UserClass : ObservableObject
    {
        public string Error { get { return null; } }
        private string _username, _name, _surname, _password;
        
        public string Name
        {
            get { return _name; }
            set
            {
                ///Throws an exception if the textBox is empty
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Username cannot be empty.");

                OnPropertyChanged(ref _name, value);
            }
        }


        public string Surname
        {
            get { return _surname; }
            set
            {
                ///Throws an exception if the textBox is empty
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException();

                OnPropertyChanged(ref _surname, value);
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                ///Throws an exception if the textBox is empty
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException();

                OnPropertyChanged(ref _password, value);
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                ///Throws an exception if the textBox is empty
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException();

                OnPropertyChanged(ref _username, value);
            }
        }

    }
}
