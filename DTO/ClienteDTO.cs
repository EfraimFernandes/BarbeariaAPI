using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Barbearia.API.DTO {
    public class ClienteDTO {
        public int ClienteId { get; set; }

        [Required]
        public string? Nome { get; set; }

        [Required]
        public string? Telefone { get; set; }

        [EmailAddress(ErrorMessage = "Favor, digitar um e-mail válido.")]
        public string?  Email { get; set; }

        public DateTime DataNasc { get; set; }
    }
}