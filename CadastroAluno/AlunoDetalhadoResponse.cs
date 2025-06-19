using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroAluno
{
    public class AlunoDetalhadoResponse
    {
        public Guid Matricula { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Cpf { get; set; }
    }
}
