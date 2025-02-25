using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Barbearia.API.Data;
using Barbearia.API.DTO;
using Barbearia.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace Barbearia.API.Controllers{
    [ApiController]
    [Route("Api/[controller]")]

    public class AgendamentoController : ControllerBase {
        private readonly ApplicationDbContext _dbcontext;

        public AgendamentoController(ApplicationDbContext dbContext){
            _dbcontext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult> Get() {
            var agendamentos = await _dbcontext
                                            .Agendamentos
                                            .Include(a => a.Cliente)
                                            .Include(b => b.Servicos)
                                            .ToListAsync();
            return Ok(agendamentos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Agendamento>> GetById(int id){
            var agendamentos = await _dbcontext.Agendamentos.FindAsync(id);
            if(agendamentos == null){
                return NotFound();
            }
            return Ok(agendamentos);
        }

         [HttpPost]
        public async Task<ActionResult> Create([FromBody] AgendamentoDTO agendamentoDTO) {
            if(ModelState.IsValid){
                Agendamento agendamento = new Agendamento();
                agendamento.ClienteId = agendamentoDTO.ClienteId;
                agendamento.Observacoes = agendamentoDTO.Observacoes;
                agendamento.DataHora = agendamentoDTO.DataHora;
                agendamento.Status = agendamentoDTO.Status;
                agendamento.Servicos = new List<Servico> ();
                agendamento.Cliente = _dbcontext.Clientes.Find(agendamento.ClienteId);

                foreach (var item in agendamentoDTO.Servicos){
                    var servico = _dbcontext.Servicos.Find(item.ServicoId);
                    if(servico == null){
                        return BadRequest();
                    }

                    agendamento.Servicos.Add(servico);
                }

                _dbcontext.Agendamentos.Add(agendamento);
                await _dbcontext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), new {id = agendamento.AgendamentoId}, agendamento);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> Edit(int id, AgendamentoDTO agendamentoDTO ) {
            if(id != agendamentoDTO.AgendamentoId) {
                return BadRequest();
            }

            if(ModelState.IsValid) {
                var agendamento = _dbcontext
                                        .Agendamentos
                                        .Include(x => x.Servicos)
                                        .Include(y => y.Cliente)
                                        .FirstOrDefault(z => z.AgendamentoId == agendamentoDTO.AgendamentoId);
                if(agendamento == null) {
                    return NotFound();
                }

                agendamento.ClienteId = agendamentoDTO.ClienteId;
                agendamento.Observacoes = agendamentoDTO.Observacoes;
                agendamento.DataHora = agendamentoDTO.DataHora;
                agendamento.Status = agendamentoDTO.Status;
                agendamento.Servicos = new List<Servico> ();
                agendamento.Cliente = _dbcontext.Clientes.Find(agendamento.ClienteId);

                foreach (var item in agendamentoDTO.Servicos){
                    var servico = _dbcontext.Servicos.Find(item.ServicoId);
                    if(servico == null){
                        return BadRequest();
                    }

                    agendamento.Servicos.Add(servico);
                }

                _dbcontext.Agendamentos.Update(agendamento);
                await _dbcontext.SaveChangesAsync();
                return Ok(agendamento);
            }
            return BadRequest();
        }

          [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) {
            var agendamento = await _dbcontext
                                        .Agendamentos
                                        .Include(x => x.Servicos)
                                        .Include(y => y.Cliente)
                                        .FirstOrDefaultAsync(z => z.AgendamentoId == id);
            if(agendamento == null) {
                return NotFound();
            }
            _dbcontext.Agendamentos.Remove(agendamento);
            await _dbcontext.SaveChangesAsync();
            return Ok(agendamento);
        }

    }

}