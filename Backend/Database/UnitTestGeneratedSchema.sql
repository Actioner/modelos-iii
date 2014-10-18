
    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK738691325ED79783]') AND parent_object_id = OBJECT_ID('Bins'))
alter table Bins  drop constraint FK738691325ED79783


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKDC04C449BCFEBE8E]') AND parent_object_id = OBJECT_ID('BinItems'))
alter table BinItems  drop constraint FKDC04C449BCFEBE8E


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKEA7BD955DBB349A]') AND parent_object_id = OBJECT_ID('Generations'))
alter table Generations  drop constraint FKEA7BD955DBB349A


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK2EA47F61D71EC2]') AND parent_object_id = OBJECT_ID('Items'))
alter table Items  drop constraint FK2EA47F61D71EC2


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKD0036E657BC1DAD9]') AND parent_object_id = OBJECT_ID('Populations'))
alter table Populations  drop constraint FKD0036E657BC1DAD9


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK738A9142D71EC2]') AND parent_object_id = OBJECT_ID('Runs'))
alter table Runs  drop constraint FK738A9142D71EC2


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK2C1C7FE5E754B2BE]') AND parent_object_id = OBJECT_ID('Users'))
alter table Users  drop constraint FK2C1C7FE5E754B2BE


    if exists (select * from dbo.sysobjects where id = object_id(N'Bins') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Bins

    if exists (select * from dbo.sysobjects where id = object_id(N'BinItems') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table BinItems

    if exists (select * from dbo.sysobjects where id = object_id(N'Generations') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Generations

    if exists (select * from dbo.sysobjects where id = object_id(N'Items') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Items

    if exists (select * from dbo.sysobjects where id = object_id(N'Populations') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Populations

    if exists (select * from dbo.sysobjects where id = object_id(N'Roles') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Roles

    if exists (select * from dbo.sysobjects where id = object_id(N'Runs') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Runs

    if exists (select * from dbo.sysobjects where id = object_id(N'Scenarios') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Scenarios

    if exists (select * from dbo.sysobjects where id = object_id(N'Users') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Users

    create table Bins (
        BinId INT IDENTITY NOT NULL,
       Filled REAL null,
       Capacity REAL null,
       PopulationId INT null,
       primary key (BinId)
    )

    create table BinItems (
        BinItemId INT IDENTITY NOT NULL,
       Label NVARCHAR(255) null,
       Size REAL null,
       BinId INT null,
       primary key (BinItemId)
    )

    create table Generations (
        GenerationId INT IDENTITY NOT NULL,
       Number INT null,
       RunId INT null,
       primary key (GenerationId)
    )

    create table Items (
        ItemId INT IDENTITY NOT NULL,
       Label NVARCHAR(255) null,
       Quantity INT null,
       Size REAL null,
       ScenarioId INT null,
       primary key (ItemId)
    )

    create table Populations (
        PopulationId INT IDENTITY NOT NULL,
       Number INT null,
       Fitness REAL null,
       BinCount INT null,
       GenerationId INT null,
       primary key (PopulationId)
    )

    create table Roles (
        RoleId INT IDENTITY NOT NULL,
       Name NVARCHAR(255) null,
       primary key (RoleId)
    )

    create table Runs (
        RunId INT IDENTITY NOT NULL,
       RunOn DATETIME null,
       ScenarioId INT null,
       primary key (RunId)
    )

    create table Scenarios (
        ScenarioId INT IDENTITY NOT NULL,
       Name NVARCHAR(255) null,
       BinSize REAL null,
       primary key (ScenarioId)
    )

    create table Users (
        UserId INT IDENTITY NOT NULL,
       Email NVARCHAR(255) null,
       Password NVARCHAR(255) null,
       Name NVARCHAR(255) null,
       Surname NVARCHAR(255) null,
       Enabled BIT null,
       RoleId INT null,
       primary key (UserId)
    )

    alter table Bins 
        add constraint FK738691325ED79783 
        foreign key (PopulationId) 
        references Populations

    alter table BinItems 
        add constraint FKDC04C449BCFEBE8E 
        foreign key (BinId) 
        references Bins

    alter table Generations 
        add constraint FKEA7BD955DBB349A 
        foreign key (RunId) 
        references Runs

    alter table Items 
        add constraint FK2EA47F61D71EC2 
        foreign key (ScenarioId) 
        references Scenarios

    alter table Populations 
        add constraint FKD0036E657BC1DAD9 
        foreign key (GenerationId) 
        references Generations

    alter table Runs 
        add constraint FK738A9142D71EC2 
        foreign key (ScenarioId) 
        references Scenarios

    alter table Users 
        add constraint FK2C1C7FE5E754B2BE 
        foreign key (RoleId) 
        references Roles
