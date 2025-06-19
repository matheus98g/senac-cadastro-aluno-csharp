namespace CadastroAluno
{
    public class Aluno
    {
        public Guid Matricula { get; set; } = Guid.NewGuid();
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Cpf { get; set; }
    }
}
