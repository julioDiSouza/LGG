-- dbggclt.campeonato_mensal definição

-- Drop table

-- DROP TABLE dbggclt.campeonato_mensal;

CREATE TABLE dbggclt.campeonato_mensal (
	id integer GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	mes int2 NOT NULL,
	ano integer NOT NULL,
	data_inicio timestamp NOT NULL,
	data_fim timestamp NOT NULL,
	status int2 NOT NULL,
	data_cadastro timestamp DEFAULT CURRENT_TIMESTAMP NOT NULL
);