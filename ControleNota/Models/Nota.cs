namespace ControleNota.Models
{

    public class Nota
    {
        public int NotaId { get; set; }
        public double Valor { get; set; }
        public DateTime DataLancamento { get; set; }

        public int AlunoId { get; set; }
        public Aluno? Aluno { get; set; }

        public int MateriaId { get; set; }
        public Materia? Materia { get; set; }
    }
}



