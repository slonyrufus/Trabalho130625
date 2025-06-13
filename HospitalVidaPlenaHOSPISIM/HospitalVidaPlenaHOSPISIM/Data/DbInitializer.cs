using System;
using System.Linq;
using HospitalVidaPlenaHOSPISIM.Models;
using HospisimContext.Data;

namespace HospisimContext.Data
{
    public static class DbInitializer
    {
        public static void Initialize(HospisimContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Console.WriteLine("Seed iniciado");
            for (int i = 1; i <= 10; i++)
            {
                context.Pacientes.Add(new Paciente
                {
                    Id = Guid.NewGuid(),
                    NomeCompleto = $"Paciente {i}",
                    CPF = $"0000000000{i:D2}",
                    DataNascimento = DateTime.Today.AddYears(-20).AddDays(i),
                    Sexo = i % 2 == 0 ? "Masculino" : "Feminino",
                    TipoSanguineo = "O+",
                    Telefone = $"(11) 99999-000{i}",
                    Email = $"paciente{i}@teste.com",
                    EnderecoCompleto = $"Rua Exemplo, {i}",
                    NumeroCartaoSUS = $"1234567890{i}",
                    EstadoCivil = i % 2 == 0 ? "Solteiro" : "Casado",
                    PossuiPlanoSaude = i % 3 == 0
                });
            }
            context.SaveChanges();

            for (int i = 1; i <= 10; i++)
            {
                context.Especialidades.Add(new Especialidade
                {
                    Id = Guid.NewGuid(),
                    Nome = $"Especialidade {i}"
                });
            }
            context.SaveChanges();

            var especialidades = context.Especialidades.ToList();
            var pacientes = context.Pacientes.ToList();

            for (int i = 1; i <= 10; i++)
            {
                var esp = especialidades[i % especialidades.Count];
                context.ProfissionaisSaude.Add(new ProfissionalSaude
                {
                    Id = Guid.NewGuid(),
                    NomeCompleto = $"Profissional {i}",
                    CPF = $"1111111111{i:D2}",
                    Email = $"prof{i}@teste.com",
                    Telefone = $"(11) 98888-000{i}",
                    RegistroConselho = $"CRM{i:0000}",
                    TipoRegistro = "CRM",
                    EspecialidadeId = esp.Id,
                    DataAdmissao = DateTime.Today.AddYears(-i),
                    CargaHorariaSemanal = 40,
                    Turno = i % 2 == 0 ? "Manhã" : "Tarde",
                    Ativo = true
                });
            }
            context.SaveChanges();

            var profissionais = context.ProfissionaisSaude.ToList();
            var prontuarios = new Prontuario[10];

            for (int i = 1; i <= 10; i++)
            {
                var pat = pacientes[i - 1];
                var pront = new Prontuario
                {
                    Id = Guid.NewGuid(),
                    Numero = $"PR{i:0000}",
                    DataAbertura = DateTime.Today.AddDays(-i),
                    ObservacoesGerais = "Observação inicial",
                    PacienteId = pat.Id
                };
                context.Prontuarios.Add(pront);
                prontuarios[i - 1] = pront;
            }
            context.SaveChanges();

            for (int i = 1; i <= 10; i++)
            {
                var pront = prontuarios[i - 1];
                var prof = profissionais[i % profissionais.Count];
                context.Atendimentos.Add(new Atendimento
                {
                    Id = Guid.NewGuid(),
                    DataHora = DateTime.Now.AddHours(-i),
                    Tipo = i % 2 == 0 ? "Consulta" : "Emergência",
                    Status = "Realizado",
                    Local = $"Sala {i}",
                    ProntuarioId = pront.Id,
                    ProfissionalId = prof.Id
                });
            }
            context.SaveChanges();

            var atendimentos = context.Atendimentos.ToList();

            foreach (var at in atendimentos)
            {
                for (int i = 1; i <= 1; i++)
                {
                    context.Prescricoes.Add(new Prescricao
                    {
                        Id = Guid.NewGuid(),
                        AtendimentoId = at.Id,
                        ProfissionalId = profissionais.First().Id,
                        Medicamento = "MedTeste",
                        Dosagem = "50mg",
                        Frequencia = "8/8h",
                        ViaAdministracao = "Oral",
                        DataInicio = DateTime.Today,
                        DataFim = DateTime.Today.AddDays(5),
                        Observacoes = "Sem observações",
                        StatusPrescricao = "Ativa"
                    });
                }
            }
            context.SaveChanges();

            foreach (var at in atendimentos)
            {
                context.Exames.Add(new Exame
                {
                    Id = Guid.NewGuid(),
                    Tipo = "Sangue",
                    DataSolicitacao = DateTime.Today,
                    DataRealizacao = DateTime.Today,
                    Resultado = "Normal",
                    AtendimentoId = at.Id
                });
            }
            context.SaveChanges();

            for (int i = 0; i < 5; i++)
            {
                var at = atendimentos[i];
                var intern = new Internacao
                {
                    Id = Guid.NewGuid(),
                    PacienteId = pacientes[i].Id,
                    AtendimentoId = at.Id,
                    DataEntrada = DateTime.Today.AddDays(-i),
                    PrevisaoAlta = DateTime.Today.AddDays(i + 1),
                    MotivoInternacao = "Motivo Teste",
                    Leito = $"L{i + 1}",
                    Quarto = $"Q{i + 1}",
                    Setor = "Clínica Geral",
                    PlanoSaudeUtilizado = "PlanoTest",
                    ObservacoesClinicas = "Observação",
                    StatusInternacao = "Ativa"
                };
                context.Internacoes.Add(intern);
                context.SaveChanges();

                var alta = new AltaHospitalar
                {
                    Id = Guid.NewGuid(),
                    InternacaoId = intern.Id,
                    Data = DateTime.Today,
                    CondicaoPaciente = "Ótima",
                    InstrucoesPosAlta = "Retorno em 7 dias"
                };
                context.AltasHospitalares.Add(alta);
            }
            context.SaveChanges();
        }
    }
}
