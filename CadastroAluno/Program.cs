
using CadastroAluno;

class Program
{
    static void Main()
    {
        List<Aluno> alunos = [];
        string? entrada;

        Console.WriteLine(@"

            ███████╗███████╗███╗   ██╗ █████╗  ██████╗
            ██╔════╝██╔════╝████╗  ██║██╔══██╗██╔════╝
            ███████╗█████╗  ██╔██╗ ██║███████║██║     
            ╚════██║██╔══╝  ██║╚██╗██║██╔══██║██║     
            ███████║███████╗██║ ╚████║██║  ██║╚██████╗
            ╚══════╝╚══════╝╚═╝  ╚═══╝╚═╝  ╚═╝ ╚═════╝        
               C A D A S T R O   D E   A L U N O S
            ");
        while (true)
        {

            Console.WriteLine("\nEscolha uma opção:");
            Console.WriteLine("1 - Adicionar aluno");
            Console.WriteLine("2 - Remover aluno (por CPF)");
            Console.WriteLine("3 - Listar alunos");
            Console.WriteLine("4 - Sair");
            Console.Write("Opção: ");
            entrada = Console.ReadLine();

            if (entrada == "4" || entrada?.ToLower() == "sair")
                break;

            switch (entrada)
            {
                case "1":
                    Console.Write("Nome: ");
                    string nome = Console.ReadLine() ?? "";
                    if (string.IsNullOrWhiteSpace(nome))
                    {
                        Console.WriteLine("Nome inválido.");
                        break;
                    }

                    Console.Write("Idade: ");
                    if (!int.TryParse(Console.ReadLine(), out int idade) || idade < 0)
                    {
                        Console.WriteLine("Idade inválida.");
                        break;
                    }

                    Console.Write("CPF (somente números): ");
                    string cpf = Console.ReadLine() ?? "";
                    if (cpf.Length != 11 || !long.TryParse(cpf, out _))
                    {
                        Console.WriteLine("CPF inválido.");
                        break;
                    }

                    // Verifica se o CPF já existe
                    if (alunos.Exists(a => a.Cpf == cpf))
                    {
                        Console.WriteLine("Já existe um aluno com esse CPF.");
                        break;
                    }

                    alunos.Add(new Aluno { Nome = nome, Idade = idade, Cpf = cpf });
                    Console.WriteLine("Aluno adicionado com sucesso!");
                    break;

                case "2":
                    Console.Write("Digite o CPF do aluno a ser removido: ");
                    string cpfRemover = Console.ReadLine() ?? "";
                    Aluno? alunoRemover = alunos.Find(a => a.Cpf == cpfRemover);
                    if (alunoRemover != null)
                    {
                        alunos.Remove(alunoRemover);
                        Console.WriteLine("Aluno removido.");
                    }
                    else
                    {
                        Console.WriteLine("Aluno com esse CPF não encontrado.");
                    }
                    break;

                case "3":
                    Console.WriteLine("\nLista de alunos:");
                    foreach (var aluno in alunos)
                    {
                        Console.WriteLine($"Nome: {aluno.Nome} | Idade: {aluno.Idade}");
                    }
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }

        Console.WriteLine("\nEncerrando programa...");
    }
}
