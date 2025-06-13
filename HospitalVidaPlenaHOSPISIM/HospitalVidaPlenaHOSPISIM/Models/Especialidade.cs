namespace HospitalVidaPlenaHOSPISIM.Models
{
    public class Especialidade
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public ICollection<ProfissionalSaude> Profissionais { get; set; }
    }
}