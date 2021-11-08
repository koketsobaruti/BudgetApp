using BudgetApp.Classes;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for OtherExpenses.xaml
    /// </summary>
    public partial class OtherExpenses : Window
    {
        List<string> exp = new List<string>();
        List<decimal> amount = new List<decimal>();
        IDictionary<string, decimal> other = new Dictionary<string, decimal>();
        string username;
        public OtherExpenses()
        {
            InitializeComponent();
        }
        public OtherExpenses(string name)
        {
            InitializeComponent();
            username = name;
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ///adds to a list and then to the datagrid
            string exp = txtExpense.Text;
            decimal cost = Convert.ToDecimal(txtcost.Text);
            other.Add(exp, cost);
            DataGrid.ItemsSource = other;
            txtExpense.Clear();
            txtcost.Clear();
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            IDictionary<string, decimal> list = new Dictionary<string, decimal>();
            /*foreach (var item in DataGrid.Items)
            {
                DataGrid.Items.Remove(item);
                var key = item
            }*/
            //adds to the database
            foreach(KeyValuePair<string,decimal> val in other)
            {
                OtherExpensesDB.AddOtherExpenses(username, other);
            }
            this.Hide();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            /*var removed = new List<string>();
            
            var selected = DataGrid.SelectedItem;
            ///MAKE IT SELECT ONE THING
            int pos = DataGrid.SelectedIndex;
            //Removes the selected row from the datagrid
            while(selected !=null)
            {
                DataGrid.Items.Remove(selected);
            }*/
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AddFinancials page = new AddFinancials();
            page.Show();
            this.Hide();
        }
    }
}
