select IdUsuario, Nombres, Apellidos, Correo, Clave, Reestablecer, Activo from Usuario

insert into USUARIO(Nombres,Apellidos,Correo,Clave) values ('test 02','user 02','user02@example.com','ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae')
go

update USUARIO set Activo = 0 where IdUsuario = 2
go


select * from USUARIO
go