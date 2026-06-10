CREATE TABLE dbggclt.video_top_dez (
	id bigserial NOT NULL,
	video_id int8 NOT NULL,
	video_ciclo_id int8 NULL,
	usuario_id uuid NULL,
	campeonato_mensal_id int4 NULL,
	titulo varchar(255) NOT NULL,
	url varchar(255) NOT NULL,
	qtd_view int8 DEFAULT 0 NOT NULL,
	qtd_like int8 DEFAULT 0 NOT NULL,
	url_imagem varchar(255) NULL,
	data_cadastro timestamp DEFAULT CURRENT_TIMESTAMP NOT NULL
);