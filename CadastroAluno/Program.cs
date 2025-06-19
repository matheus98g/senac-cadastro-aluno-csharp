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
            Console.WriteLine("3 - Listar alunos (ordenar por Nome ou Idade)");
            Console.WriteLine("4 - Filtrar alunos por letra inicial");
            Console.WriteLine("5 - Buscar aluno por CPF");
            Console.WriteLine("6 - Sair");
            Console.Write("Opção: ");
            entrada = Console.ReadLine();

            if (entrada == "6" || entrada?.ToLower() == "sair")
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

                    if (alunos.Exists(a => a.Cpf == cpf))
                    {
                        Console.WriteLine("Já existe um aluno com esse CPF.");
                        break;
                    }

                    alunos.Add(new Aluno 
                    { 
                        Nome = nome, 
                        Idade = idade, 
                        Cpf = cpf ,
                        Matricula = Guid.NewGuid()
                    });
                    Console.WriteLine("Aluno adicionado com sucesso!");
                    break;

                case "2":
                    Console.Write("Digite o CPF do aluno a ser removido: ");
                    string cpfRemover = Console.ReadLine() ?? "";
                    var alunoRemover = alunos.FirstOrDefault(a => a.Cpf == cpfRemover);
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
                    Console.Write("Ordenar por nome(n) ou idade(i)? ");
                    string criterio = Console.ReadLine()?.ToLower() ?? "n";
                    IEnumerable<Aluno> ordenados = criterio == "i"
                        ? alunos.OrderBy(a => a.Idade)
                        : alunos.OrderBy(a => a.Nome);

                    var listaResponse = ordenados
                        .Select(a => new AlunoResponse 
                        { 
                            Nome = a.Nome,
                            Idade = a.Idade
                        });

                    Console.WriteLine("\nAlunos cadastrados:");
                    foreach (var a in listaResponse)
                        Console.WriteLine($"Nome: {a.Nome} | Idade: {a.Idade}");
                    break;

                case "4":
                    Console.Write("Digite a letra inicial: ");
                    string letra = Console.ReadLine()?.ToUpper() ?? "";
                    if (letra.Length != 1)
                    {
                        Console.WriteLine("Entrada inválida.");
                        break;
                    }

                    var filtrados = alunos
                        .Where(a => a.Nome.ToUpper().StartsWith(letra))
                        .Select(a => new AlunoResponse 
                        { 
                            Nome = a.Nome, 
                            Idade = a.Idade,
                        })
                        .ToList();

                    if (filtrados.Count == 0)
                    {
                        Console.WriteLine("Nenhum aluno encontrado com essa letra.");
                    }
                    else
                    {
                        Console.WriteLine("\nAlunos filtrados:");
                        foreach (var a in filtrados)
                            Console.WriteLine($"Nome: {a.Nome} | Idade: {a.Idade}");
                    }
                    break;

                case "5":
                    Console.Write("Digite o CPF: ");
                    string cpfBusca = Console.ReadLine() ?? "";

                    var alunoDetalhado = alunos
                        .Where(a => a.Cpf == cpfBusca)
                        .Select(a => new AlunoDetalhadoResponse
                        {
                            Nome = a.Nome,
                            Idade = a.Idade,
                            Cpf = a.Cpf,
                            Matricula = a.Matricula
                        })
                        .FirstOrDefault();

                    if (alunoDetalhado == null)
                    {
                        Console.WriteLine("Aluno não encontrado.");
                    }
                    else
                    {
                        Console.WriteLine("\nAluno encontrado:");
                        Console.WriteLine($"Nome: {alunoDetalhado.Nome}");
                        Console.WriteLine($"Idade: {alunoDetalhado.Idade}");
                        Console.WriteLine($"CPF: {alunoDetalhado.Cpf}");
                        Console.WriteLine($"Matrícula: {alunoDetalhado.Matricula}");
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
