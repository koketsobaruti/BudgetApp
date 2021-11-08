namespace BudgetApp.Classes
{
    /// <summary>
    /// Defines the <see cref="Income" />.
    /// </summary>
    internal class Income
    {
        /// <summary>
        /// Gets or sets the grossIncome.
        /// </summary>
        public decimal grossIncome { get; set; }

        /// <summary>
        /// Gets or sets the tax.
        /// </summary>
        public decimal tax { get; set; }

        /// <summary>
        /// Gets or sets the incomeAfterTax.
        /// </summary>
        public decimal incomeAfterTax { get; set; }

        /// <summary>
        /// Gets or sets the netIncome.
        /// </summary>
        public decimal netIncome { get; set; }

        /// <summary>
        /// Gets or sets the totalExpenses.
        /// </summary>
        public decimal totalExpenses { get; set; }
    }
}
