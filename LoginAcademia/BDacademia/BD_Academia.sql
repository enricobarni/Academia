------------------------------------------------------
-- 1. RECRIAR O BANCO COM SEGURANCA
-- ------------------------------------------------------------
USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = N'AcademiaBD')
BEGIN
    ALTER DATABASE AcademiaBD SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE AcademiaBD;
END
GO


CREATE DATABASE AcademiaBD
GO
USE AcademiaBD;
GO


-- ------------------------------------------------------------
-- 2. GrupoMuscular
-- ------------------------------------------------------------
CREATE TABLE GrupoMuscular (
    cd_grupoMuscular INT          IDENTITY(1,1) NOT NULL,
    nm_grupoMuscular VARCHAR(50)  NOT NULL,
    ds_grupoMuscular VARCHAR(200) NULL,

    CONSTRAINT PK_GrupoMuscular      PRIMARY KEY (cd_grupoMuscular),
    CONSTRAINT UQ_GrupoMuscular_nome UNIQUE      (nm_grupoMuscular)
);
GO


-- ------------------------------------------------------------
-- 3. Exercicio
-- ------------------------------------------------------------
CREATE TABLE Exercicio (
    cd_exercicio     INT          IDENTITY(1,1) NOT NULL,
    nm_exercicio     VARCHAR(100) NOT NULL,
    ds_exercicio     VARCHAR(500) NULL,
    cd_grupoMuscular INT          NOT NULL,

    CONSTRAINT PK_Exercicio PRIMARY KEY (cd_exercicio),

    CONSTRAINT FK_Exercicio_GrupoMuscular
        FOREIGN KEY (cd_grupoMuscular)
        REFERENCES GrupoMuscular(cd_grupoMuscular)
        ON DELETE NO ACTION
        ON UPDATE CASCADE
);
GO


-- ------------------------------------------------------------
-- 4. Usuario
--    [1] nm_usuario = username de login (unico, alfanumerico)
--    [1] nm_cliente = nome completo de exibicao
--    [2] Sem campos de endereco (movidos para tabela Endereco)
-- ------------------------------------------------------------
CREATE TABLE Usuario (
    cd_usuario  INT          IDENTITY(1,1) NOT NULL,

    -- [1] username unico para login — apenas letras e numeros
    nm_usuario  VARCHAR(30)  NOT NULL,

    -- [1] nome completo exibido nas telas
    nm_cliente  VARCHAR(100) NOT NULL,

    ds_email    VARCHAR(150) NOT NULL,
    ds_senha    VARCHAR(60)  NOT NULL,   -- hash BCrypt, sempre 60 chars
    ds_telefone VARCHAR(11)  NOT NULL,   -- DDD + numero, sem formatacao

    ic_admin    BIT          NOT NULL CONSTRAINT DF_Usuario_icAdmin DEFAULT 0,
    ic_ativo    BIT          NOT NULL CONSTRAINT DF_Usuario_icAtivo DEFAULT 1,
    dt_cadastro DATETIME2    NOT NULL CONSTRAINT DF_Usuario_dtCad  DEFAULT GETDATE(),

    CONSTRAINT PK_Usuario PRIMARY KEY (cd_usuario),

    -- ambos unicos: login aceita email OU username
    CONSTRAINT UQ_Usuario_email     UNIQUE (ds_email),
    CONSTRAINT UQ_Usuario_nmUsuario UNIQUE (nm_usuario),

    -- username: apenas letras (a-z A-Z) e numeros (0-9), sem espaco
    CONSTRAINT CK_Usuario_nmUsuario
        CHECK (nm_usuario NOT LIKE '%[^a-zA-Z0-9]%')
);
GO


