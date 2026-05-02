using Expenses.API.Models.Base;

namespace Expenses.API.Models
{
    public class Transaction : BaseEntity
    {
        public string Type { get; set; }     // "Income" or "Expense"
        public double Amount { get; set; }  // transaction value
        public string Category { get; set; } // e.g., Food, Salary
    }
}
