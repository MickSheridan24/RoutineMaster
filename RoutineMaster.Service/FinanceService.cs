using RoutineMaster.Data;
using Microsoft.EntityFrameworkCore;
using RoutineMaster.Models.Entities;
using Microsoft.Extensions.Logging;
using RoutineMaster.Models.Dtos;

namespace RoutineMaster.Service
{
    public class FinanceService : IFinanceService
    {
        private readonly ILogger<FinanceService> logger;
        private RMDataContext context;
        public FinanceService(RMDataContext context, ILogger<FinanceService> logger)
        {
            this.context = context;
            this.logger=logger;
        }

    public async Task CreateBudget(int userId, CreateBudgetDto budgetDto)
    {
        var budget = new Budget{
            UserId = userId,
            Name = budgetDto.Name,
            Amount = budgetDto.Amount,
            SavingsAccountId = budgetDto.FundId,
            Created = DateTime.UtcNow
        };

        context.Budgets.Add(budget);
        await context.SaveChangesAsync();
    }

    public async Task CreateExpense(int userId, ExpenseEntry expenseEntry)
    {
        expenseEntry.UserId = userId;
        expenseEntry.Date = DateTime.UtcNow;
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
        logger.LogInformation("DELETING Expense {id}", id);
        if (foundExpense != default)
        {
            context.ExpenseEntries.Remove(foundExpense);
            await context.SaveChangesAsync();
        }
    }

    public async Task DeleteBudget(int userId, int id)
    {
        var foundBudget = context.Budgets.SingleOrDefault(e => e.Id == id);
        logger.LogInformation("DELETING Budget {id}", id);
        if (foundBudget != default)
        {
            context.Budgets.Remove(foundBudget);
            await context.SaveChangesAsync();
        }
    }

    public async Task DeleteFund(int userId, int id)
    {
        var foundFund = context.SavingsAccounts.SingleOrDefault(e => e.Id == id);
        logger.LogInformation("DELETING Fund {id}", id);
        if (foundFund != default)
        {
            context.SavingsAccounts.Remove(foundFund);
            await context.SaveChangesAsync();
        }
    }

    public async Task<ICollection<BudgetDto>> GetBudgets(int userId)
    {
        return await context.Budgets.Where(b => b.UserId == userId)
        .Select(b => new BudgetDto{
            Name = b.Name,
            Id = b.Id,
            Amount = b.Amount,
            Spent = b.Entries.Select(e => e.Amount).Sum(),
            FundId = b.SavingsAccount != null ?  b.SavingsAccount!.Id : null,
            FundName = b.SavingsAccount != null ? b.SavingsAccount!.Name : null
        }).ToListAsync();
    }

    public async Task<ICollection<ExpenseEntryDto>> GetExpenses(int userId)
    {
        return await context.ExpenseEntries
        .Include(b => b.Budget)
        .OrderByDescending(e => e.Date)
        .Where(b => b.UserId == userId)
        .Select(e => new ExpenseEntryDto{
            Id= e.Id,
            Name = e.Name,
            Amount = e.Amount,
            Date = e.Date,
            BudgetId = e.Budget != null ?  e.Budget!.Id : null,
            BudgetName = e.Budget != null ? e.Budget!.Name : null
        })
        .ToListAsync();
    }

    public async Task<ICollection<SavingsAccount>> GetFunds(int userId)
    {
        return await context.SavingsAccounts.Where(b => b.UserId == userId).ToListAsync();
    }

    public async Task<UserIncomeDto> GetUserIncome(int userId)
    {
        var user = await context.Users
        .Include(u => u.UserIncome)
        .Include(u => u.Budgets)
        .Include(u => u.ExpenseEntries.Where(e => e.Date.Month == DateTime.UtcNow.Month))
        .SingleOrDefaultAsync(u => u.Id == userId);

        if(user != default){
            return new UserIncomeDto(){
                Amount = user.UserIncome.Amount,
                IncidentalBonus = user.UserIncome.IncidentalBonus,
                Remaining = user.UserIncome.Amount - user.ExpenseEntries.Select(e => e.Amount).Sum(),
                Budgeted = user.Budgets.Select(b => b.Amount).Sum()
            };
        }
        return null; 
    }

    public async Task UpdateBudget(int id, UpdateBudgetDto budget)
    {
        var foundBudget = await context.Budgets.SingleOrDefaultAsync(b => b.Id == id);
        logger.LogInformation("UPDATING {id}", id);
        if (foundBudget != default)
        {
            logger.LogInformation("FOUND {amount}, {name}", foundBudget.Amount, foundBudget.Name);

            foundBudget.Amount = budget.Amount;
            foundBudget.Name = budget.Name;
            foundBudget.SavingsAccountId = budget.FundId;
            await context.SaveChangesAsync();
        }
    }

    public async Task UpdateUserIncome(int userId, UserIncome userIncome)
    {
        var foundIncome = await context.UserIncomes.SingleOrDefaultAsync(i => i.UserId == userId);
        if (foundIncome != default)
        {
            foundIncome.IncidentalBonus = userIncome.IncidentalBonus;
            foundIncome.Amount = userIncome.Amount;
            await context.SaveChangesAsync();
        }
    }
}
}