/****
 Este script cria a tabela Musica
****/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Musica](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [nchar](30) NOT NULL,
	[Album] [nchar](30) NULL,
	[Ano] [int] NULL,
	[Genero] [nchar](30) NULL,
	[Cantor] [nchar](30) NULL,
 CONSTRAINT [PK_Musica] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


