# Application Setup

This document covers the technologies and setup required to get the application working.

## API Design

Based off of [Create a web API](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api) and [Web API with MongoDB](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app)

## Database 

### Sqlite Setup

Start from the `MyEventPresentations.Data.Sqlite` folder

``` 
dotnet ef migrations add InitialCreate --startup-project ../MyEventPresentations.Api
dotnet ef database update --startup-project ../MyEventPresentations.Api
```

### Microsoft SQL Server 

#### Database Creation

**Note** You can also use the [CreateDatabase-MSSQL.sql](CreateDatabase-MSSQL.sql) script.

**Note** You will need to replace the password for `MyEventPresentations_User`.

```tsql
CREATE DATABASE MyEventPresentations
ON (
    NAME=MyEventsPresentations_Data,
    FileNAME='C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\MyEventPresentations.mdf',
    Size=10,
    Maxsize=50,
    Filegrowth=5
)
LOG ON (
    NAME=MyEventsPresentations_Log,
    FileNAME='C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\MyEventPresentations.ldf',
    Size=5mb,
    Maxsize=25mb,
    Filegrowth=5mb
)

CREATE LOGIN MyEventPresentations_User WITH PASSWORD = '<Set Your Password>'
GO

USE MyEventPresentations
GO

CREATE User MyEventPresentations_User FOR login myconferenceevent_User
GO

exec sp_addrolemember 'db_datareader', 'MyEventPresentations_User'
GO

exec sp_addrolemember 'db_datawriter', 'MyEventPresentations_User'
GO
```

#### Table Creation

```tsql
create table Presentations
(
	PresentationId INT IDENTITY 
        constraint PK_Presentations
        PRIMARY KEY not null,
	Title VARCHAR(255) NULL,
	Abstract VARCHAR(4096) NULL,
	MoreInfoUri VARCHAR(512) NULL,
	SourceCodeRepositoryUri VARCHAR(512) NULL,
	PowerpointUri VARCHAR(512) NULL,
	VideoUri VARCHAR(512) NULL
);

create table ScheduledPresentations
(
	ScheduledPresentationId INT IDENTITY
        constraint PK_ScheduledPresentations
        primary key NOT NULL,
	PresentationId INTEGER
        constraint FK_ScheduledPresentations_Presentations_PresentationId
        references Presentations (PresentationId)
            on delete CASCADE,
	PresentationUri VARCHAR(512) NULL,
	VideoStorageUri VARCHAR(512) NULL,
	VideoUri VARCHAR(512) NULL,
	AttendeeCount INTEGER not null,
	EndTime DATETIME not null,
	StartTime DATETIME not null,
	RoomName VARCHAR(256) NULL
);

create index IX_ScheduledPresentations_PresentationId
	on ScheduledPresentations (PresentationId);

create table __EFMigrationsHistory
(
	MigrationId VARCHAR(256) not null
        constraint PK___EFMigrationsHistory
        primary key,
	ProductVersion VARCHAR(MAX) not null
);
```

## Azurite

Install [documentation](https://docs.microsoft.com/en-us/azure/storage/common/storage-use-azurite)

```bash
npm install -g azurite
```

Run Azurite

```bash
azurite --silent --location c:\azurite --debug c:\azurite\debug.log
```

*Optional*: Add the following to your `.gitignore`

```yaml
# Azurite
__blogstorage__/
__queuestorage__/
__azurite*.json
```

### Azurite Connections

To use locally, Azurite supports the well known [storage account and key](https://docs.microsoft.com/en-us/azure/storage/common/storage-use-azurite?toc=/azure/storage/blobs/toc.json#authorization-for-tools-and-sdks).

* Account name: `devstoreaccount1`
* Account key: `Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==`