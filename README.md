# Sistema de Gerenciamento de Academia

Sistema desktop desenvolvido em **C# com Windows Forms** para gerenciamento de usuários, administradores, treinos e exercícios de uma academia.

O projeto foi criado com finalidade acadêmica, tendo como principal objetivo aplicar, na prática, conceitos de **arquitetura em camadas**, desenvolvimento de interfaces desktop, integração com banco de dados e organização de regras de negócio.

---

## Objetivos de aprendizado

O projeto foi desenvolvido para consolidar conhecimentos importantes de programação e engenharia de software:

* Desenvolvimento de aplicações desktop com Windows Forms;
* Programação orientada a objetos em C#;
* Arquitetura em camadas;
* Separação entre interface, regras de negócio e acesso a dados;
* Integração com banco de dados SQL Server;
* Implementação de operações CRUD;
* Validação de dados;
* Autenticação e controle de acesso;
* Relacionamentos entre entidades;
* Tratamento de erros e exceções;
* Organização e manutenção de código.

---

## Funcionalidades

### Autenticação e controle de acesso

* Login de usuários;
* Diferenciação entre administradores e clientes;
* Redirecionamento para interfaces específicas conforme o tipo de usuário;
* Validação das credenciais;
* Proteção das senhas por meio de hash.

### Gerenciamento de usuários

* Cadastro de novos usuários;
* Consulta dos usuários cadastrados;
* Busca incremental de usuários;
* Edição de informações pessoais;
* Exclusão de usuários;
* Cadastro e atualização de endereço;
* Validação dos campos antes do envio ao banco de dados.

### Perfil do cliente

* Visualização das informações pessoais;
* Edição dos dados do perfil;
* Atualização de informações de endereço;
* Consulta dos treinos associados ao cliente.

### Gerenciamento de treinos

* Criação de treinos;
* Edição de treinos existentes;
* Exclusão de treinos;
* Associação de treinos aos usuários;
* Definição da data de início;
* Consulta e filtragem dos treinos cadastrados.

### Gerenciamento de exercícios

* Cadastro de exercícios em um treino;
* Associação de múltiplos exercícios ao mesmo treino;
* Organização por grupo muscular;
* Definição de séries, repetições e outras informações do exercício;
* Consulta dos exercícios vinculados a cada treino.

### Administração

* Interface exclusiva para administradores;
* Gerenciamento dos clientes;
* Gerenciamento dos treinos;
* Pesquisa e filtragem de registros;
* Acesso centralizado às principais operações do sistema.

---

## Arquitetura do projeto

O sistema foi estruturado com separação de responsabilidades entre diferentes camadas.

### Interface de usuário — IHM

Responsável pelas telas do Windows Forms e pela interação direta com o usuário.

Algumas das principais interfaces são:

* `IHMLogin.cs`: tela de autenticação;
* `IHMcadastro.cs`: cadastro de usuários;
* `IHMCliente.cs`: área principal do cliente;
* `IHMPerfil.cs`: visualização do perfil;
* `IHMEditarPerfil.cs`: edição dos dados pessoais;
* `IHMAdm1.cs`: gerenciamento administrativo;
* `IHMAdmMeio.cs`: navegação entre áreas administrativas;
* `IHMAdm2.cs`: gerenciamento de treinos e exercícios.

A camada de interface coleta os dados fornecidos pelo usuário e encaminha as operações para a camada de negócio.

### Camada de negócio — BLL

O arquivo `AcademiaBLL.cs` concentra as principais regras de negócio e validações da aplicação.

Entre suas responsabilidades estão:

* Validar dados obrigatórios;
* Verificar formatos e limites dos campos;
* Controlar as operações permitidas;
* Preparar os dados antes de enviá-los para persistência;
* Impedir que informações inválidas cheguem ao banco;
* Encaminhar as operações para a camada de acesso a dados.

Essa camada funciona como intermediária entre a interface e o banco de dados.

### Camada de acesso a dados — DAL

O arquivo `AcademiaDAL.cs` é responsável pela comunicação com o banco de dados.

Entre suas responsabilidades estão:

* Abrir e gerenciar conexões;
* Executar comandos SQL;
* Inserir registros;
* Consultar informações;
* Atualizar dados;
* Excluir registros;
* Transformar resultados do banco em objetos utilizados pela aplicação.

Essa separação evita que as telas executem comandos SQL diretamente e torna o código mais organizado.

### Classes de domínio

O projeto também possui classes utilizadas para representar as principais informações do sistema:

