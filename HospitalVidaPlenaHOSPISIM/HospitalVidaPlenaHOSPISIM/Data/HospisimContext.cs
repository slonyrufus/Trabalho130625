using Microsoft.EntityFrameworkCore;
using HospitalVidaPlenaHOSPISIM.Models;

namespace HospisimContext.Data
{
    public class HospisimContext : DbContext
    {
        public HospisimContext(DbContextOptions<HospisimContext> options) : base(options)
        {
        }
        public DbSet<AltaHospitalar> AltasHospitalares { get; set; }
        public DbSet<Atendimento> Atendimentos { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
        public DbSet<Exame> Exames { get; set; }
        public DbSet<Internacao> Internacoes { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Prescricao> Prescricoes { get; set; }
        public DbSet<ProfissionalSaude> ProfissionaisSaude { get; set; }
        public DbSet<Prontuario> Prontuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AltaHospitalar>().ToTable("AltaHospitalar");
            modelBuilder.Entity<Atendimento>().ToTable("Atendimento");
            modelBuilder.Entity<Especialidade>().ToTable("Especialidade");
            modelBuilder.Entity<Exame>().ToTable("Exame");

            modelBuilder.Entity<Internacao>().ToTable("Internacao")
                .HasOne(i => i.Paciente)
                .WithMany()
                .HasForeignKey(i => i.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Paciente>().ToTable("Paciente");

            modelBuilder.Entity<Prescricao>().ToTable("Prescricao")
                .HasOne(p => p.Profissional)
                .WithMany()
                .HasForeignKey(p => p.ProfissionalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProfissionalSaude>().ToTable("ProfissionaisSaude");
            modelBuilder.Entity<Prontuario>().ToTable("Prontuario");
        }
    }
}