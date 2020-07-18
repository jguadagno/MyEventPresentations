-- Need to assign a password for the MyEventPresentations_User. Look for `CREATE LOGIN`

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

CREATE LOGIN MyEventPresentations_User WITH PASSWORD = ''
GO

USE MyEventPresentations
GO

CREATE User MyEventPresentations_User FOR login myconferenceevent_User
GO

exec sp_addrolemember 'db_datareader', 'MyEventPresentations_User'
GO

exec sp_addrolemember 'db_datawriter', 'MyEventPresentations_User'
GO

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

