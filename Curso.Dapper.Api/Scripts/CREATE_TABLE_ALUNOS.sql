CREATE TABLE Alunos(
    Id INTEGER PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(50) NOT NULL,
    Email VARCHAR(50) NOT NULL,
    DataNascimento DATETIME NOT NULL,
    Ativo BIT NOT NULL,
    DataCriacao DATETIME NOT NULL
)

CREATE TABLE Cursos(
	Id INTEGER PRIMARY KEY IDENTITY(1,1),
	Nome VARCHAR(150) NOT NULL,
	Descricao VARCHAR(50) NOT NULL,
    Coordenador VARCHAR(50) NOT NULL,
    Professor VARCHAR(50) NOT NULL,
	DataCriacao DATETIME NOT NULL
)

CREATE TABLE AlunosCursos(
	Id INTEGER PRIMARY KEY IDENTITY(1,1),
	IdAluno INTEGER NOT NULL,
	IdCurso INTEGER NOT NULL,
	DataCriacao DATETIME NOT NULL,
FOREIGN KEY (IdAluno) REFERENCES Alunos(Id),
FOREIGN KEY (IdCurso) REFERENCES Cursos(Id)
)

CREATE TABLE Turnos(
	Id INTEGER PRIMARY KEY IDENTITY(1,1),
	Nome VARCHAR(50) NOT NULL,
	DataCriacao DATETIME NOT NULL
)

CREATE TABLE Turmas(
	Id INTEGER PRIMARY KEY IDENTITY(1,1),
	Nome VARCHAR(50) NOT NULL,
	NomeCurso VARCHAR(50) NOT NULL,
	DataCriacao DATETIME NOT NULL,
	IdCurso INTEGER NOT NULL,
	IdTurno INTEGER NOT NULL,
FOREIGN KEY (IdCurso) REFERENCES Cursos(Id),
FOREIGN KEY (IdTurno) REFERENCES Turnos(Id)
)

CREATE TABLE CursosTurmas(
	Id INTEGER PRIMARY KEY IDENTITY(1,1),
	IdCurso INTEGER NOT NULL,
	IdTurma INTEGER NOT NULL,
	DataCriacao DATETIME NOT NULL,
FOREIGN KEY (IdCurso) REFERENCES Cursos(Id),
FOREIGN KEY (IdTurma) REFERENCES Turmas(Id)
)

INSERT INTO Turnos(Nome, DataCriacao)
VALUES('Manh�', '2020-01-01')

INSERT INTO Turnos(Nome, DataCriacao)
VALUES('Tarde', '2020-01-01')

INSERT INTO Turnos(Nome, DataCriacao)
VALUES('Noite', '2020-01-01')

INSERT INTO Turnos(Nome, DataCriacao)
VALUES('Integral', '2020-01-01')

-- Path: Curso.Dapper.Exemplo.Api\Scripts\INSERT_ALUNOS.sql
INSERT INTO Alunos(Nome, Email, DataNascimento, Ativo, DataCriacao)
VALUES('Jo�o', 'jose@email.com', '1990-01-01', 1, '2020-01-01')

-- Path: Curso.Dapper.Exemplo.Api\Scripts\INSERT_ALUNOS.sql
INSERT INTO Alunos(Nome, Email, DataNascimento, Ativo, DataCriacao)
VALUES('Maria', 'maria@email.com', '1990-01-01', 1, '2020-01-01')

-- Path: Curso.Dapper.Exemplo.Api\Scripts\INSERT_ALUNOS.sql
INSERT INTO Alunos(Nome, Email, DataNascimento, Ativo, DataCriacao)
VALUES('Jos�', 'jose@email.com', '1990-01-01', 1, '2020-01-01')

-- Path: Curso.Dapper.Exemplo.Api\Scripts\INSERT_CURSOS.sql
INSERT INTO Cursos(Nome, Descricao, Coordenador, Professor, DataCriacao)
VALUES('Analise Desenvolvimento de Sistemas', 'Descri��o 1', 'Coordenador 1', 'Professor 1', '2020-01-01')

-- Path: Curso.Dapper.Exemplo.Api\Scripts\INSERT_CURSOS.sql
INSERT INTO Cursos(Nome, Descricao, Coordenador, Professor, DataCriacao)
VALUES('Ci�ncias da Computa��o', 'Descri��o 2', 'Coordenador 2', 'Professor 2', '2020-01-01')

-- Path: Curso.Dapper.Exemplo.Api\Scripts\INSERT_CURSOS.sql
INSERT INTO Cursos(Nome, Descricao, Coordenador, Professor, DataCriacao)
VALUES('Engenharia da Computa��o', 'Descri��o 3', 'Coordenador 3', 'Professor 3', '2020-01-01')

-- Path: Curso.Dapper.Exemplo.Api\Scripts\INSERT_CURSOS.sql
INSERT INTO Cursos(Nome, Descricao, Coordenador, Professor, DataCriacao)
VALUES('Sistemas de Informa��o', 'Descri��o 4', 'Coordenador 4', 'Professor 4', '2020-01-01')

INSERT INTO Turmas(Nome, NomeCurso, DataCriacao, IdCurso, IdTurno)
VALUES('Turma 1', 'Analise Desenvolvimento de Sistemas', '2020-01-01', 1, 1)

