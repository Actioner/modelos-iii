
    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKD2CAA9BABAAA14E5]') AND parent_object_id = OBJECT_ID('Movies'))
alter table Movies  drop constraint FKD2CAA9BABAAA14E5


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK2101B4ACCBD90E78]') AND parent_object_id = OBJECT_ID('MovieGenres'))
alter table MovieGenres  drop constraint FK2101B4ACCBD90E78


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK1E4EC13C4A93E074]') AND parent_object_id = OBJECT_ID('PromotionDays'))
alter table PromotionDays  drop constraint FK1E4EC13C4A93E074


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKB404305DDE818DD0]') AND parent_object_id = OBJECT_ID('Reservations'))
alter table Reservations  drop constraint FKB404305DDE818DD0


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKB404305DD00399A6]') AND parent_object_id = OBJECT_ID('Reservations'))
alter table Reservations  drop constraint FKB404305DD00399A6


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK9B08B49F225DA580]') AND parent_object_id = OBJECT_ID('ReservationsToSeats'))
alter table ReservationsToSeats  drop constraint FK9B08B49F225DA580


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK9B08B49FFE8582E4]') AND parent_object_id = OBJECT_ID('ReservationsToSeats'))
alter table ReservationsToSeats  drop constraint FK9B08B49FFE8582E4


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKEC6105F6F8BC6B]') AND parent_object_id = OBJECT_ID('Rows'))
alter table Rows  drop constraint FKEC6105F6F8BC6B


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKEE5BB587D3D1C615]') AND parent_object_id = OBJECT_ID('Screens'))
alter table Screens  drop constraint FKEE5BB587D3D1C615


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKDE313C65F6F8BC6B]') AND parent_object_id = OBJECT_ID('Screenings'))
alter table Screenings  drop constraint FKDE313C65F6F8BC6B


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKDE313C65CBD90E78]') AND parent_object_id = OBJECT_ID('Screenings'))
alter table Screenings  drop constraint FKDE313C65CBD90E78


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK8862AD37AE019A4B]') AND parent_object_id = OBJECT_ID('Seats'))
alter table Seats  drop constraint FK8862AD37AE019A4B


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK2C1C7FE5C14BE346]') AND parent_object_id = OBJECT_ID('Users'))
alter table Users  drop constraint FK2C1C7FE5C14BE346


    if exists (select * from dbo.sysobjects where id = object_id(N'Movies') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Movies

    if exists (select * from dbo.sysobjects where id = object_id(N'MovieGenres') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MovieGenres

    if exists (select * from dbo.sysobjects where id = object_id(N'Multiplexes') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Multiplexes

    if exists (select * from dbo.sysobjects where id = object_id(N'Prices') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Prices

    if exists (select * from dbo.sysobjects where id = object_id(N'Promotions') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Promotions

    if exists (select * from dbo.sysobjects where id = object_id(N'PromotionDays') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table PromotionDays

    if exists (select * from dbo.sysobjects where id = object_id(N'Ratings') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Ratings

    if exists (select * from dbo.sysobjects where id = object_id(N'Reservations') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Reservations

    if exists (select * from dbo.sysobjects where id = object_id(N'ReservationsToSeats') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ReservationsToSeats

    if exists (select * from dbo.sysobjects where id = object_id(N'Roles') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Roles

    if exists (select * from dbo.sysobjects where id = object_id(N'Rows') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Rows

    if exists (select * from dbo.sysobjects where id = object_id(N'Screens') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Screens

    if exists (select * from dbo.sysobjects where id = object_id(N'Screenings') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Screenings

    if exists (select * from dbo.sysobjects where id = object_id(N'Seats') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Seats

    if exists (select * from dbo.sysobjects where id = object_id(N'Users') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Users

    create table Movies (
        MovieId INT IDENTITY NOT NULL,
       Title NVARCHAR(255) null,
       OriginalTitle NVARCHAR(255) null,
       YearOfRelease INT null,
       Director NVARCHAR(255) null,
       MainCast text null,
       Trailer NVARCHAR(255) null,
       Synopsis text null,
       SmallPoster NVARCHAR(255) null,
       Poster NVARCHAR(255) null,
       Runtime INT null,
       RatingId INT null,
       primary key (MovieId)
    )

    create table MovieGenres (
        MovieId INT not null,
       Genre INT null
    )

    create table Multiplexes (
        MultiplexId INT IDENTITY NOT NULL,
       Name NVARCHAR(255) null,
       Address NVARCHAR(255) null,
       City NVARCHAR(255) null,
       Subways NVARCHAR(255) null,
       Buses NVARCHAR(255) null,
       Poster NVARCHAR(255) null,
       Latitude DOUBLE PRECISION null,
       Longitude DOUBLE PRECISION null,
       primary key (MultiplexId)
    )

    create table Prices (
        PriceId INT IDENTITY NOT NULL,
       Value DOUBLE PRECISION null,
       Type NVARCHAR(255) null,
       primary key (PriceId)
    )

    create table Promotions (
        PromotionId INT IDENTITY NOT NULL,
       Name NVARCHAR(255) null,
       Description NVARCHAR(255) null,
       Value NVARCHAR(255) null,
       Poster NVARCHAR(255) null,
       StartDate DATETIME null,
       EndDate DATETIME null,
       Type NVARCHAR(255) null,
       Active BIT null,
       primary key (PromotionId)
    )

    create table PromotionDays (
        PromotionId INT not null,
       DayOfWeek INT null
    )

    create table Ratings (
        RatingId INT IDENTITY NOT NULL,
       Name NVARCHAR(255) null,
       Description NVARCHAR(255) null,
       primary key (RatingId)
    )

    create table Reservations (
        ReservationId INT IDENTITY NOT NULL,
       Code NVARCHAR(255) null,
       Promotion NVARCHAR(255) null,
       Total DOUBLE PRECISION null,
       Time DATETIME null,
       ReservationStatus NVARCHAR(255) null,
       ReservationPaymentStatus NVARCHAR(255) null,
       UserId INT null,
       ScreeningId INT null,
       primary key (ReservationId)
    )

    create table ReservationsToSeats (
        ReservationId INT not null,
       SeatId INT not null
    )

    create table Roles (
        RoleId INT IDENTITY NOT NULL,
       Name NVARCHAR(255) null,
       primary key (RoleId)
    )

    create table Rows (
        RowId INT IDENTITY NOT NULL,
       Name NVARCHAR(255) null,
       ScreenId INT null,
       primary key (RowId)
    )

    create table Screens (
        ScreenId INT IDENTITY NOT NULL,
       Name NVARCHAR(255) null,
       Capacity INT null,
       MultiplexId INT null,
       primary key (ScreenId)
    )

    create table Screenings (
        ScreeningId INT IDENTITY NOT NULL,
       StartDate DATETIME null,
       ScreenId INT null,
       MovieId INT null,
       primary key (ScreeningId)
    )

    create table Seats (
        SeatId INT IDENTITY NOT NULL,
       Number INT null,
       RowId INT null,
       primary key (SeatId)
    )

    create table Users (
        UserId INT IDENTITY NOT NULL,
       Email NVARCHAR(255) null,
       Password NVARCHAR(255) null,
       Name NVARCHAR(255) null,
       Surname NVARCHAR(255) null,
       Phone NVARCHAR(255) null,
       Address NVARCHAR(255) null,
       Document NVARCHAR(255) null,
       Hash NVARCHAR(255) null,
       Enabled BIT null,
       Validated BIT null,
       BirthDate DATETIME null,
       RoleId INT null,
       primary key (UserId)
    )

    alter table Movies 
        add constraint FKD2CAA9BABAAA14E5 
        foreign key (RatingId) 
        references Ratings

    alter table MovieGenres 
        add constraint FK2101B4ACCBD90E78 
        foreign key (MovieId) 
        references Movies

    alter table PromotionDays 
        add constraint FK1E4EC13C4A93E074 
        foreign key (PromotionId) 
        references Promotions

    alter table Reservations 
        add constraint FKB404305DDE818DD0 
        foreign key (UserId) 
        references Users

    alter table Reservations 
        add constraint FKB404305DD00399A6 
        foreign key (ScreeningId) 
        references Screenings

    alter table ReservationsToSeats 
        add constraint FK9B08B49F225DA580 
        foreign key (SeatId) 
        references Seats

    alter table ReservationsToSeats 
        add constraint FK9B08B49FFE8582E4 
        foreign key (ReservationId) 
        references Reservations

    alter table Rows 
        add constraint FKEC6105F6F8BC6B 
        foreign key (ScreenId) 
        references Screens

    alter table Screens 
        add constraint FKEE5BB587D3D1C615 
        foreign key (MultiplexId) 
        references Multiplexes

    alter table Screenings 
        add constraint FKDE313C65F6F8BC6B 
        foreign key (ScreenId) 
        references Screens

    alter table Screenings 
        add constraint FKDE313C65CBD90E78 
        foreign key (MovieId) 
        references Movies

    alter table Seats 
        add constraint FK8862AD37AE019A4B 
        foreign key (RowId) 
        references Rows

    alter table Users 
        add constraint FK2C1C7FE5C14BE346 
        foreign key (RoleId) 
        references Roles
