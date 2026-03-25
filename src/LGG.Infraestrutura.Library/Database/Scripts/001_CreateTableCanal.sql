-- Script de criação da tabela Canal
-- Data: 2026-03-19

CREATE TABLE IF NOT EXISTS canal (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    usuario_id UUID NOT NULL,
    canal_id VARCHAR(36) NULL,
    codigo VARCHAR(6) NOT NULL UNIQUE,
    nome VARCHAR(255) NULL,
    url_personalizada VARCHAR(255) NULL,
    url_imagem VARCHAR(255) NULL,
    status INTEGER NOT NULL DEFAULT 0,
    data_cadastro TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Criar índices para melhor performance
CREATE INDEX idx_canal_usuario_id ON canal(usuario_id);
CREATE INDEX idx_canal_codigo ON canal(codigo);
CREATE INDEX idx_canal_status ON canal(status);
