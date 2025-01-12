namespace DesafioSGP.Domain.Entities
{
    public class Projeto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty; // Inicializado com string vazia
        public string Descricao { get; set; } = string.Empty; // Inicializado com string vazia
        public string Status { get; set; } = string.Empty; // Inicializado com string vazia
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }


        public Projeto(int id, string nome, string descricao, DateTime dataInicio, DateTime dataFim, string status)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Status = status;
        }

        public Projeto()
        {
        }
    }
}
