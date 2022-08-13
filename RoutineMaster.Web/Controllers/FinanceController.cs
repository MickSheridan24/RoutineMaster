using Microsoft.AspNetCore.Mvc;
using RoutineMaster.Models.Dtos;
using RoutineMaster.Models.Entities;
using RoutineMaster.Service;

namespace RoutineMaster.Web.Controllers
{
    public class FinanceController
    {
        private IFinanceService service;
        private ILogger<FinanceService> logger;
        
        private IScoreService scoreService;

        public FinanceController(IFinanceService service, IScoreService scoreService, ILogger<FinanceService> logger){
            this.service = service;
            this.logger = logger;
            this.scoreService = scoreService;
        }

        [HttpGet("userIncome")]

        public async Task<IActionResult> GetUserIncome(){
            return new JsonResult(await service.GetUserIncome(1));
        }

        [HttpGet("budgets")]

        public async Task<IActionResult> GetBudgets(){
            return new JsonResult(await service.GetBudgets(1));
        }

        [HttpGet("expenses")]

        public async Task<IActionResult> GetExpenses(){
            return new JsonResult(await service.GetExpenses(1));
        }
        
        [HttpGet("funds")]

        public async Task<IActionResult> GetFunds(){
            return new JsonResult(await service.GetFunds(1));
        }

        [HttpPut("userIncome")]

        public async Task<IActionResult> UpdateUserIncome([FromBody] UserIncome userIncome){
            await service.UpdateUserIncome(1, userIncome);
            return new OkResult();
        }

        [HttpPost("budgets")]

        public async Task<IActionResult> CreateBudget([FromBody] CreateBudgetDto budget){
            await service.CreateBudget(1, budget);
            return new OkResult();
        }


        [HttpPut("expenses/{id}")]

        public async Task<IActionResult> UpdateExpense([FromRoute] int id, [FromBody] UpdateExpenseDto expenseDto){
            await service.UpdateExpense(1, id, expenseDto);
            return new OkResult();
        }

        [HttpPut("budgets/{id}")]

        public async Task<IActionResult> UpdateBudget([FromRoute] int id, [FromBody] UpdateBudgetDto budget){
            await service.UpdateBudget(id, budget);
            return new OkResult();
        }

        [HttpPost("expenses")]

        public async Task<IActionResult> CreateExpense([FromBody] CreateExpenseEntryDto expenseEntry){
            await service.CreateExpense(1, expenseEntry);
            return new OkResult();
        }

        [HttpPost("funds")]

        public async Task<IActionResult> CreateFund([FromBody] SavingsAccount fund){
            await service.CreateFund(1, fund);
            return new OkResult();
        }

        [HttpDelete("expenses/{id}")]

        public async Task<IActionResult> DeleteExpense([FromRoute] int id){
            await service.DeleteExpense(1, id);
            return new OkResult();
        }

        [HttpDelete("budgets/{id}")]
        public async Task<IActionResult> DeleteBudget([FromRoute] int id){
            logger.LogInformation("DELETIGN BUDGET {id}", id);
            await service.DeleteBudget(1, id);
            return new OkResult();
        }
        
        
        [HttpDelete("funds/{id}")]
        public async Task<IActionResult> DeleteFund([FromRoute] int id){
            await service.DeleteFund(1, id);
            return new OkResult();
        }


        [HttpPost("archiveMonth")]
        public async Task<IActionResult> ArchiveMonth([FromBody] int referenceMonth){
            await service.ArchiveMonth(referenceMonth);
            return new OkResult();
        }

        [HttpGet("financeSummary")]
        public async Task<IActionResult> FinanceSummary(){
            return new JsonResult(await service.GetFinanceSummary());
        }

        [HttpGet("financeScore")]
        public async Task<IActionResult> GetScore(){
            return new JsonResult(scoreService.GetScore(Models.Enums.EScoreType.FINANCES));
        }
    }
}