-- ------------------------------------------------------------
-- 5. Endereco  [2][3]
--    Nome da tabela corrigido (PDF tinha "Usuario" no cabecalho)
--    cd_usuario e PK + FK simultaneamente (relacao 1:1 rigida)
--    CASCADE: ao excluir usuario, endereco e removido junto
-- ------------------------------------------------------------
CREATE TABLE Endereco (
    cd_usuario INT          NOT NULL,   -- PK + FK (1:1 com Usuario)

    ds_cep     CHAR(8)      NOT NULL,   -- 8 digitos numericos, sem hifen
    ds_rua     VARCHAR(150) NOT NULL,   -- preenchido via ViaCEP
    ds_numero     VARCHAR(10)  NOT NULL,   -- aceita "123A", "S/N"
    ds_complemento VARCHAR(50) NULL,       -- opcional: Apto 12, Bloco B, etc.
    ds_bairro  VARCHAR(100) NOT NULL,   -- preenchido via ViaCEP
    ds_cidade  VARCHAR(100) NOT NULL,   -- preenchido via ViaCEP
    ds_estado  CHAR(2)      NOT NULL,   -- sigla UF: SP, RJ...

    CONSTRAINT PK_Endereco PRIMARY KEY (cd_usuario),

    CONSTRAINT FK_Endereco_Usuario
        FOREIGN KEY (cd_usuario)
        REFERENCES Usuario(cd_usuario)
        ON DELETE CASCADE
        ON UPDATE NO ACTION,

    -- CEP: exatamente 8 digitos numericos
    CONSTRAINT CK_Endereco_cep
        CHECK (ds_cep LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),

    -- Estado: exatamente 2 letras maiusculas
    CONSTRAINT CK_Endereco_estado
        CHECK (ds_estado LIKE '[A-Z][A-Z]'),

    -- Complemento: quando preenchido, nao pode ser string vazia nem so espacos
    CONSTRAINT CK_Endereco_complemento
        CHECK (ds_complemento IS NULL OR LEN(LTRIM(ds_complemento)) >= 1)
);
GO


-- ------------------------------------------------------------
-- 6. Treino
--    [4] tp_divisao CHECK definido — sem "?"
--    [5] cd_admin FK definida — sem "?" e sem parentese aberto
-- ------------------------------------------------------------
CREATE TABLE Treino (
    cd_treino   INT          IDENTITY(1,1) NOT NULL,
    nm_treino   VARCHAR(100) NOT NULL,
    ds_treino   VARCHAR(500) NULL,
    tp_divisao  CHAR(1)      NOT NULL,   -- [4] A, B, C, D ou E
    dt_inicio   DATETIME2    NOT NULL CONSTRAINT DF_Treino_dtInicio DEFAULT GETDATE(),
    dt_fim      AS DATEADD(MONTH, 2, dt_inicio) PERSISTED,
    cd_usuario  INT          NOT NULL,   -- cliente dono do treino
    cd_admin    INT          NOT NULL,   -- admin que criou o treino
    ic_ativo    BIT          NOT NULL CONSTRAINT DF_Treino_icAtivo DEFAULT 1,
    dt_cadastro DATETIME2    NOT NULL CONSTRAINT DF_Treino_dtCad  DEFAULT GETDATE(),

    CONSTRAINT PK_Treino PRIMARY KEY (cd_treino),

    -- nome do treino: aceita espaco entre palavras, mas nao no inicio/fim nem espaco duplicado
    CONSTRAINT CK_Treino_nmTreino
        CHECK (
            LEN(nm_treino) >= 3
            AND nm_treino = LTRIM(RTRIM(nm_treino))
            AND nm_treino NOT LIKE '%  %'
        ),

    -- [4] divisao restrita a A, B, C, D ou E
    CONSTRAINT CK_Treino_divisao
        CHECK (tp_divisao IN ('A','B','C','D','E')),

    -- dt_fim e calculada automaticamente como 2 meses apos dt_inicio
    CONSTRAINT CK_Treino_datas
        CHECK (dt_fim >= dt_inicio),

    -- cliente: CASCADE — treinos removidos ao excluir o usuario
    CONSTRAINT FK_Treino_Usuario
        FOREIGN KEY (cd_usuario)
        REFERENCES Usuario(cd_usuario)
        ON DELETE CASCADE
        ON UPDATE NO ACTION,

    -- [5] admin: NO ACTION — nao pode excluir admin com treinos vinculados
    --     (dois caminhos CASCADE para a mesma tabela causam erro no SQL Server)
    CONSTRAINT FK_Treino_Admin
        FOREIGN KEY (cd_admin)
        REFERENCES Usuario(cd_usuario)  -- mesma tabela Usuario, papel diferente
        ON DELETE NO ACTION
        ON UPDATE NO ACTION
);
GO