* `Login_Cadastro.cs`: dados de autenticação e cadastro dos usuários;
* `Endereco.cs`: informações de endereço;
* `Treino.cs`: dados dos treinos;
* `TreinoExercico.cs`: associação entre treinos e exercícios;
* `Erro.cs`: apoio ao tratamento e apresentação de erros.

---

## Fluxo da aplicação

O funcionamento geral do sistema segue o seguinte fluxo:

```text
Usuário
   ↓
Interface Windows Forms
   ↓
Camada de negócio — BLL
   ↓
Camada de acesso a dados — DAL
   ↓
Banco de dados SQL Server
```

Por exemplo, durante o cadastro de um usuário:

1. O usuário preenche os campos na interface;
2. A tela envia os dados para a camada BLL;
3. A BLL valida os campos e aplica as regras de negócio;
4. Se os dados forem válidos, a DAL executa o comando de inserção;
5. O banco de dados salva o novo registro;
6. A interface informa o resultado da operação.

---

## Tecnologias utilizadas

* **C#**
* **Windows Forms**
* **.NET Framework 4.7.2**
* **SQL Server**
* **ADO.NET**
* **Guna UI2**
* **BCrypt.Net**
* **Visual Studio**
* **Git e GitHub**

---

## Banco de dados

O sistema utiliza um banco de dados relacional para armazenar os dados da aplicação.

Entre as principais entidades estão:

* Usuário;
* Endereço;
* Treino;
* Exercício;
* Grupo muscular;
* Associação entre treino e exercício.

Os relacionamentos permitem que:

* Um usuário possua informações de endereço;
* Um usuário tenha um ou mais treinos;
* Um treino possua múltiplos exercícios;
* Cada exercício pertença a um grupo muscular.

O projeto inclui os arquivos necessários para configuração e utilização do banco de dados durante o desenvolvimento.

---

## Desafios de desenvolvimento

### 1. Separação de responsabilidades

Um dos principais desafios foi evitar que toda a lógica do sistema permanecesse dentro dos formulários.

Para isso, o projeto foi dividido em:

* Interface;
* Regras de negócio;
* Acesso a dados;
* Classes de domínio.

Essa organização melhora a legibilidade e facilita futuras alterações.

### 2. Relacionamento entre treinos e exercícios

Um treino pode possuir vários exercícios, o que exige o controle de relacionamentos entre diferentes registros do banco de dados.

Foi necessário implementar operações para:

* Criar o treino;
* Associar exercícios ao treino;
* Consultar os exercícios associados;
* Atualizar as associações;
* Remover registros sem comprometer a integridade dos dados.

### 3. Validação dos dados

O sistema realiza validações antes de executar operações no banco de dados.

Entre os exemplos estão:

* Verificação de campos obrigatórios;
* Remoção de espaços indevidos;
* Validação dos dados do usuário;
* Prevenção de cadastros inconsistentes;
* Exibição de mensagens de erro compreensíveis.

### 4. Controle de acesso

A aplicação possui interfaces diferentes para administradores e clientes.

Após a autenticação, o sistema identifica o perfil do usuário e libera apenas as funcionalidades correspondentes ao seu nível de acesso.

### 5. Persistência dos dados

A integração com SQL Server exigiu o gerenciamento de:

* Conexões;
* Comandos SQL;
* Parâmetros;
* Consultas;
* Inserções;
* Atualizações;
* Exclusões;
* Tratamento de erros de banco de dados.

---

## Destaques técnicos

### Arquitetura em camadas

A separação entre IHM, BLL e DAL reduz o acoplamento entre as partes do sistema.

```text
IHM → interação com o usuário
BLL → regras e validações
DAL → comunicação com o banco
```

### Senhas protegidas

As senhas não precisam ser armazenadas diretamente em texto puro. O projeto utiliza BCrypt para gerar e verificar hashes, aumentando a segurança da autenticação.

### Interface personalizada

Os formulários utilizam componentes do Guna UI2 para oferecer uma interface mais moderna do que os controles padrões do Windows Forms.

### Busca incremental

O sistema permite filtrar registros conforme o administrador digita, facilitando a localização de usuários, treinos ou exercícios.

### Operações CRUD

O projeto aplica as quatro operações fundamentais de sistemas com persistência de dados:

```text
Create → cadastrar
Read   → consultar
Update → atualizar
Delete → excluir
```

---

## Estrutura do projeto

