# HOSPISIM – Sistema de Gestão Hospitalar
Projeto feito com ASP.NET MVC para gerenciar os dados de pacientes, atendimentos, profissionais de saúde e demais entidades relacionadas no ambiente hospitalar, nomeado de HOSPISIM.

## Instruções

### Pré-requisitos

- [.NET Framework 4.7.2+](https://dotnet.microsoft.com/)
- [Microsoft SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
- Visual Studio 2019+ com suporte a ASP.NET MVC
- Entity Framework 6+

### Passos

1. **Clonar o repositório**
   ```bash
   git clone https://github.com/seu-usuario/HOSPISIM.git
   cd HOSPISIM
   ```
2. **Abrir o projeto no Visual Studio**

3. **Restaurar os pacotes NuGet**
Clique com o botão direito na solução > Restaurar Pacotes NuGet

4. Atualizar o banco de dados com EF Migrations

5. Abrir o Package Manager Console e executar:

```bash
Copy
Edit
Update-Database
Executar o projeto
```

Para executar, basta apenas apertar F5 ou em iniciar depuração.

# Relacionamentos
```
Paciente possui um ou mais prontuários
- Paciente 1:N Prontuário

Prontuário contém um ou mais atendimentos
- Prontuário 1:N Atendimento

Profissional de saúde realiza um ou mais atendimentos
- Profissional 1:N Atendimento | Atendimento.cs

Múltiplas prescrições
- Atendimento 1:N Prescrição | Atendimento.cs

Múltiplos exames
- Atendimento 1:N Exame | Exame.cs

Zero ou uma internação
- Atendimento 0..1:1 Internação | Internacao.cs

Cada internação pode levar a uma única alta hospitalar opcional
- Internação 0..1:1 Alta Hospitalar | Internacao.cs

Cada profissional de saúde pertence a uma única especialidade
- Profissional N:1 Especialidade
```

![image](https://raw.githubusercontent.com/slonyrufus/Trabalho130625/refs/heads/main/relation.png)

# Modelo
Movel View Control
MVC
