namespace HospitalVidaPlenaHOSPISIM.Models
{
    public class Exame
    {
        public Guid Id { get; set; }
        public string Tipo { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public DateTime? DataRealizacao { get; set; }
        public string Resultado { get; set; }
        public Guid AtendimentoId { get; set; }
        public Atendimento Atendimento { get; set; }
    }
}