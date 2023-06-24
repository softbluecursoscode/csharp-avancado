
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/25/2014 14:16:37
-- Generated from EDMX file: D:\VisualStudio\Projetos\ModelFirst\AppModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [testdb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AlunoSet'
CREATE TABLE [dbo].[AlunoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nome] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'NotaSet'
CREATE TABLE [dbo].[NotaSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Materia] nvarchar(max)  NOT NULL,
    [Valor] float  NOT NULL,
    [Aluno_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'AlunoSet'
ALTER TABLE [dbo].[AlunoSet]
ADD CONSTRAINT [PK_AlunoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NotaSet'
ALTER TABLE [dbo].[NotaSet]
ADD CONSTRAINT [PK_NotaSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Aluno_Id] in table 'NotaSet'
ALTER TABLE [dbo].[NotaSet]
ADD CONSTRAINT [FK_AlunoNota]
    FOREIGN KEY ([Aluno_Id])
    REFERENCES [dbo].[AlunoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AlunoNota'
CREATE INDEX [IX_FK_AlunoNota]
ON [dbo].[NotaSet]
    ([Aluno_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------