```text
Academia/
│
├── LoginAcademia/
│   ├── BDacademia/
│   │   └── Arquivos relacionados ao banco de dados
│   │
│   ├── Properties/
│   ├── Resources/
│   │
│   ├── AcademiaBLL.cs
│   ├── AcademiaDAL.cs
│   ├── Endereco.cs
│   ├── Erro.cs
│   ├── Login_Cadastro.cs
│   ├── Treino.cs
│   ├── TreinoExercico.cs
│   │
│   ├── IHMLogin.cs
│   ├── IHMcadastro.cs
│   ├── IHMCliente.cs
│   ├── IHMPerfil.cs
│   ├── IHMEditarPerfil.cs
│   ├── IHMAdm1.cs
│   ├── IHMAdmMeio.cs
│   ├── IHMAdm2.cs
│   │
│   ├── App.config
│   ├── Program.cs
│   └── LoginAcademia.csproj
│
├── LoginAcademia.sln
├── packages/
└── .gitignore
```

---

## Como executar o projeto

### Pré-requisitos

Antes de iniciar, tenha instalado:

* Windows;
* Visual Studio;
* Carga de trabalho **Desenvolvimento para desktop com .NET**;
* .NET Framework 4.7.2;
* SQL Server ou SQL Server Express;
* SQL Server Management Studio, recomendado para administrar o banco;
* Git, caso queira clonar o repositório pelo terminal.

### 1. Clone o repositório

```bash
git clone https://github.com/enricobarni/Academia.git
```

### 2. Acesse a pasta do projeto

```bash
cd Academia
```

### 3. Abra a solução

Abra o arquivo:

```text
LoginAcademia.sln
```

Também é possível abrir pelo terminal:

```bash
start LoginAcademia.sln
```

### 4. Restaure os pacotes

No Visual Studio:

1. Clique com o botão direito na solução;
2. Selecione **Restaurar Pacotes NuGet**;
3. Aguarde a instalação das dependências.

### 5. Configure o banco de dados

Verifique os arquivos do diretório:

```text
LoginAcademia/BDacademia
```

Configure a conexão no arquivo:

```text
LoginAcademia/App.config
```

A string de conexão deve apontar para a instância correta do SQL Server ou para o arquivo de banco utilizado localmente.

Exemplo genérico:

```xml
<connectionStrings>
  <add
    name="ConexaoAcademia"
    connectionString="Data Source=SERVIDOR;Initial Catalog=Academia;Integrated Security=True"
    providerName="System.Data.SqlClient" />
</connectionStrings>
```

Substitua `SERVIDOR` e `Academia` pelos dados correspondentes ao seu ambiente.

### 6. Execute a aplicação

No Visual Studio:

```text
F5
```

Ou utilize:

```text
Ctrl + F5
```

A aplicação será iniciada pela tela de login.

---

## Acesso de administrador

Para acessar as funcionalidades administrativas da aplicação, utilize as seguintes credenciais:

```text
Usuário: admin
Senha: admin123
```

Após realizar o login, o sistema identificará o perfil administrativo e direcionará o usuário para a área de gerenciamento de clientes, treinos e exercícios.

> **Observação:** essas credenciais foram criadas para fins acadêmicos e de demonstração. Em um ambiente de produção, senhas padrão não devem ser expostas no repositório e precisam ser substituídas por credenciais seguras.


---

## Aprendizados obtidos

Durante o desenvolvimento deste projeto, foram praticados conceitos como:

* Organização de uma aplicação em camadas;
* Modelagem de banco de dados relacional;
* Desenvolvimento de interfaces desktop;
* Comunicação entre formulários;
* Escrita e execução de comandos SQL;
* Validação de entradas;
* Autenticação de usuários;
* Hash de senhas;
* Operações CRUD;
* Relacionamentos entre tabelas;
* Tratamento de exceções;
* Versionamento de código com Git.

O projeto representa uma etapa importante no aprendizado de arquitetura de software e demonstra a construção de uma aplicação desktop completa, desde a interface até a persistência dos dados.

---

## Contexto acadêmico

Este sistema foi desenvolvido como atividade acadêmica do segundo bimestre, com foco na aplicação prática dos conteúdos estudados em programação, banco de dados e arquitetura de sistemas.

---

## Autor

### Desenvolvido por:

* GitHub: [enricobarni](https://github.com/enricobarni)
* LinkedIn: [Enrico Barni Venturato](https://www.linkedin.com/in/enrico-barni-venturato/)
 
<br>

* GitHub: [Matheus-Deus](https://github.com/Matheus-Deus)
* LinkedIn: [Matheus Cavalcanti](https://www.linkedin.com/in/matheus-deus/)
  
<br>

* GitHub: [giovanisteiger](https://github.com/giovanisteiger)
* LinkedIn: [Giovani Steiger](https://www.linkedin.com/in/giovani-steiger-7a2912389/)

---

Este projeto faz parte da nossa jornada de aprendizado em desenvolvimento de software, arquitetura de sistemas, banco de dados e programação com C#.
