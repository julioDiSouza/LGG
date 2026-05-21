-- dbggclt.videos_balde definição

-- Drop table

-- DROP TABLE dbggclt.videos_balde;

CREATE TABLE dbggclt.videos_balde (
	id bigserial NOT NULL,
	video_ciclo_id int8 NOT NULL,
	usuario_id uuid NOT NULL,
	video_id int8 NOT NULL,
	video_id_yt varchar(255) NOT NULL,
	data_cadastro timestamp DEFAULT CURRENT_TIMESTAMP NOT NULL,
	data_inicio_processamento timestamp DEFAULT CURRENT_TIMESTAMP NULL
);