using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIExemplo.Data;
using APIExemplo.Models;

namespace APIExemplo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentoesController : ControllerBase
    {
        private readonly ExemploContext _context;

        public MedicamentoesController(ExemploContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medicamento>>> GetMedicamentos()
        {
            return await _context.Medicamentos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Medicamento>> GetById(int id)
        {
            var medicamento = await _context.Medicamentos.FindAsync(id);

            if (medicamento == null)
            {
                return NotFound();
            }

            return medicamento;
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar(Medicamento medicamento)
        {
            var anoAtual = DateTime.UtcNow.Year;

            if (medicamento.AnoVencimento < (anoAtual + 1))
            {
                return BadRequest("Selecione no minimo o proximo ano");
            }

            _context.Entry(medicamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicamentoExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Medicamento>> Salvar(Medicamento medicamento)
        {
            var anoAtual = DateTime.UtcNow.Year;

            if(medicamento.AnoVencimento < (anoAtual + 1))
            {
                return BadRequest("Selecione no minimo o proximo ano");
            }

            _context.Medicamentos.Add(medicamento);
            await _context.SaveChangesAsync();

            return Ok(medicamento);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Medicamentos == null)
            {
                return NotFound();
            }
            var medicamento = await _context.Medicamentos.FindAsync(id);
            if (medicamento == null)
            {
                return NotFound();
            }

            _context.Medicamentos.Remove(medicamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicamentoExists(int id)
        {
            return (_context.Medicamentos?.Any(e => e.IdMedicamento == id)).GetValueOrDefault();
        }
    }
}
