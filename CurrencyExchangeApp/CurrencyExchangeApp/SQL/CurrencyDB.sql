/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     Sat, Aug, 19, 2017 1:45:23 AM                */
/*==============================================================*/


if exists (select 1
          from sysobjects
          where  id = object_id('USP_CURRENCYRATE_INSERT')
          and type in ('P','PC'))
   drop procedure USP_CURRENCYRATE_INSERT
go

if exists (select 1
          from sysobjects
          where  id = object_id('USP_CURRENCYRATE_INSERTMANY')
          and type in ('P','PC'))
   drop procedure USP_CURRENCYRATE_INSERTMANY
go

if exists (select 1
          from sysobjects
          where  id = object_id('USP_CURRENCYRATE_SELECTALLBYCODE')
          and type in ('P','PC'))
   drop procedure USP_CURRENCYRATE_SELECTALLBYCODE
go

if exists (select 1
          from sysobjects
          where  id = object_id('USP_CURRENCYRATE_SELECTBYCRITERIA')
          and type in ('P','PC'))
   drop procedure USP_CURRENCYRATE_SELECTBYCRITERIA
go

if exists (select 1
          from sysobjects
          where  id = object_id('USP_CURRENCY_DELETEALL')
          and type in ('P','PC'))
   drop procedure USP_CURRENCY_DELETEALL
go

if exists (select 1
          from sysobjects
          where  id = object_id('USP_CURRENCY_INSERT')
          and type in ('P','PC'))
   drop procedure USP_CURRENCY_INSERT
go

if exists (select 1
          from sysobjects
          where  id = object_id('USP_CURRENCY_INSERTMANY')
          and type in ('P','PC'))
   drop procedure USP_CURRENCY_INSERTMANY
go

if exists (select 1
          from sysobjects
          where  id = object_id('USP_CURRENCY_SELECTALL')
          and type in ('P','PC'))
   drop procedure USP_CURRENCY_SELECTALL
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CURRENCYRATE') and o.name = 'FK_CURRENCY_REFERENCE_CURRENCY')
alter table CURRENCYRATE
   drop constraint FK_CURRENCY_REFERENCE_CURRENCY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CURRENCY')
            and   type = 'U')
   drop table CURRENCY
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CURRENCYRATE')
            and   name  = 'CURRENCYRATE_INDEX'
            and   indid > 0
            and   indid < 255)
   drop index CURRENCYRATE.CURRENCYRATE_INDEX
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CURRENCYRATE')
            and   type = 'U')
   drop table CURRENCYRATE
go

drop type CURRENCYTABLE
go

drop type CURRENCYRATETABLE
go

create type CURRENCYRATETABLE
   as table (
      RATE decimal(18,4) not null,
      DATE date not null,
      CURRENCYCODE nvarchar(20) not null
   )
go

create type CURRENCYTABLE
   as table (
      CODE nvarchar(20) not null,
      TITLE nvarchar(150) not null
   )
go

/*==============================================================*/
/* Table: CURRENCY                                              */
/*==============================================================*/
create table CURRENCY (
   CODE                 nvarchar(20)         not null,
   TITLE                nvarchar(150)        not null,
   constraint PK_CURRENCY primary key (CODE)
)
go

/*==============================================================*/
/* Table: CURRENCYRATE                                          */
/*==============================================================*/
create table CURRENCYRATE (
   ID                   uniqueidentifier     not null,
   RATE                 decimal(18,4)        null,
   DATE                 date                 null,
   CURRENCYCODE         nvarchar(20)         null,
   constraint PK_CURRENCYRATE primary key nonclustered (ID)
)
go

/*==============================================================*/
/* Index: CURRENCYRATE_INDEX                                    */
/*==============================================================*/
create unique index CURRENCYRATE_INDEX on CURRENCYRATE (
DATE ASC,
CURRENCYCODE ASC
)
include (RATE)
go

alter table CURRENCYRATE
   add constraint FK_CURRENCY_REFERENCE_CURRENCY foreign key (CURRENCYCODE)
      references CURRENCY (CODE)
go


create procedure USP_CURRENCYRATE_INSERT 
(
	@Rate decimal(18, 4),
	@Date date,
	@CurrencyCode nvarchar(20)
)
AS
BEGIN
	INSERT INTO [dbo].[CURRENCYRATE]
	(
		[RATE],
		[DATE],
		[CURRENCYCODE]
	)
	VALUES
	(
		@Rate,
		@Date,
		@CurrencyCode
	)
END
go


create procedure USP_CURRENCYRATE_INSERTMANY
(
	@rates [dbo].[CurrencyRateTable] READONLY
)
AS
BEGIN
	INSERT INTO [dbo].[CURRENCYRATE]
	(
		[RATE],
		[DATE],
		[CURRENCYCODE]
	)
	SELECT
		[RATE],
		[DATE],
		[CURRENCYCODE]
	FROM @rates
END
go


create procedure USP_CURRENCYRATE_SELECTALLBYCODE
(
	@Code nvarchar(20)
)
AS
BEGIN
	SELECT
		CR.[Rate],
		CR.[Date],
		CR.[CurrencyCode]
	FROM
		[dbo].[CURRENCYRATE] CR
	WHERE 
		CR.[CurrencyCode] = @Code
	ORDER BY CR.[DATE] ASC
END
go


create procedure USP_CURRENCYRATE_SELECTBYCRITERIA
(
	@Code nvarchar(20)
)
AS
BEGIN
	SELECT TOP 1
		CR.[Rate],
		CR.[Date],
		CR.[CurrencyCode]
	FROM
		[dbo].[CURRENCYRATE] CR
	WHERE 
		CR.[CurrencyCode] = @Code
	ORDER BY CR.[DATE] DESC
END
go


create procedure USP_CURRENCY_DELETEALL
AS
BEGIN
	DELETE FROM [dbo].[CurrencyRate]
	DELETE FROM [dbo].[Currency]
END
go


create procedure USP_CURRENCY_INSERT
(
	@Code nvarchar(20),
	@Title nvarchar(150)
)
AS
BEGIN
	INSERT INTO [dbo].[CURRENCY]
	(
		[CODE],
		[TITLE]
	)
	VALUES
	(
		@Code,
		@Title
	)
END
go


create procedure USP_CURRENCY_INSERTMANY
(
	@Currencies [dbo].[CurrencyTable] READONLY
)
AS
BEGIN
	INSERT INTO [dbo].[CURRENCY]
	(
		[CODE],
		[TITLE]
	)
	SELECT
		[Code],
		[Title]
	FROM @Currencies
END
go


create procedure USP_CURRENCY_SELECTALL
AS
BEGIN
	SELECT
		[Code],
		[Title]
	FROM
		[dbo].[CURRENCY]
END
go

