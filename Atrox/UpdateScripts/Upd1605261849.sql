Use GestionSystem
go
/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE Facturas
	(
	Id bigint NOT NULL IDENTITY (1, 1),
	IdFactura varchar(50) NOT NULL,
	IdUser bigint NOT NULL,
	Fecha date NOT NULL,
	Finalizada bit NOT NULL,
	Emitida bit NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE Facturas SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
GO
CREATE PROCEDURE INSERT_FACTURA (@IdUser bigint,@Fecha Date)
as
begin
insert into Facturas (IdFactura,IdUser,Fecha,Finalizada,Emitida) values ('',@IdUser,@Fecha,0,0)
end
GO
CREATE PROCEDURE DELETE_FACTURA (@IdUser bigint, @IdFactura bigint)
as
begin
delete Facturas where Id=@IdFactura and IdUser=@IdUser
end
GO
CREATE PROCEDURE FINALIZAR_FACTURA (@IdUser bigint, @IdFactura bigint)
as
begin
update Facturas set Finalizada=1 where Id=@IdFactura and IdUser=@IdUser
end
go
CREATE PROCEDURE SELECT_ULTIMAFACTURACREADA (@IdUser bigint)
as
begin
select top 1 * from Facturas where IdUser=@IdUser ORDER BY Id desc
end

