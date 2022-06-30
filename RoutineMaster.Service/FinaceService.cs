using RoutineMaster.Data;
using Microsoft.EntityFrameworkCore;
using RoutineMaster.Models.Entities;

namespace RoutineMaster.Service{
    public class FinanceService : IFinanceService{
        private RMDataContext context;
        public FinanceService(RMDataContext context){
            this.context = context;
        }

        public async Task CreateBudget(int userId, Budget budget)
        {
            budget.UserId = userId;
            context.Budgets.Add(budget);
            await context.SaveChangesAsync();
        }

        public async Task CreateExpense(int userId, ExpenseEntry expenseEntry)
        {
            expenseEntry.UserId = userId;
            context.ExpenseEntries.Add(expenseEntry);
            await context.SaveChangesAsync();
        }

        public async Task CreateFund(int userId, SavingsAccount fund)
        {
            fund.UserId = userId;
            context.SavingsAccounts.Add(fund);
            await context.SaveChangesAsync();
        }

        public async Task DeleteExpense(int userId, int id)
        {
            var foundExpense = context.ExpenseEntries.SingleOrDefault(e => e.Id == id);
            if(foundExpense != default){
                context.ExpenseEntries.Remove(foundExpense);
                await context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Budget>> GetBudgets(int userId)
        {
            return await context.Budgets.Where(b => b.UserId == userId).ToListAsync();
        }

        public async Task<ICollection<ExpenseEntry>> GetExpenses(int userId)
        {
            return await context.ExpenseEntries
            .Include(b => b.Budget)
            .Where(b => b.UserId == userId).ToListAsync();
        }

        public async Task<ICollection<SavingsAccount>> GetFunds(int userId)
        {
            return await context.SavingsAccounts.Where(b => b.UserId == userId).ToListAsync();
        }

        public async Task<UserIncome> GetUserIncome(int userId)
        {
            return await context.UserIncomes.SingleOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task UpdateBudget(int id, Budget budget)
        {
            var foundBudget = await context.Budgets.SingleOrDefaultAsync(b => b.Id == id);

            if(foundBudget != default){
                foundBudget.Amount = budget.Amount;
                foundBudget.Name = budget.Name;
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateUserIncome(int userId, UserIncome userIncome)
        {
            var foundIncome = await context.UserIncomes.SingleOrDefaultAsync(i => i.UserId == userId);
            if(foundIncome != default){
                foundIncome.IncidentalBonus = userIncome.IncidentalBonus;
                foundIncome.Amount = userIncome.Amount;
                await context.SaveChangesAsync();
            }
        }
    }
}