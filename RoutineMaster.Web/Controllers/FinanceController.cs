using Microsoft.AspNetCore.Mvc;
using RoutineMaster.Models.Entities;
using RoutineMaster.Service;

namespace RoutineMaster.Web.Controllers
{
    public class FinanceController
    {
        private IFinanceService service;

        public FinanceController(IFinanceService service){
            this.service = service;
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

        public async Task<IActionResult> CreateBudget([FromBody] Budget budget){
            await service.CreateBudget(1, budget);
            return new OkResult();
        }

        [HttpPut("budgets/{id}")]

        public async Task<IActionResult> UpdateBudget([FromRoute] int id, [FromBody] Budget budget){
            await service.UpdateBudget(id, budget);
            return new OkResult();
        }

        [HttpPost("expenses")]

        public async Task<IActionResult> CreateExpense([FromBody] ExpenseEntry expenseEntry){
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

    }
}