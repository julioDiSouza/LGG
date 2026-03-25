select * from dbggclt.usuarios;

-- Script de criação da tabela Canal
-- Data: 2026-03-19

CREATE TABLE IF NOT EXISTS canais (
    id UUID PRIMARY KEY,
    usuario_id UUID NOT NULL,
    canal_id VARCHAR(36) NULL,
    codigo VARCHAR(6) NOT NULL UNIQUE,
    nome VARCHAR(255) NULL,
	titulo VARCHAR(255) NULL,
    url_personalizada VARCHAR(255) NULL,
    url_imagem VARCHAR(255) NULL,
    status INTEGER NOT NULL DEFAULT 0,
    data_cadastro TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Criar índices para melhor performance
CREATE INDEX idx_canais_usuario_id ON canais(usuario_id);
CREATE INDEX idx_canais_codigo ON canais(codigo);
CREATE INDEX idx_canais_status ON canais(status);


