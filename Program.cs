using System;

class Pessoa
{
    public string Nome { get; set; }
    public int Idade { get; set; }
    public string Genero { get; set; }
    public string Profissao { get; set; }

    public Pessoa(string nome, int idade, string genero, string profissao)
    {
        Nome = nome;
        Idade = idade;
        Genero = genero;
        Profissao = profissao;
    }
}

class Suite
{
    public int Numero { get; set; }
    public bool Disponivel { get; set; }
    public int Capacidade { get; set; }
    public decimal ValorDiaria { get; set; }
    public Reserva[] Reservas { get; set; }

    public Suite(int numero, int capacidade, decimal valorDiaria)
    {
        Numero = numero;
        Disponivel = true;
        Capacidade = capacidade;
        ValorDiaria = valorDiaria;
        Reservas = new Reserva[10]; // Define um array para armazenar as reservas (pode ajustar o tamanho conforme necessário)
    }
}

class Reserva
{
    public Pessoa Cliente { get; set; }
    public Suite SuiteReservada { get; set; }
    public DateTime DataInicioReserva { get; set; }
    public DateTime DataFimReserva { get; set; }

    public Reserva(Pessoa cliente, Suite suite, DateTime inicioReserva, DateTime fimReserva)
    {
        Cliente = cliente;
        SuiteReservada = suite;
        DataInicioReserva = inicioReserva;
        DataFimReserva = fimReserva;
        suite.Disponivel = false; // A suíte fica indisponível após a reserva
    }

    public decimal CalcularValorFinalDiaria()
    {
        decimal valorDiariaFinal = SuiteReservada.ValorDiaria;

        // Verificar se a reserva ultrapassa 10 dias e aplicar desconto de 10%
        TimeSpan duracaoReserva = DataFimReserva - DataInicioReserva;
        int diasReserva = duracaoReserva.Days;

        if (diasReserva > 10)
        {
            valorDiariaFinal *= 0.9m; // Aplicar desconto de 10%
        }

        return valorDiariaFinal;
    }

    public void ExibirDetalhes()
    {
        Console.WriteLine($"Reserva feita por {Cliente.Nome}, para a suíte número {SuiteReservada.Numero}");
        Console.WriteLine($"Data de início da reserva: {DataInicioReserva}");
        Console.WriteLine($"Data de fim da reserva: {DataFimReserva}");
        Console.WriteLine($"Capacidade da suíte: {SuiteReservada.Capacidade}");
        Console.WriteLine($"Valor da diária: {SuiteReservada.ValorDiaria:C}");
        Console.WriteLine($"Valor final da diária: {CalcularValorFinalDiaria():C}");
    }
}

class Program
{
    static Pessoa[] pessoas = new Pessoa[10]; // Define um array para armazenar as pessoas
    static Suite[] suites = new Suite[5]; // Define um array para armazenar as suítes
    static int totalPessoas = 0; // Controla o total de pessoas cadastradas
    static int totalSuites = 0; // Controla o total de suítes cadastradas

    static void Main()
    {
        bool sair = false;

        do
        {
            ExibirMenu();
            int opcao = LerOpcao();

            switch (opcao)
            {
                case 1:
                    Cadastrar();
                    break;

                case 2:
                    Consultar();
                    break;

                case 3:
                    Listar();
                    break;

                case 4:
                    sair = true;
                    break;

                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

        } while (!sair);
    }

    static void ExibirMenu()
    {
        Console.WriteLine("======= MENU =======");
        Console.WriteLine("1. Cadastro");
        Console.WriteLine("   1.1. Hospede");
        Console.WriteLine("   1.2. Suite");
        Console.WriteLine("   1.3. Reserva");
        Console.WriteLine("2. Consultar");
        Console.WriteLine("   2.1. Hospede (Apenas 1)");
        Console.WriteLine("   2.2. Suite (Apenas 1)");
        Console.WriteLine("   2.3. Reserva (Apenas 1)");
        Console.WriteLine("3. Listar");
        Console.WriteLine("   3.1. Hospedes");
        Console.WriteLine("    3.2. Suites");
        Console.WriteLine("    3.3. Reservas");
        Console.WriteLine("4. Opção SAIR");
        Console.WriteLine("====================");
    }

    static int LerOpcao()
    {
        Console.Write("Escolha uma opção: ");
        int opcao;
        while (!int.TryParse(Console.ReadLine(), out opcao))
        {
            Console.WriteLine("Opção inválida. Tente novamente.");
            Console.Write("Escolha uma opção: ");
        }
        return opcao;
    }

    static void Cadastrar()
    {
        Console.WriteLine("======= CADASTRO =======");
        Console.WriteLine("1. Hospede");
        Console.WriteLine("2. Suite");
        Console.WriteLine("3. Reserva");

        int opcaoCadastro = LerOpcao();

        switch (opcaoCadastro)
        {
            case 1:
                CadastrarHospede();
                break;

            case 2:
                CadastrarSuite();
                break;

            case 3:
                CadastrarReserva();
                break;

            default:
                Console.WriteLine("Opção inválida.");
                break;
        }
    }

    static void CadastrarHospede()
    {
        Console.WriteLine("======= CADASTRO DE HOSPEDE =======");
        if (totalPessoas < pessoas.Length)
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            Console.Write("Idade: ");
            int idade;
            while (!int.TryParse(Console.ReadLine(), out idade) || idade < 0)
            {
                Console.WriteLine("Idade inválida. Tente novamente.");
                Console.Write("Idade: ");
            }
            Console.Write("Gênero: ");
            string genero = Console.ReadLine();
            Console.Write("Profissão: ");
            string profissao = Console.ReadLine();

            pessoas[totalPessoas++] = new Pessoa(nome, idade, genero, profissao);
            Console.WriteLine("Hóspede cadastrado com sucesso!");
        }
        else
        {
            Console.WriteLine("Limite de hóspedes atingido.");
        }
    }

