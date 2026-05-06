-- Script de criação da tabela videos
-- Data: 2026-04-03

CREATE TABLE IF NOT EXISTS videos (
    id BIGSERIAL PRIMARY KEY,
    canal_id UUID NOT NULL,
    usuario_id UUID NULL,
    campeonato_mensal_id INTEGER NULL,
    video_ciclo_id BIGINT NULL,
    titulo VARCHAR(255) NOT NULL,
    descricao TEXT NULL,
    url VARCHAR(255) NOT NULL,
    video_id VARCHAR(255) NOT NULL,
    qtd_view BIGINT NOT NULL DEFAULT 0,
    qtd_like BIGINT NOT NULL DEFAULT 0,
    url_imagem VARCHAR(255) NULL,
    data_publicacao TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    data_criacao TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    ativo BOOLEAN NOT NULL DEFAULT TRUE,
    CONSTRAINT fk_videos_canal_id FOREIGN KEY (canal_id) REFERENCES canal(id)
);

-- Criar índices para melhor performance
CREATE INDEX idx_videos_canal_id ON videos(canal_id);
CREATE INDEX idx_videos_usuario_id ON videos(usuario_id);
CREATE INDEX idx_videos_ativo ON videos(ativo);
CREATE INDEX idx_videos_data_publicacao ON videos(data_publicacao);
CREATE INDEX idx_videos_video_id ON videos(video_id);
