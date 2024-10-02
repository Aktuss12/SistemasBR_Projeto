CREATE TABLE PRODUTO (
    idProduto INT NOT NULL,
    nome VARCHAR(100) NOT NULL,
    categoria VARCHAR(50) NOT NULL,
    fornecedor VARCHAR(50),
    codigoDeBarras VARCHAR(13),
    unidadeDeMedida VARCHAR(50) NOT NULL,
    estoque INTEGER NOT NULL,
    marca VARCHAR(50),
    custo DECIMAL(7, 2),
    markup DECIMAL(7, 2),
    precoDaVenda DECIMAL(7, 2) NOT NULL,
    ativo SMALLINT NOT NULL,
    imagem BLOB,
    CONSTRAINT PK_PRODUTO PRIMARY KEY (idProduto)
);

CREATE GENERATOR id_produto_generator;

CREATE TRIGGER id_produto_trigger FOR PRODUTO
BEFORE INSERT position 0
AS
BEGIN
    NEW.idProduto = GEN_ID(id_produto_generator, 1);
    NEW.ativo = 1;
END;

CREATE TABLE INFORMACOESFISCAIS (
    idProduto INT,
    origemDaMercadoria VARCHAR(50),
    situacaoTributaria VARCHAR(50),
    naturezaDaOperacao VARCHAR(50),
    ncm VARCHAR(8),
    aliquotaDeIcms DECIMAL(5, 2),
    reducaoDeCalculo DECIMAL(5, 2),
    FOREIGN KEY (idProduto) REFERENCES PRODUTO(idProduto)
);

DROP TABLE INFORMACOESFISCAIS;
DROP TABLE PRODUTO;
DROP GENERATOR id_produto_generator;
DROP TRIGGER id_produto_trigger;