-- ------------------------------------------------------------
-- 7. TreinoExercicio
-- ------------------------------------------------------------
CREATE TABLE TreinoExercicio (
    cd_treinoExercicio  INT          IDENTITY(1,1) NOT NULL,
    cd_treino           INT          NOT NULL,
    cd_exercicio        INT          NOT NULL,
    qt_series           TINYINT      NOT NULL,
    qt_repeticoes       TINYINT      NOT NULL,
    qt_descansoSegundos SMALLINT     NOT NULL,
    nr_ordem            TINYINT      NOT NULL,
    ds_observacao       VARCHAR(300) NULL,

    CONSTRAINT PK_TreinoExercicio PRIMARY KEY (cd_treinoExercicio),

    CONSTRAINT CK_TreinoEx_series
        CHECK (qt_series >= 1),
    CONSTRAINT CK_TreinoEx_repeticoes
        CHECK (qt_repeticoes >= 1),
    CONSTRAINT CK_TreinoEx_descanso
        CHECK (qt_descansoSegundos >= 0),
    CONSTRAINT CK_TreinoEx_ordem
        CHECK (nr_ordem >= 1),

    -- CASCADE: ao excluir um treino, todos os exercicios vinculados em TreinoExercicio sao removidos automaticamente
    CONSTRAINT FK_TreinoEx_Treino
        FOREIGN KEY (cd_treino)
        REFERENCES Treino(cd_treino)
        ON DELETE CASCADE,

    CONSTRAINT FK_TreinoEx_Exercicio
        FOREIGN KEY (cd_exercicio)
        REFERENCES Exercicio(cd_exercicio)
        ON DELETE NO ACTION
);
GO


-- ------------------------------------------------------------
-- 8. INDICES DE PERFORMANCE
-- ------------------------------------------------------------

-- Exercicio
CREATE INDEX IX_Exercicio_cdGrupoMuscular
    ON Exercicio(cd_grupoMuscular);

-- Usuario — login pode ser por email ou por username
CREATE UNIQUE INDEX IX_Usuario_dsEmail
    ON Usuario(ds_email);
CREATE UNIQUE INDEX IX_Usuario_nmUsuario
    ON Usuario(nm_usuario);

-- Treino
CREATE INDEX IX_Treino_cdUsuario
    ON Treino(cd_usuario);
CREATE INDEX IX_Treino_cdAdmin
    ON Treino(cd_admin);
CREATE INDEX IX_Treino_tpDivisao
    ON Treino(tp_divisao);

-- TreinoExercicio
CREATE INDEX IX_TreinoEx_cdTreino
    ON TreinoExercicio(cd_treino);
CREATE INDEX IX_TreinoEx_cdExercicio
    ON TreinoExercicio(cd_exercicio);
GO


-- ------------------------------------------------------------
-- 9. SEED — Grupos musculares
-- ------------------------------------------------------------
INSERT INTO GrupoMuscular (nm_grupoMuscular, ds_grupoMuscular) VALUES
('Peito',   'Musculos peitoral maior e menor'),
('Costas',  'Latissimo do dorso, trapezio e romboides'),
('Ombro',   'Deltoides anterior, medial e posterior'),
('Biceps',  'Biceps braquial e braquial'),
('Triceps', 'Triceps braquial - tres cabecas'),
('Pernas',  'Quadriceps, isquiotibiais e gluteos'),
('Abdomen', 'Reto abdominal, obliquos e transverso');
GO


