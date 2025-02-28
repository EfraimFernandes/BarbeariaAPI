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
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase {

        public readonly ApplicationDbContext _dbContext;

        public ClienteController(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult> Get() {
            var clientes = await _dbContext.Clientes.ToListAsync();
            return Ok(clientes);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Cliente>> GetById(int id){
            var cliente = await _dbContext.Clientes.FindAsync(id);
            if(cliente == null){
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ClienteDTO clienteDTO) {
            if(ModelState.IsValid){
                Cliente cliente = new Cliente();
                cliente.Nome = clienteDTO.Nome;
                cliente.Telefone = clienteDTO.Telefone;
                cliente.Email = clienteDTO.Email;
                cliente.DataNasc = clienteDTO.DataNasc;

                _dbContext.Clientes.Add(cliente);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction("1", cliente);
            }
            return BadRequest();
        }

        [HttpPut("{ind:int}")]
        public async Task<ActionResult> Edit(int id, ClienteDTO clienteDTO) {
            if(id != clienteDTO.ClienteId) {
                return BadRequest();
            }

            if(ModelState.IsValid) {
                var cliente = _dbContext.Clientes.Find(clienteDTO.ClienteId);
                if(cliente == null) {
                    return NotFound();
                }

                cliente.Nome = clienteDTO.Nome;
                cliente.Telefone = clienteDTO.Telefone;
                cliente.Email = clienteDTO.Email;
                cliente.DataNasc = clienteDTO.DataNasc;

                _dbContext.Update(cliente);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) {
            var cliente = await _dbContext.Clientes.FindAsync(id);
            if(cliente == null) {
                return NotFound();
            }
            _dbContext.Clientes.Remove(cliente);
            await _dbContext.SaveChangesAsync();
            return Ok(cliente);
        }

    }
}