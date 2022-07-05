using RoutineMaster.Models.Dtos;
using RoutineMaster.Models.Entities;

namespace RoutineMaster.Service
{
    public interface IFinanceService
    {
        Task<UserIncomeDto> GetUserIncome(int userId);
        Task<ICollection<BudgetDto>> GetBudgets(int userId);
        Task<ICollection<ExpenseEntryDto>> GetExpenses(int userId);
        Task<ICollection<SavingsAccount>> GetFunds(int userId);
        Task UpdateUserIncome(int userId, UserIncome userIncome);
        Task CreateBudget(int userId, CreateBudgetDto budget);
        Task UpdateBudget(int id, UpdateBudgetDto budget);
        Task CreateExpense(int userId, ExpenseEntry expenseEntry);
        Task CreateFund(int userId, SavingsAccount fund);
        Task DeleteExpense(int userId, int id);
        Task DeleteBudget(int userId, int id);
        Task DeleteFund(int userId, int id);
    }
}