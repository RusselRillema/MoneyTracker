using MoneyTracker.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Dal
{
    public class ExpenseDatabase
    {
        readonly SQLiteAsyncConnection database;
        public ExpenseDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Expense>().Wait();
        }

        public Task<List<Expense>> GetExpensesAsync()
        {
            return database.Table<Expense>().ToListAsync();
        }

        public Task<Expense> GetExpenseAsync(int id)
        {
            return database.Table<Expense>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveExpenseAsync(Expense expense)
        {
            if (expense.ID == 0)
                return database.InsertAsync(expense);
            else
                return database.UpdateAsync(expense);
        }

        public Task<int> DeleteExpenseAsync(Expense expense)
        {
            return database.DeleteAsync(expense);
        }
    }
}
