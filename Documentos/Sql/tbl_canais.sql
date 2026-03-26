-- dbggclt.canais definição

-- Drop table

-- DROP TABLE dbggclt.canais;

CREATE TABLE dbggclt.canais (
	id uuid NOT NULL,
	usuario_id uuid NOT NULL,
	canal_id varchar(36) NULL,
	codigo varchar(6) NOT NULL,
	nome varchar(255) NULL,
	url_personalizada varchar(255) NULL,
	url_imagem varchar(255) NULL,
	status int4 DEFAULT 0 NOT NULL,
	data_cadastro timestamp DEFAULT CURRENT_TIMESTAMP NOT NULL,
	titulo varchar(255) NULL,
	CONSTRAINT canais_codigo_key UNIQUE (codigo),
	CONSTRAINT canais_pkey PRIMARY KEY (id)
);
CREATE INDEX idx_canais_codigo ON dbggclt.canais USING btree (codigo);
CREATE INDEX idx_canais_status ON dbggclt.canais USING btree (status);
CREATE INDEX idx_canais_usuario_id ON dbggclt.canais USING btree (usuario_id);