-- ------------------------------------------------------------
-- 10. SEED — Exercicios
--     Foram adicionados mais 6 exercicios para cada grupo muscular.
-- ------------------------------------------------------------
INSERT INTO Exercicio (nm_exercicio, ds_exercicio, cd_grupoMuscular) VALUES
-- Peito (1) - existentes
('Supino Reto',              'Exercicio base para peitoral com barra ou halteres',             1),
('Crucifixo',                'Isolamento de peitoral em banco plano',                          1),
('Flexao de Bracos',         'Exercicio funcional para peitoral e triceps',                    1),
-- Peito (1) - novos
('Supino Inclinado',         'Variacao inclinada com foco na parte superior do peitoral',      1),
('Supino Declinado',         'Variacao declinada com foco na parte inferior do peitoral',      1),
('Crossover',                'Aducao horizontal no cabo para isolamento do peitoral',           1),
('Peck Deck',                'Crucifixo guiado na maquina para peitoral',                      1),
('Flexao Inclinada',         'Flexao com maos elevadas, menor intensidade',                    1),
('Flexao Declinada',         'Flexao com pes elevados, maior foco no peitoral superior',       1),

-- Costas (2) - existentes
('Puxada Frontal',           'Desenvolve o latissimo do dorso na polia alta',                  2),
('Remada Curvada',           'Exercicio composto para espessura de costas',                    2),
('Levantamento Terra',       'Movimento fundamental para cadeia posterior',                    2),
-- Costas (2) - novos
('Remada Baixa',             'Remada sentada na polia para dorsais e romboides',               2),
('Remada Unilateral',        'Remada com halter para trabalhar cada lado das costas',          2),
('Pullover na Polia',        'Movimento de extensao de ombro com foco em dorsais',             2),
('Barra Fixa',               'Exercicio com peso corporal para dorsais e biceps',              2),
('Puxada Neutra',            'Puxada na polia com pegada neutra',                              2),
('Remada Cavalinho',         'Remada com apoio ou barra T para espessura dorsal',              2),

-- Ombro (3) - existentes
('Desenvolvimento',          'Elevacao de barra ou halteres acima da cabeca',                  3),
('Elevacao Lateral',         'Isolamento do deltoide medial com halteres',                     3),
-- Ombro (3) - novos
('Elevacao Frontal',         'Isolamento do deltoide anterior com halteres ou barra',          3),
('Crucifixo Inverso',        'Movimento para deltoide posterior',                              3),
('Desenvolvimento Arnold',   'Variacao do desenvolvimento com rotacao dos halteres',           3),
('Remada Alta',              'Exercicio para deltoides e trapezio',                            3),
('Face Pull',                'Puxada para deltoide posterior e estabilidade escapular',        3),
('Elevacao Lateral na Polia','Variacao na polia para tensao constante no deltoide medial',     3),

-- Biceps (4) - existentes
('Rosca Direta',             'Curl com barra ou halteres para biceps',                         4),
('Rosca Martelo',            'Variacao neutra que trabalha braquial e braquiorradial',         4),
-- Biceps (4) - novos
('Rosca Alternada',          'Rosca com halteres alternando os bracos',                        4),
('Rosca Concentrada',        'Isolamento unilateral do biceps sentado',                        4),
('Rosca Scott',              'Rosca no banco Scott para maior controle do movimento',          4),
('Rosca na Polia',           'Rosca no cabo com tensao constante',                             4),
('Rosca Inclinada',          'Rosca com halteres em banco inclinado',                          4),
('Rosca 21',                 'Metodo com repeticoes parciais e completas para biceps',         4),

