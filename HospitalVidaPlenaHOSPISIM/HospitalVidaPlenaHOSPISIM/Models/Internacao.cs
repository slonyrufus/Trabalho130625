namespace HospitalVidaPlenaHOSPISIM.Models
{
    public class Internacao
    {
        public Guid Id { get; set; }
        public Guid PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public Guid AtendimentoId { get; set; }
        public Atendimento Atendimento { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime? PrevisaoAlta { get; set; }
        public string MotivoInternacao { get; set; }
        public string Leito { get; set; }
        public string Quarto { get; set; }
        public string Setor { get; set; }
        public string? PlanoSaudeUtilizado { get; set; }
        public string ObservacoesClinicas { get; set; }
        public string StatusInternacao { get; set; }
        public AltaHospitalar AltaHospitalar { get; set; }
    }
}