IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220602171950_PrimeiraMigracao')
BEGIN
    CREATE TABLE [Clientes] (
        [Id] int NOT NULL IDENTITY,
        [Nome] VARCHAR(80) NOT NULL,
        [Phone] CHAR(11) NULL,
        [CEP] CHAR(8) NOT NULL,
        [Estado] CHAR(2) NOT NULL,
        [Cidade] nvarchar(60) NOT NULL,
        CONSTRAINT [PK_Clientes] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220602171950_PrimeiraMigracao')
BEGIN
    CREATE TABLE [Produtos] (
        [Id] int NOT NULL IDENTITY,
        [CodigoBarras] VARCHAR(14) NOT NULL,
        [Descricao] VARCHAR(60) NOT NULL,
        [Valor] decimal(18,2) NOT NULL,
        [TipoProduto] nvarchar(max) NOT NULL,
        [Ativo] bit NOT NULL,
        CONSTRAINT [PK_Produtos] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220602171950_PrimeiraMigracao')
BEGIN
    CREATE TABLE [Pedidos] (
        [Id] int NOT NULL IDENTITY,
        [CienteId] int NOT NULL,
        [CLienteId] int NULL,
        [IniciadoEM] datetime2 NOT NULL DEFAULT (GETDATE()),
        [FinalizadoEm] datetime2 NOT NULL,
        [TipoFrete] int NOT NULL,
        [Status] nvarchar(max) NOT NULL,
        [Observacoes] VARCHAR(512) NULL,
        CONSTRAINT [PK_Pedidos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Pedidos_Clientes_CLienteId] FOREIGN KEY ([CLienteId]) REFERENCES [Clientes] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220602171950_PrimeiraMigracao')
BEGIN
    CREATE TABLE [PedidoItem] (
        [Id] int NOT NULL IDENTITY,
        [PedidoId] int NOT NULL,
        [ProdutoId] int NOT NULL,
        [Quantidade] int NOT NULL DEFAULT 0,
        [Valor] decimal(18,2) NOT NULL DEFAULT 0.0,
        [Desconto] decimal(18,2) NOT NULL DEFAULT 0.0,
        CONSTRAINT [PK_PedidoItem] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PedidoItem_Pedidos_PedidoId] FOREIGN KEY ([PedidoId]) REFERENCES [Pedidos] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_PedidoItem_Produtos_ProdutoId] FOREIGN KEY ([ProdutoId]) REFERENCES [Produtos] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220602171950_PrimeiraMigracao')
BEGIN
    CREATE INDEX [idx_cliente_telefone] ON [Clientes] ([Phone]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220602171950_PrimeiraMigracao')
BEGIN
    CREATE INDEX [IX_PedidoItem_PedidoId] ON [PedidoItem] ([PedidoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220602171950_PrimeiraMigracao')
BEGIN
    CREATE INDEX [IX_PedidoItem_ProdutoId] ON [PedidoItem] ([ProdutoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220602171950_PrimeiraMigracao')
BEGIN
    CREATE INDEX [IX_Pedidos_CLienteId] ON [Pedidos] ([CLienteId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220602171950_PrimeiraMigracao')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220602171950_PrimeiraMigracao', N'3.1.5');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220602183703_SegundaMigracao')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220602183703_SegundaMigracao', N'3.1.5');
END;

GO

