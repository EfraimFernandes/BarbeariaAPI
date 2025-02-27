using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Barbearia.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Barbearia.API.Data {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Agendamento> Agendamentos { get; set; }

        public DbSet<Servico> Servicos { get; set; }
    }
}