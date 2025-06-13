# HOSPISIM – Sistema de Gestão Hospitalar
Projeto feito com ASP.NET MVC para gerenciar os dados de pacientes, atendimentos, profissionais de saúde e demais entidades relacionadas no ambiente hospitalar, nomeado de HOSPISIM.

## Execução

### Pré-requisitos

- [.NET Framework 4.7.2+](https://dotnet.microsoft.com/)
- [Microsoft SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
- Visual Studio 2019+ com suporte a ASP.NET MVC
- Entity Framework 6+

### Passos

1. **Clonar**
   ```bash
   git clone https://github.com/slonyrufus/Trabalho130625/HOSPISIM.git
   cd HOSPISIM

# Relacionamentos
Paciente possui um ou mais prontuários
- Paciente 1:N Prontuário

Prontuário contém um ou mais atendimentos
- Prontuário 1:N Atendimento

Profissional de saúde realiza um ou mais atendimentos
- Profissional 1:N Atendimento

Cada Atendimento pode conter:

Múltiplas prescrições
- Atendimento 1:N Prescrição

Múltiplos exames
- Atendimento 1:N Exame

Zero ou uma internação
- Atendimento 0..1:1 Internação

Cada internação pode levar a uma única alta hospitalar opcional
- Internação 0..1:1 Alta Hospitalar

Cada profissional de saúde pertence a uma única especialidade
- Profissional N:1 Especialidade