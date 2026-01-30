namespace ControleNota.Models
{


    public class Aluno
    {
        public int AlunoId { get; set; }

        public string? Nome { get; set; }
        public int RM { get; set; }
        public string? Email { get; set; }


        public ICollection<Nota>? Notas { get; set; }
    }

}


