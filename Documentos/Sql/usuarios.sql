-- dbggclt.usuarios definição

-- Drop table

-- DROP TABLE dbggclt.usuarios;

CREATE TABLE dbggclt.usuarios (
	id uuid NOT NULL,
	nome varchar(255) NOT NULL,
	email varchar(255) NOT NULL,
	data_nascimento timestamp NOT NULL,
	sexo int2 NOT NULL,
	tipo int2 NOT NULL,
	data_cadastro timestamp DEFAULT now() NOT NULL,
	senha varchar(255) NOT NULL,
	status int2 NOT NULL,
	data_status timestamp NOT NULL,
	refresh_token varchar(255) NULL,
	refresh_token_data_expiracao timestamp NULL,
	reset_password_token varchar(255) NULL,
	reset_password_data_expiracao timestamp NULL,
	codigo_video varchar(6) NOT NULL,
	CONSTRAINT usuarios_email_key UNIQUE (email),
	CONSTRAINT usuarios_pkey PRIMARY KEY (id)
);