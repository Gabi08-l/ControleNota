namespace ControleNota.Models
{
    public class Professor
    {
        public int ProfessorId { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public ICollection<Materia>? Materias { get; set; }
    }
}
