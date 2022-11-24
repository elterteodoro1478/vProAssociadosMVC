using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vProAssociados.Models
{
    public class Associado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Placa { get; set; }
        public string TelefoneFixo { get; set; }
        public string TelefoneCeluar { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public bool UsuarioMaster { get; set; }
    }
}