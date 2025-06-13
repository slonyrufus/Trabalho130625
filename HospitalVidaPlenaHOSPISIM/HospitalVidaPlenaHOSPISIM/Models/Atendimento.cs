
namespace HospitalVidaPlenaHOSPISIM.Models
{
    public enum Status
    {
        REALIZADO, EM_ANDAMENTO, CANCELADO
    }
    public class Atendimento
    {
        public Guid Id { get; set; }
        public DateTime DataHora { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }
        public string Local { get; set; }
        public Guid ProntuarioId { get; set; }
        public Prontuario Prontuario { get; set; }
        public Guid ProfissionalId { get; set; }
        public ProfissionalSaude Profissional { get; set; }
        public ICollection<Prescricao> Prescricoes { get; set; }
        public ICollection<Exame> Exames { get; set; }
        public Internacao Internacao { get; set; }
    }
}