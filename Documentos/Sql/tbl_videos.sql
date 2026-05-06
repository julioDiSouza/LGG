CREATE TABLE banco.videos (
	id bigserial NOT NULL,
	usuario_id uuid NULL,
	campeonato_mensal_id int4 NULL,
	video_ciclo_id int8 NULL,
	titulo varchar(255) NOT NULL,
	descricao text NULL,
	url varchar(255) NOT NULL,
	video_id varchar(255) NOT NULL,
	qtd_view int8 DEFAULT 0 NOT NULL,
	qtd_like int8 DEFAULT 0 NOT NULL,
	url_imagem varchar(255) NULL,
	data_publicacao timestamp DEFAULT CURRENT_TIMESTAMP NOT NULL,
	data_cadastro timestamp DEFAULT CURRENT_TIMESTAMP NOT NULL,
	status int2 NULL,
	CONSTRAINT videos_pkey PRIMARY KEY (id)
);