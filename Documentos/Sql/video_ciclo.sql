CREATE TABLE dbggclt.video_ciclo (
	id bigserial NOT NULL,
	campeonato_mensal_id int4 NULL,
	data_cadastro timestamp DEFAULT CURRENT_TIMESTAMP NOT NULL,
	status int2 NULL,
	data_status timestamp DEFAULT CURRENT_TIMESTAMP NOT NULL
);