INSERT INTO Turmas(Nome, NomeCurso, DataCriacao, IdCurso, IdTurno)
VALUES('Turma 2', 'Ci�ncias da Computa��o', '2020-01-01', 2, 2)

INSERT INTO Turmas(Nome, NomeCurso, DataCriacao, IdCurso, IdTurno)
VALUES('Turma 3', 'Engenharia da Computa��o', '2020-01-01', 3, 3)

INSERT INTO Turmas(Nome, NomeCurso, DataCriacao, IdCurso, IdTurno)
VALUES('Turma 4', 'Sistemas de Informa��o', '2020-01-01', 4, 4)

INSERT INTO AlunosCursos(IdAluno, IdCurso, DataCriacao)
VALUES(1, 1, '2020-01-01')

INSERT INTO AlunosCursos(IdAluno, IdCurso, DataCriacao)
VALUES(2, 2, '2020-01-01')

INSERT INTO AlunosCursos(IdAluno, IdCurso, DataCriacao)
VALUES(3, 3, '2020-01-01')

INSERT INTO AlunosCursos(IdAluno, IdCurso, DataCriacao)
VALUES(1, 4, '2020-01-01')

INSERT INTO AlunosCursos(IdAluno, IdCurso, DataCriacao)
VALUES(2, 1, '2020-01-01')

INSERT INTO AlunosCursos(IdAluno, IdCurso, DataCriacao)
VALUES(3, 2, '2020-01-01')

INSERT INTO AlunosCursos(IdAluno, IdCurso, DataCriacao)
VALUES(1, 3, '2020-01-01')

INSERT INTO AlunosCursos(IdAluno, IdCurso, DataCriacao)
VALUES(2, 4, '2020-01-01')

INSERT INTO CursosTurmas(IdCurso, IdTurma, DataCriacao)
VALUES(1, 1, '2020-01-01')

INSERT INTO CursosTurmas(IdCurso, IdTurma, DataCriacao)
VALUES(2, 2, '2020-01-01')

INSERT INTO CursosTurmas(IdCurso, IdTurma, DataCriacao)
VALUES(3, 3, '2020-01-01')

INSERT INTO CursosTurmas(IdCurso, IdTurma, DataCriacao)
VALUES(4, 4, '2020-01-01')

INSERT INTO CursosTurmas(IdCurso, IdTurma, DataCriacao)
VALUES(1, 2, '2020-01-01')

INSERT INTO CursosTurmas(IdCurso, IdTurma, DataCriacao)
VALUES(2, 3, '2020-01-01')

INSERT INTO CursosTurmas(IdCurso, IdTurma, DataCriacao)
VALUES(3, 4, '2020-01-01')

-- PROC DE CONSULTA POR ID
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.ObterTurnoPorId
	@id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Turnos
	WHERE Id = @id;
END
GO

-- PROC DE CONSULTA TODOS
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.ObterTurno
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Turnos;
END
GO

-- PROC DE INSERÇÃO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.InserirTurno
	@Nome VARCHAR(40)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO Turnos (Nome, DataCriacao)
	VALUES(@Nome, GETDATE() );
	SELECT CAST(SCOPE_IDENTITY() as int)
END
GO

--PROC DE ATUALIZAÇÃO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.AtualizarTurno
	@Nome VARCHAR(40),
	@id INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Turnos
	SET Nome = @Nome
	WHERE Id =@id;
	SELECT @@ROWCOUNT;
END
GO

-- PROC DE EXCLUSÃO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.ExcluirTurno
	@id INT
AS
BEGIN
	SET NOCOUNT ON;
	DELETE FROM Turnos
	WHERE Id =@id;
	SELECT @@ROWCOUNT;
END
GO

--PROC USANDO TABELA TIPO COM PARAMETRO
CREATE TYPE [dbo].[NomesTurnosType] AS TABLE
(
	[NomeTurno] VARCHAR(14) NOT NULL,
	[IdTurno] INT NOT NULL
)

CREATE PROCEDURE [dbo].[BuscarTurnosPorNomeEIds]
	@TabelaTurnos [dbo].[NomesTurnosType] READONLY
AS 
BEGIN
	SELECT
	*
	FROM Turnos (NOLOCK)
	WHERE Id in (SELECT IdTurno FROM @TabelaTurnos)
	
	UNION

	SELECT
	*
	FROM Turnos (NOLOCK)
	WHERE Nome in (SELECT NomeTurno FROM @TabelaTurnos)
END;

DECLARE @TabelaTurnos AS [dbo].[NomesTurnosType];

INSERT INTO @TabelaTurnos VALUES('Tarde', 0), ('', 1)

EXEC [dbo].[BuscarTurnosPorNomeEIds] @TabelaTurnos

-- Tabela FilaEnvioEmail
CREATE TABLE FilaEnvioEmail
(
	Id INT NOT NULL IDENTITY(1,1),
	CorpoEmail VARCHAR(4000) NOT NULL,
	Enviado BIT NOT NULL,
	De VARCHAR(200) NOT NULL,
	Para VARCHAR(200) NOT NULL
)
