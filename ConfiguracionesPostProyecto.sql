select IdUsuario, Nombres, Apellidos, Correo, Clave, Reestablecer, Activo from Usuario

insert into USUARIO(Nombres,Apellidos,Correo,Clave) values ('test 02','user 02','user02@example.com','ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae')
go

update USUARIO set Activo = 0 where IdUsuario = 2
go


select * from USUARIO 
go


--EN CASO DE ACTUALIZAR USE ESTE SCRIPT SOLO COMO EJEMPLO PARA VALIDAR EL STORE PROCEDURE

sp_EditarUsuario '2', 'Omar 02','user 02','user02@example.com','1', 'Correcto', '1'