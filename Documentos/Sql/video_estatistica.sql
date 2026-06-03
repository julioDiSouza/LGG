CREATE TABLE dbggclt.video_estatistica (
	id bigserial NOT NULL,
	video_ciclo_id int8 NOT null,
	video_id int8 NULL,
	qtd_view int4 DEFAULT 0 NOT NULL,
	qtd_like int4 DEFAULT 0 NOT NULL,
	data_cadastro timestamp DEFAULT CURRENT_TIMESTAMP NOT null
);
