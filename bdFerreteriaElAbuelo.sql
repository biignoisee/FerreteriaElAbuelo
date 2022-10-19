-- TRABAJO EXP FORMATIVA - SISTEMA DE VENTAS CON C# MVC FERRETERIA EL ABUELO
-- SE CREARA BASE DE DATOS / TABLAS / PROCEDIMIENTOS ALMACENADOS

--Creación de la database
Create database dbFerreteriaElAbuelo
GO

USE dbFerreteriaElAbuelo
GO


-- Implementar la logica en la creación de tablas 

--Tabla Categoria
CREATE TABLE CATEGORIA(
IdCategoria int primary key identity,
Descripcion varchar(100),
Activo bit default 1,
FechaRegistro datetime default getdate()
)
GO

--Tabla Marca
CREATE TABLE MARCA(
IdMarca int primary key identity,
Descripcion varchar(100),
Activo bit default 1,
FechaRegistro datetime default getdate()
)
GO


--Tabla Producto
CREATE TABLE PRODUCTO(
IdProducto int primary key identity,
Nombre varchar(500),
Descripcion varchar(500),
IdMarca int references MARCA(IdMarca),
IdCategoria int references CATEGORIA(IdCategoria),
Precio decimal(10,2) default 0,
Stock int,
RutaImagen varchar(100),
NombreImagen varchar(100),
Activo bit default 1,
FechaRegistro datetime default getdate()
)
GO


--Tabla Cliente
CREATE TABLE CLIENTE(
IdCliente int primary key identity,
Nombres varchar(100),
Apellidos varchar(100),
Correo varchar(100),
Clave varchar(150),
Reestablecer bit default 0,   -- sistema crea por default la contraseña y nosotros lo reestablecemos
FechaRegistro datetime default getdate()
)
GO

--Tabla Carrito
CREATE TABLE CARRITO(
IdCarrito int primary key identity,
IdCliente int references CLIENTE(IdCliente),
IdProducto int references PRODUCTO(IdProducto),
Cantidad int
)
GO

--Tabla Venta
CREATE TABLE VENTA(
IdVenta int primary key identity,
IdCliente int references CLIENTE(IdCliente),
TotalProducto int,
MontoTotal decimal(10,2),
Contacto varchar(50),
IdDistrito varchar(10),
Telefono varchar(50),
Direccion varchar(500),
IdTransaccion varchar(50),   --trabajar transaccion con paypal
FechaVenta datetime default getdate()
)
GO


--tabla Detalle Venta
CREATE TABLE DETALLE_VENTA(
IdDetalleVenta int primary key identity,
IdVenta int references VENTA(IdVenta),
IdProducto int references PRODUCTO(IdProducto),
Cantidad int,
Total decimal(10,2)
)
GO


--Table Usuario
CREATE TABLE USUARIO(
IdUsuario int primary key identity,
Nombres varchar(100),
Apellidos varchar(100),
Correo varchar(100),
Clave varchar(150),
Reestablecer bit default 1,
Activo bit default 1,
FechaRegistro datetime default getdate()
)
GO


--CREAMOS LAS TABLAS QUE CORRESPONDIENTE PARA LA UBICACIÓN DEL CLIENTE

--Tabla DEPARTAMENTO
CREATE TABLE DEPARTAMENTO(
IdDepartamento varchar(2) not null,
Descripcion varchar(45) not null
)
GO

--Tabla Provincia
CREATE TABLE PROVINCIA(
IdProvincia varchar(4) not null,
Descripcion varchar(45) not null,
IdDepartamento varchar(2) not null
)
GO


--Tabla Distrito
CREATE TABLE DISTRITO(
IdDistrito varchar(6) not null,
Descripcion varchar(45) not null,
IdProvincia varchar(4) not null,
IdDepartamento varchar(2) not null
)
GO



-- INSERTAMOS DATOS A LA TABLA

insert into USUARIO(Nombres,Apellidos,Correo,Clave) values ('omar','aguilar','test@example.com','ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae')
go

select * from USUARIO
go

 insert into CATEGORIA(Descripcion) values 
 ('Industria'),
 ('Construcción'),
 ('Pinturas'),
 ('Proteccion - Equipo Vestuario'),
 ('Baño - Fontaneria'),
 ('Cerrajería'),
 ('Doméstico')
go


insert into MARCA(Descripcion) values
('KOPLAST'),
('CPP'),
('PAVCO'),
('TRUPER'),
('VIRUTEX'),
('TEKNO'),
('FORTE')
go

insert into DEPARTAMENTO(IdDepartamento,Descripcion)
values 
('01','Arequipa'),
('02','Ica'),
('03','Lima'),
('04','La Libertad')
go

insert into PROVINCIA(IdProvincia,Descripcion,IdDepartamento)
values
('0101','Arequipa','01'),
('0102','Camaná','01'),

--ICA - PROVINCIAS
('0201', 'Ica ', '02'),
('0202', 'Chincha ', '02'),

--LIMA - PROVINCIAS
('0301', 'Lima ', '03'),
('0302', 'Barranca ', '03'),

--La Libertad - PROVINCIAS
('0401', 'La Libertad ', '04'),
('0402', 'Trujillo ', '04')
go


insert into DISTRITO(IdDistrito,Descripcion,IdProvincia,IdDepartamento) values 
('010101','Nieva','0101','01'),
('010102', 'El Cenepa', '0101', '01'),
('010201', 'Camaná', '0102', '01'),
('010202', 'José María Quimper', '0102', '01'),

--ICA - DISTRITO
('020101', 'Ica', '0201', '02'),
('020102', 'La Tinguiña', '0201', '02'),
('020201', 'Chincha Alta', '0202', '02'),
('020202', 'Alto Laran', '0202', '02'),

--LIMA - DISTRITO
('030101', 'Lima', '0301', '03'),
('030102', 'Ancón', '0301', '03'),
('030201', 'Barranca', '0302', '03'),
('030202', 'Paramonga', '0302', '03'),

--LA LIBERTAD - DISTRITO
('040101', 'La Libertad', '0401', '04'),
('040102', 'Otuzco', '0401', '04'),
('040201', 'Trujillo', '0402', '04'),
('040202', 'La Esperanza', '0402', '04')
go


select * from DISTRITO
go