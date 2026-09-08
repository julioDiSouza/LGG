--------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------
---------UK---------
alter table video_ciclo add constraint uk_video_ciclo unique(id);
alter table email_balde add constraint uk_user_id unique(usuario_id); 
--------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------
---------FK---------
alter table email_balde add constraint fk_usuario foreign key (usuario_id) references usuario(id);
---------------------
alter table video add constraint fk_usuario foreign key (usuario_id) references usuario(id);
alter table video add constraint fk_vd_cliclo foreign key (video_ciclo_id) references video_ciclo(id);
---------------------
alter table video_balde add constraint fk_usuario foreign key (usuario_id) references usuario(id);
alter table video_balde add constraint fk_vd_ciclo foreign key (video_ciclo_id) references video_ciclo(id);
alter table video_balde add constraint fk_vd_id foreign key (video_id) references video(id);
---------------------
alter table video_ciclo add constraint fk_cp_mid foreign key (campeonato_mensal_id) references campeonato_mensal(id);
---------------------
alter table video_estatistica add constraint fk_vd_ciclo foreign key(video_ciclo_id) references video_ciclo(id);
alter table video_estatistica add constraint fk_vd_id foreign key(video_id) references video(id);
---------------------
alter table video_top_dez add constraint fk_vd_id foreign key(video_id) references video(id);
alter table video_top_dez add constraint fk_vd_ciclo_id foreign key(video_ciclo_id) references video_ciclo(id);
alter table video_top_dez add constraint fk_usr_id foreign key(usuario_id) references usuario(id);
alter table video_top_dez add constraint fk_cp_mid foreign key(campeonato_mensal_id) references campeonato_mensal (id);
---------------------
--------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------