    static void CadastrarSuite()
    {
        Console.WriteLine("======= CADASTRO DE SUITE =======");
        if (totalSuites < suites.Length)
        {
            Console.Write("Número da suíte: ");
            int numero;
            while (!int.TryParse(Console.ReadLine(), out numero) || numero < 1)
            {
                Console.WriteLine("Número inválido. Tente novamente.");
                Console.Write("Número da suíte: ");
            }
            Console.Write("Capacidade: ");
            int capacidade;
            while (!int.TryParse(Console.ReadLine(), out capacidade) || capacidade < 1)
            {
                Console.WriteLine("Capacidade inválida. Tente novamente.");
                Console.Write("Capacidade: ");
            }
            Console.Write("Valor da diária: ");
            decimal valorDiaria;
            while (!decimal.TryParse(Console.ReadLine(), out valorDiaria) || valorDiaria < 0)
            {
                Console.WriteLine("Valor inválido. Tente novamente.");
                Console.Write("Valor da diária: ");
            }

            suites[totalSuites++] = new Suite(numero, capacidade, valorDiaria);
            Console.WriteLine("Suíte cadastrada com sucesso!");
        }
        else
        {
            Console.WriteLine("Limite de suítes atingido.");
        }
    }

    static void CadastrarReserva()
    {
        Console.WriteLine("======= CADASTRO DE RESERVA =======");

        Console.WriteLine("Escolha um hóspede para a reserva:");
        ListarHospedes();
        int indiceHospede = LerIndice("Número do hóspede: ", totalPessoas);
        Pessoa hospedeSelecionado = pessoas[indiceHospede];

        Console.WriteLine("Escolha uma suíte para a reserva:");
        ListarSuites();
        int indiceSuite = LerIndice("Número da suíte: ", totalSuites);
        Suite suiteSelecionada = suites[indiceSuite];

        Console.Write("Data de início da reserva (dd/mm/yyyy): ");
        DateTime dataInicioReserva;
        while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dataInicioReserva))
        {
            Console.WriteLine("Data inválida. Tente novamente.");
            Console.Write("Data de início da reserva (dd/mm/yyyy): ");
        }

        Console.Write("Data de fim da reserva (dd/mm/yyyy): ");
        DateTime dataFimReserva;
        while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dataFimReserva) || dataFimReserva <= dataInicioReserva)
        {
            Console.WriteLine("Data inválida. Tente novamente.");
            Console.Write("Data de fim da reserva (dd/mm/yyyy): ");
        }

        Reserva novaReserva = new Reserva(hospedeSelecionado, suiteSelecionada, dataInicioReserva, dataFimReserva);

        // Verifica se a suíte possui espaço para a reserva
        if (suiteSelecionada.Reservas.Length > 0)
        {
            int indiceDisponivel = Array.IndexOf(suiteSelecionada.Reservas, null);
            if (indiceDisponivel != -1)
            {
                suiteSelecionada.Reservas[indiceDisponivel] = novaReserva;
                Console.WriteLine("Reserva cadastrada com sucesso!");
            }
            else
            {
                Console.WriteLine("Limite de reservas para a suíte atingido.");
            }
        }
        else
        {
            Console.WriteLine("Suíte não configurada corretamente.");
        }
    }

    static void Consultar()
    {
        Console.WriteLine("======= CONSULTA =======");
        Console.WriteLine("1. Hospede (Apenas 1)");
        Console.WriteLine("2. Suite (Apenas 1)");
        Console.WriteLine("3. Reserva (Apenas 1)");

        int opcaoConsulta = LerOpcao();

        switch (opcaoConsulta)
        {
            case 1:
                ConsultarHospede();
                break;

            case 2:
                ConsultarSuite();
                break;

            case 3:
                ConsultarReserva();
                break;

            default:
                Console.WriteLine("Opção inválida.");
                break;
        }
    }

    static void ConsultarHospede()
    {
        Console.WriteLine("======= CONSULTA DE HOSPEDE =======");
        ListarHospedes();
        int indiceHospede = LerIndice("Número do hóspede: ", totalPessoas);
        Pessoa hospedeSelecionado = pessoas[indiceHospede];

        Console.WriteLine("Detalhes do hóspede:");
        Console.WriteLine($"Nome: {hospedeSelecionado.Nome}");
        Console.WriteLine($"Idade: {hospedeSelecionado.Idade}");
        Console.WriteLine($"Gênero: {hospedeSelecionado.Genero}");
        Console.WriteLine($"Profissão: {hospedeSelecionado.Profissao}");
    }

    static void ConsultarSuite()
    {
        Console.WriteLine("======= CONSULTA DE SUITE =======");
        ListarSuites();
        int indiceSuite = LerIndice("Número da suíte: ", totalSuites);
        Suite suiteSelecionada = suites[indiceSuite];

        Console.WriteLine("Detalhes da suíte:");
        Console.WriteLine($"Número: {suiteSelecionada.Numero}");
        Console.WriteLine($"Capacidade: {suiteSelecionada.Capacidade}");
        Console.WriteLine($"Valor da diária: {suiteSelecionada.ValorDiaria:C}");

        Console.WriteLine("Reservas para a suíte:");
        for (int i = 0; i < suiteSelecionada.Reservas.Length; i++)
        {
            if (suiteSelecionada.Reservas[i] != null)
            {
                Console.WriteLine($"Reserva {i + 1}: {suiteSelecionada.Reservas[i].Cliente.Nome}, {suiteSelecionada.Reservas[i].DataInicioReserva:dd/MM/yyyy} - {suiteSelecionada.Reservas[i].DataFimReserva:dd/MM/yyyy}");
            }
        }
    }

    static void ConsultarReserva()
    {
        Console.WriteLine("======= CONSULTA DE RESERVA =======");
        ListarReservas();
        int indiceReserva = LerIndice("Número da reserva: ", totalSuites);

        // Verificar se o índice está dentro do intervalo válido
        if (indiceReserva >= 0 && indiceReserva < totalSuites)
        {
            Suite suiteComReserva = suites[indiceReserva];

            // Verificar se a suíte possui reservas
            if (suiteComReserva.Reservas != null)
            {
                foreach (var reserva in suiteComReserva.Reservas)
                {
                    if (reserva != null)
                    {
                        Console.WriteLine("Detalhes da reserva:");
                        reserva.ExibirDetalhes();
                        return;  // Encerrar a função após encontrar a reserva
                    }
                }
            }
        }

        Console.WriteLine("Reserva não encontrada.");
    }

    static void Listar()
    {
        Console.WriteLine("======= LISTAGEM =======");
        Console.WriteLine("1. Hospedes");
        Console.WriteLine("2. Suites");
        Console.WriteLine("3. Reservas");

        int opcaoListagem = LerOpcao();

        switch (opcaoListagem)
        {
            case 1:
                ListarHospedes();
                break;

            case 2:
                ListarSuites();
                break;

            case 3:
                ListarReservas();
                break;

            default:
                Console.WriteLine("Opção inválida.");
                break;
        }
    }

    static void ListarHospedes()
    {
        Console.WriteLine("======= LISTAGEM DE HOSPEDES =======");
        for (int i = 0; i < totalPessoas; i++)
        {
            Console.WriteLine($"{i + 1}. {pessoas[i].Nome}");
        }
    }

    static void ListarSuites()
    {
        Console.WriteLine("======= LISTAGEM DE SUITES =======");
        for (int i = 0; i < totalSuites; i++)
        {
            Console.WriteLine($"{i + 1}. Suíte {suites[i].Numero}");
        }
    }

    static void ListarReservas()
    {
        Console.WriteLine("======= LISTAGEM DE RESERVAS =======");

        foreach (var suite in suites)
        {
            if (suite.Reservas != null)
            {
                foreach (var reserva in suite.Reservas)
                {
                    if (reserva != null)
                    {
                        Console.WriteLine($"Reserva para {reserva.Cliente.Nome} na Suíte {suite.Numero}");
                        Console.WriteLine($"Data de início: {reserva.DataInicioReserva:dd/MM/yyyy}");
                        Console.WriteLine($"Data de fim: {reserva.DataFimReserva:dd/MM/yyyy}");
                        Console.WriteLine($"Valor final da diária: {reserva.CalcularValorFinalDiaria():C}");
                        Console.WriteLine();
                    }
                    else
                    {
                        // Adicione uma mensagem informando que não há reservas para esta suíte
                        Console.WriteLine($"Não há reservas para a Suíte {suite.Numero}");
                    }
                }
            }
            else
            {
                // Adicione uma mensagem informando que não há reservas para esta suíte
                Console.WriteLine($"Não há reservas para a Suíte {suite.Numero}");
            }
        }
    }

    static int LerIndice(string mensagem, int maximo)
    {
        int indice;
        do
        {
            Console.Write(mensagem);
        } while (!int.TryParse(Console.ReadLine(), out indice) || indice < 1 || indice > maximo);
        return indice - 1;
    }
}
