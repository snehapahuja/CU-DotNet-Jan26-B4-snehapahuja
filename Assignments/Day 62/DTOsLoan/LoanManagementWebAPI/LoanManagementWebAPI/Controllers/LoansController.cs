using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoanManagementWebAPI.DTOs;
using LoanManagementWebAPI.Data;
using LoanManagementWebAPI.Models;

namespace LoanManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase 
    {
        private readonly LoanManagementWebAPIContext _context;
        private readonly IMapper _mapper;

        public LoansController(LoanManagementWebAPIContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Loans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetLoanDto>>> GetLoan()
        {
            var loans = await _context.Loan.ToListAsync();
            var loanDtos = _mapper.Map<List<GetLoanDto>>(loans);
            return loanDtos;
            //return await _context.Loan.ToListAsync();
        }

        // GET: api/Loans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetLoanDto>> GetLoan(int id)
        {
            var loan = await _context.Loan.FindAsync(id);
            var loans = _mapper.Map<GetLoanDto>(loan);
            if (loan == null)
            {
                return NotFound();
            }

            return loans;
        }

        // PUT: api/Loans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoan(int id, UpdateLoanDto loanDto)
        {
            if (id != loanDto.LoanId)
            {
                return BadRequest();
            }

            var existingLoan = await _context.Loan.FindAsync(id);
            //_context.Entry(loanDto).State = EntityState.Modified;

            if (existingLoan == null) return NotFound();
            _mapper.Map(loanDto, existingLoan);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Loans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GetLoanDto>> PostLoan(CreateLoanDto loanDto)
        {
            var loan = _mapper.Map<Loan>(loanDto);
            _context.Loan.Add(loan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoan", new { id = loan.LoanId }, loan);
        }

        // DELETE: api/Loans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            var loan = await _context.Loan.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }

            _context.Loan.Remove(loan);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool LoanExists(int id)
        {
            return _context.Loan.Any(e => e.LoanId == id);
        }
    }
}
