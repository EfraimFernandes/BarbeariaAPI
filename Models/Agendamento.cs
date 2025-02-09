using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Barbearia.API.Models.Agendamento {
    public class Agendamento {
        [Key]
        public int AgendamentoId { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [Required]
        public DateTime DataHora { get; set; }

        public string? Observacoes { get; set; }

        [Required]
        public int Status { get; set; }
    }
}