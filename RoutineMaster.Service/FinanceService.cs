using RoutineMaster.Data;
using Microsoft.EntityFrameworkCore;
using RoutineMaster.Models.Entities;
using Microsoft.Extensions.Logging;
using RoutineMaster.Models.Dtos;
using System.Linq;

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

        public async Task CreateExpense(int userId, CreateExpenseEntryDto expenseEntry)
        {

            logger.LogInformation("CREATING EXPENSE {entry}", expenseEntry.Name);
            var newTags = expenseEntry.Tags.Where(t => t.Id == default)
            .Select(t => new Tag{Name = t.Name, UserId = userId})
            .ToArray();
            
            logger.LogInformation("CREATING TAGS {names}", string.Join(" ", newTags.Select(t => t.Name)));

            context.Tags.AddRange(newTags);

            await context.SaveChangesAsync();

            
            logger.LogInformation("CREATED TAGIDS {ids}", string.Join(" ", newTags.Select(t => t.Id)));


            
            var entity = new ExpenseEntry{
                UserId = userId,
                Date = DateTime.UtcNow,
                Name = expenseEntry.Name,
                Amount = expenseEntry.Amount,
                BudgetId = expenseEntry.BudgetId,
            };


            context.ExpenseEntries.Add(entity);

            await context.SaveChangesAsync();

            logger.LogInformation("CREATED ENTITYID {id}", entity.Id);


            var newJoins = (newTags.Select(t => new ExpenseTag{
                TagId = t.Id, 
                ExpenseEntryId = entity.Id
            }));


            context.ExpenseTags.AddRange(newJoins.ToArray());


            var existingJoins = (expenseEntry.Tags.Where(t => t.Id != default)
            .Select(t => new ExpenseTag{
                TagId = t.Id.Value, 
                ExpenseEntryId = entity.Id
            }));


            context.ExpenseTags.AddRange(existingJoins.ToArray());

            await context.SaveChangesAsync();

            logger.LogInformation("CREATED EXPENSETAGIDS {ids}", string.Join(" ", newJoins.Select(t => t.ExpenseEntryId + "/" + t.TagId)));
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
            })
            .OrderByDescending(b => b.Spent / b.Amount)
            .ToListAsync();
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
                BudgetName = e.Budget != null ? e.Budget!.Name : null,
                Tags = e.ExpenseTags.Select(et => new TagDto(){
                    Name = et.Tag.Name,
                    Id = et.TagId
                }).ToList()
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


        public async Task UpdateExpense(int userId, int id, UpdateExpenseDto expenseDto)
        {
            var foundExpense = await context.ExpenseEntries.SingleOrDefaultAsync(b => b.Id == id);
            if (foundExpense != default)
            {

                foundExpense.Amount = expenseDto.Amount;
                foundExpense.Name = expenseDto.Name;
                foundExpense.BudgetId = expenseDto.BudgetId;

                var existingExpenseTags = context.ExpenseTags.Where(et => et.ExpenseEntryId == expenseDto.Id)
                .Select(t => t.TagId).AsParallel();

                var dtoExpenseTags = expenseDto.Tags
                .Where(t => t.Id != default)
                .Select(t => t.Id.Value);

                var toDelete = existingExpenseTags.Except(dtoExpenseTags.AsParallel()).ToList();

                var toAdd = dtoExpenseTags.Except(existingExpenseTags.AsParallel()).ToList();

                var toCreate = expenseDto.Tags.Where(t => t.Id == default)
                .Select(c => new Tag{Name = c.Name, UserId = userId})
                .ToArray();

                context.ExpenseTags.RemoveRange(context.ExpenseTags.Where(t =>t.ExpenseEntryId == id && toDelete.Contains(t.TagId)));

                await context.ExpenseTags.AddRangeAsync(context.Tags.Where(t => toAdd.Contains(t.Id)).Select(t => new ExpenseTag{
                    Tag = t,
                    ExpenseEntry = foundExpense
                }));

                await context.Tags.AddRangeAsync(toCreate);
                await context.SaveChangesAsync();

                await context.ExpenseTags.AddRangeAsync(toCreate.Select(t => new ExpenseTag{
                    Tag = t,
                    ExpenseEntry = foundExpense
                }));
                await context.SaveChangesAsync();
            }
        }
    }
}