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
        private readonly IScoreService scoreService;
        private RMDataContext context;
        public FinanceService(RMDataContext context, IScoreService scoreService, ILogger<FinanceService> logger)
        {
            this.context = context;
            this.logger=logger;
            this.scoreService = scoreService;
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

            var now = DateTime.UtcNow;

            logger.LogCritical("NOW {now}",  now);
            
            var entity = new ExpenseEntry{
                UserId = userId,
                Date = now,
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

            await UpdateScore();
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

            await UpdateScore();

        }

        public async Task DeleteBudget(int userId, int id)
        {
            var foundBudget = context.Budgets
            .Include(b => b.Entries)
            .SingleOrDefault(e => e.Id == id);
            logger.LogInformation("DELETING Budget {id}", id);
            if (foundBudget != default)
            {
                if(foundBudget.Entries.Count() > 0){
                    foundBudget.Archived = true;
                }
                else context.Budgets.Remove(foundBudget);
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
            return await context.Budgets
            .Where(b => b.UserId == userId && !b.Archived)
            .Select(b => new BudgetDto{
                Name = b.Name,
                Id = b.Id,
                Amount = b.Amount,
                Spent = b.Entries.Where(e => e.Date.Month == DateTime.Now.Month).Select(e => e.Amount).Sum(),
                FundId = b.SavingsAccount != null ?  b.SavingsAccount!.Id : null,
                FundName = b.SavingsAccount != null ? b.SavingsAccount!.Name : null
            })
            .OrderByDescending(b => b.Spent / (b.Amount  == 0 ? 1.0 : b.Amount))
            .ToListAsync();
        }

        public async Task<ICollection<ExpenseEntryDto>> GetExpenses(int userId)
        {
            return await context.ExpenseEntries
            .Include(b => b.Budget)
            .OrderByDescending(e => e.Date)
            .Where(b => b.UserId == userId && b.Date.Month == DateTime.UtcNow.Month)
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
            .Include(u => u.ExpenseEntries.Where(e => e.Date.Month == DateTime.UtcNow.Month && e.Date.Year == DateTime.UtcNow.Year))
            .SingleOrDefaultAsync(u => u.Id == userId);

            if(user != default){
                return new UserIncomeDto(){
                    Amount = user.UserIncome.Amount,
                    IncidentalBonus = user.UserIncome.IncidentalBonus,
                    Remaining = user.UserIncome.Amount + user.UserIncome.IncidentalBonus - user.ExpenseEntries
                    .Select(e => e.Amount).Sum(),
                    Budgeted = user.Budgets
                    .Where(b => !b.Archived)
                    .Select(b => b.Amount).Sum()
                };
            }
            return null; 
        }

        public async Task<IEnumerable<FinanceStatsSummaryDto>> GetFinanceSummary(){
           
            var ret = new List<FinanceStatsSummaryDto>();
            for(var i = 0; i < DateTime.UtcNow.Day; i++){

                var daySummary = new FinanceStatsSummaryDto{
                    Present = context.ExpenseEntries.Where(e => e.Date.Day <= i + 1 && e.Date.Month == DateTime.UtcNow.Month && e.Date.Year == DateTime.UtcNow.Year)
                    .Sum(e => e.Amount),
                    LastMonth = context.ExpenseEntries.Where(e => e.Date.Day <= i + 1 && e.Date.Month == DateTime.UtcNow.AddMonths(-1).Month && e.Date.Year == DateTime.UtcNow.Year)
                    .Sum(e => e.Amount),
                    Average = CalculateAvgExpenseToDay(i + 1, DateTime.Parse("07-01-2022"))
                };
                ret.Add(daySummary);
            }

            return ret;
           
        }

        private double CalculateAvgExpenseToDay(int day, DateTime? startDate = null){
            var groupedByMonth = context.ExpenseEntries
            .Where(e => e.Date.Year == DateTime.UtcNow.Year && e.Date.Day <= day) 
            .ToList()
            .GroupBy(e => e.Date.Month + " " + e.Date.Year, e => e);

            var sums = groupedByMonth.Select(g => g.Sum(e => e.Amount));

            if (startDate == null) startDate = DateTime.Parse(DateTime.UtcNow.Year.ToString());

            var emptyMonths = 0;

            for(var i = startDate.Value.Month; i <= DateTime.UtcNow.Month; i++){
                if(!context.ExpenseEntries.Any(e => e.Date.Day <= day && e.Date.Month == i && e.Date.Year == startDate.Value.Year)){
                    logger.LogCritical("EMPTY MONTH {m}", i);
                    emptyMonths ++;
                }
            }


            return  groupedByMonth.Count() > 0 ? sums.Sum() / (groupedByMonth.Count() + emptyMonths) : 0;
        }    

        private bool IsCurrentMonth(DateTime date)    {
            return date.Month == DateTime.UtcNow.Month && date.Year == DateTime.UtcNow.Year;
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

                foreach (var tag in expenseDto.Tags)
                {
                    var found = context.Tags.FirstOrDefault(t => t.Name.ToLower() == tag.Name.ToLower());
                    if(found != default){
                        tag.Id = found.Id;
                    }
                }


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

        public async Task ArchiveMonth(int reference)
        { 
            //Increment funds
            var relevantBudgets = await context.Budgets
            .Include(b => b.SavingsAccount)
            .Include(b => b.Entries)
            .Where(b => b.SavingsAccountId != null)
            .ToListAsync();

            foreach (var budget in relevantBudgets)
            {
                foreach (var e in budget.Entries)
                {
                    logger.LogInformation("PROCESSING ENTRY " + e.Name + ", " + e.Amount + ", " + e.Date);
                    logger.LogInformation(e.Date.Month.ToString() + ", " + reference + ", " + (e.Date.Month == reference).ToString());
                }

                var validEntries = budget.Entries.Where(e => e.Date.Month == reference);

                var total = validEntries.Count() > 0 ?  validEntries.Sum(e => e.Amount) : 0;
                logger.LogInformation(budget.Name + " : " + validEntries.Count() + " : " + total);

                if(total < budget.Amount){
                    budget.SavingsAccount.Amount += Math.Round(budget.Amount - total, 2);
                    await context.SaveChangesAsync();
                }               
            }
        }

        private async Task UpdateScore(){
            var today = DateTime.Now;
            var score = await context.ExpenseEntries
            .Where(e => e.Date.Year == today.Year 
            && e.Date.Month == today.Month 
            && e.Date.Day == today.Day).CountAsync();
            logger.LogCritical("UPDATING SCORE {s}", score);

            var remaining = (int) GetUserIncome(1).Result.Remaining / (score * 100);

            await scoreService.SetScore(Models.Enums.EScoreType.FINANCES, remaining + (score * 2));
            
        }
    }
}