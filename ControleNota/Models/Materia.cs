namespace ControleNota.Models
{

    public class Materia
    {
        public int MateriaId { get; set; }
        public string? Nome { get; set; }
        public int CargaHoraria { get; set; }

        public int ProfessorId { get; set; }
        public Professor? Professor { get; set; }



        public ICollection<Nota>? Notas { get; set; }
    }
}


