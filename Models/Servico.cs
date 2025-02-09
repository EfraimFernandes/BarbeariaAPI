using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Barbearia.API.Models.Servico
{
    public class Servico
    {
        [Key]
         public int ServicoId { get; set; }

         [Required(ErrorMessage = "Favor, colocar um nome para o serviço.")]
         [DisplayName("Nomde do Serviço")]
         public string? NomeServico { get; set; }

         [DisplayName("Descrição")]
         public string? Descricao { get; set; }

        [Range(10, 999, ErrorMessage = "A duração deve ficar entre 10 à 999")]
         public int DuracaoMin { get; set; }

         [Range(0.01, 999.99, ErrorMessage = "O preço deve estar entre 0.01 e 999.99")]
         public float Preco { get; set; }
    }
}