-- Triceps (5) - existentes
('Triceps Pulley',           'Extensao no cabo para isolamento do triceps',                    5),
('Triceps Frances',          'Extensao de testa com barra ou halteres',                        5),
('Mergulho',                 'Exercicio composto para triceps e peitoral inferior',            5),
-- Triceps (5) - novos
('Triceps Corda',            'Extensao no cabo usando corda',                                  5),
('Triceps Testa',            'Extensao de cotovelos deitado com barra ou halteres',            5),
('Supino Fechado',           'Supino com pegada fechada enfatizando triceps',                  5),
('Triceps Coice',            'Extensao unilateral de cotovelo com halter',                     5),
('Triceps Banco',            'Mergulho entre bancos com foco em triceps',                      5),
('Extensao Unilateral na Polia','Extensao de triceps unilateral no cabo',                      5),

-- Pernas (6) - existentes
('Agachamento',              'Movimento base para quadriceps, gluteos e isquiotibiais',        6),
('Leg Press',                'Agachamento guiado na maquina',                                  6),
('Cadeira Extensora',        'Isolamento de quadriceps na maquina',                            6),
-- Pernas (6) - novos
('Cadeira Flexora',          'Isolamento de posterior de coxa na maquina',                     6),
('Mesa Flexora',             'Flexao de joelhos deitado para posteriores',                     6),
('Avanco',                   'Passada unilateral para quadriceps e gluteos',                   6),
('Stiff',                    'Movimento para posteriores, gluteos e lombar',                   6),
('Panturrilha em Pe',        'Elevacao plantar em pe para gastrocnemio',                       6),
('Hip Thrust',               'Elevacao de quadril com foco em gluteos',                        6),

-- Abdomen (7) - existentes
('Abdominal Crunch',         'Exercicio classico de flexao de tronco',                         7),
('Prancha Isometrica',       'Estabilizacao do core sem movimento dinamico',                   7),
-- Abdomen (7) - novos
('Elevacao de Pernas',       'Exercicio para reto abdominal com foco na porcao inferior',      7),
('Abdominal Infra',          'Flexao de quadril para abdomen inferior',                        7),
('Abdominal Bicicleta',      'Movimento alternado para reto abdominal e obliquos',            7),
('Prancha Lateral',          'Estabilizacao lateral com foco em obliquos',                     7),
('Russian Twist',            'Rotacao de tronco para obliquos',                               7),
('Abdominal na Polia',       'Flexao de tronco com carga na polia alta',                       7);
GO

-- ------------------------------------------------------------
-- 11. SEED — Admin padrao
--
--  ATENCAO: antes de rodar, gere o hash BCrypt no C#:
--
--      string hash = BCrypt.Net.BCrypt.HashPassword("Admin@123");
--      Console.WriteLine(hash);
--
--  Substitua 'HASH_BCRYPT_AQUI' pelo valor impresso.
--  Nunca armazene senha em texto puro no banco.
-- ------------------------------------------------------------
DECLARE @idAdmin INT;

INSERT INTO Usuario (
    nm_usuario, nm_cliente, ds_email, ds_senha,
    ds_telefone, ic_admin, ic_ativo
) VALUES (
    'admin',                 -- nm_usuario: username de login
    'Administrador',         -- nm_cliente: nome de exibicao
    'admin@academia.com',    -- ds_email
    '$2a$11$gla6QkfB.Elcln9jUgYzKeJQlQAGk8zqeEY0JKRDPlWj64Y7vPJjW',      -- substituir pelo hash gerado em C#
    '13000000000',           -- ds_telefone (ficticio)
    1,                       -- ic_admin = 1 (administrador)
    1                        -- ic_ativo = 1
);

SET @idAdmin = SCOPE_IDENTITY();

-- Endereco obrigatorio: inserir logo apos criar o usuario
INSERT INTO Endereco (
    cd_usuario, ds_cep, ds_rua, ds_numero, ds_bairro, ds_cidade, ds_estado
) VALUES (
    @idAdmin,
    '11040020',         -- CEP ficticio (8 digitos sem hifen)
    'Rua da Academia',
    '1',
    'Centro',
    'Santos',
    'SP'
);
GO

