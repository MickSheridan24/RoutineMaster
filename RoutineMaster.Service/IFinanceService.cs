using RoutineMaster.Models.Entities;

namespace RoutineMaster.Service
{
    public interface IFinanceService
    {
        Task<UserIncome> GetUserIncome(int userId);
        Task<ICollection<Budget>> GetBudgets(int userId);
        Task<ICollection<ExpenseEntry>> GetExpenses(int userId);
        Task<ICollection<SavingsAccount>> GetFunds(int userId);
        Task UpdateUserIncome(int userId, UserIncome userIncome);
        Task CreateBudget(int userId, Budget budget);
        Task UpdateBudget(int id, Budget budget);
        Task CreateExpense(int userId, ExpenseEntry expenseEntry);
        Task CreateFund(int userId, SavingsAccount fund);
        Task DeleteExpense(int userId, int id);
    }
}