CREATE TABLE dbggclt.campeonato_mensal (
	id int4 GENERATED ALWAYS AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START 1 CACHE 1 NO CYCLE) NOT NULL,
	mes int2 NOT NULL,
	ano int4 NOT NULL,
	data_inicio timestamp NOT NULL,
	data_fim timestamp NOT NULL,
	status int2 NOT NULL,
	data_cadastro timestamp DEFAULT CURRENT_TIMESTAMP NOT NULL,
	CONSTRAINT campeonato_mensal_pkey PRIMARY KEY (id)
);