-- APAGAR TODOS OS USUARIOS DE TESTE (descomente para usar)
-- ============================================================
--USE AcademiaBD;
--DELETE FROM Endereco;
--DELETE FROM Usuario WHERE ic_admin = 0;
--DBCC CHECKIDENT ('Usuario', RESEED, 1);
-- ============================================================


-- ============================================================
-- 12. QUERIES PRONTAS PARA O C#
-- ============================================================

-- ------------------------------------------------------------
-- A) Login — aceita email OU username
--    Usar na BLL: passar o valor digitado como @login
--    Verificar senha com BCrypt.Verify() no C# (nao no SQL)
-- ------------------------------------------------------------
/*
SELECT
    u.cd_usuario,
    u.nm_usuario,
    u.nm_cliente,
    u.ds_senha,      -- passar para BCrypt.Verify(senhaDigitada, ds_senha)
    u.ic_admin,
    u.ic_ativo
FROM Usuario u
WHERE (u.ds_email = @login OR u.nm_usuario = @login)
  AND u.ic_ativo = 1;
*/


-- ------------------------------------------------------------
-- B) Perfil completo do usuario (com endereco) — JOIN 1:1
-- ------------------------------------------------------------
/*
SELECT
    u.cd_usuario,
    u.nm_usuario,
    u.nm_cliente,
    u.ds_email,
    u.ds_telefone,
    u.ic_admin,
    u.ic_ativo,
    u.dt_cadastro,
    e.ds_cep,
    e.ds_rua,
    e.ds_numero,
    e.ds_bairro,
    e.ds_cidade,
    e.ds_estado
FROM Usuario u
    INNER JOIN Endereco e ON e.cd_usuario = u.cd_usuario
WHERE u.cd_usuario = @cd_usuario;
*/


-- ------------------------------------------------------------
-- C) Treinos do cliente com exercicios — DataGridView principal
--    Passar @cd_usuario = id do cliente selecionado
-- ------------------------------------------------------------
/*
SELECT
    t.cd_treino,
    t.nm_treino,
    t.tp_divisao,
    t.dt_inicio,
    t.dt_fim,
    t.ic_ativo,
    e.nm_exercicio,
    gm.nm_grupoMuscular,
    te.qt_series,
    te.qt_repeticoes,
    te.qt_descansoSegundos,
    te.nr_ordem,
    te.ds_observacao,
    adm.nm_cliente AS nm_admin
FROM Treino t
    INNER JOIN TreinoExercicio te  ON te.cd_treino        = t.cd_treino
    INNER JOIN Exercicio        e  ON e.cd_exercicio       = te.cd_exercicio
    INNER JOIN GrupoMuscular   gm  ON gm.cd_grupoMuscular  = e.cd_grupoMuscular
    INNER JOIN Usuario         adm ON adm.cd_usuario       = t.cd_admin
WHERE t.cd_usuario = @cd_usuario
ORDER BY t.dt_inicio DESC, te.nr_ordem ASC;
*/


-- ------------------------------------------------------------
-- D) Lista de clientes ativos — DataGridView de gerenciamento
-- ------------------------------------------------------------
/*
SELECT
    u.cd_usuario,
    u.nm_usuario,
    u.nm_cliente,
    u.ds_email,
    u.ds_telefone,
    u.dt_cadastro,
    e.ds_cidade,
    e.ds_estado
FROM Usuario u
    INNER JOIN Endereco e ON e.cd_usuario = u.cd_usuario
WHERE u.ic_admin = 0
  AND u.ic_ativo = 1
ORDER BY u.nm_cliente ASC;
*/


-- ============================================================
--  FIM DO SCRIPT v8 - dt_inicio DEFAULT GETDATE(), dt_fim calculada,
--  ON DELETE CASCADE em TreinoExercicio -> Treino e seeds expandidas
-- ============================================================
