CREATE TABLE dbggclt.email_balde (
	id bigserial NOT NULL,
	usuario_id uuid NOT NULL,
	email varchar(255) NOT NULL,
	tipo int8 NOT NULL,
	data_cadastro timestamp DEFAULT CURRENT_TIMESTAMP NOT NULL,
	data_inicio_processamento timestamp NULL
);