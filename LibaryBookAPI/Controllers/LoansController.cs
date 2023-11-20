using LibaryAux.Repository;
using LibaryDomain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibaryBookAPI.Controllers
{
    public class LoansController : LibaryController<Loan>
    {
        private LoanRepository _loanRepository;
        public LoansController(LoanRepository loanRepository) : base(loanRepository)
        {
           _loanRepository = loanRepository;
        }

        [HttpGet("ActiveLoans")]
        public async Task<IActionResult> ActiveLoansByUserId(int userId)
        {
            try
            {
                var result = await _loanRepository.GetActiveLoansByUserId(userId);
                return Ok(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex}");
            }
        }

        [HttpGet("CheckExistence")]
        public async override Task<IActionResult> CheckExistence(string param)
        {
            try
            {
                return Ok("Not Implemented Yet");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex}");
            }
        }
    }
}
