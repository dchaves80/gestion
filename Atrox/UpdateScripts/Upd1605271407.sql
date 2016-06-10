Use GestionSystem
Go
create procedure select_top5productsByName (@UserId bigint, @Top bigint, @Chain varchar(50) )
as
begin
declare @chaintosearch varchar(50) = @Chain + '%'
select top (@Top) Descripcion from Articulos where Descripcion like @chaintosearch
end
go
create procedure select_top5productsByCodigo (@UserId bigint, @Top bigint, @Chain varchar(50) )
as
begin
declare @chaintosearch varchar(50) = @Chain + '%'
select top (@Top) Descripcion from Articulos where CodigoInterno like @chaintosearch
end
go
ALTER procedure select_top5productsByCodigo (@UserId bigint, @Top bigint, @Chain varchar(50) )
as
begin
declare @chaintosearch varchar(50) = @Chain + '%'
select top (@Top) CodigoInterno from Articulos where CodigoInterno like @chaintosearch and IdUser=@UserId
end
go
ALTER procedure select_top5productsByName (@UserId bigint, @Top bigint, @Chain varchar(50) )
as
begin
declare @chaintosearch varchar(50) = @Chain + '%'
select top (@Top) Descripcion from Articulos where Descripcion like @chaintosearch and IdUser=@UserId
end
go