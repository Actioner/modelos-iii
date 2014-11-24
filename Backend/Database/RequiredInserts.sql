USE [ModelosIII];

SET IDENTITY_INSERT [Roles] ON
INSERT INTO [Roles] ([RoleId],[Name])
VALUES(1,'Administrador'), (2,'Usuario');
SET IDENTITY_INSERT [Roles] OFF

SET IDENTITY_INSERT [Users] ON
INSERT INTO [Users] ([UserId],[Password],[Name],[Surname],[Enabled],[RoleId],[Email])
VALUES(1,'8993c89bafb53ae5e89eb81ff0454c18','Administrador','Administrador',1,1,'admin@sa.com');
SET IDENTITY_INSERT [Users] OFF

SET IDENTITY_INSERT [Scenarios] ON
INSERT INTO [Scenarios] ([ScenarioId],[Name],[BinSize])
VALUES(1,'Escenario 1',10),
(2,'Escenario 2',20);
SET IDENTITY_INSERT [Scenarios] OFF

SET IDENTITY_INSERT [Items] ON
INSERT INTO [Items] ([ItemId],[Label],[Quantity],[Size],[ScenarioId])
VALUES(1,'Item 1', 3, 3, 1), 
(2,'Item 2', 4, 4, 1), 
(3,'Item 3', 3, 5, 1),
(4,'Item 1', 10, 2.2, 2),
(5,'Item 2', 17, 4.8, 2),
(6,'Item 3', 3, 8.3, 2),
(7,'Item 4', 5, 10.2, 2);
SET IDENTITY_INSERT [Items] OFF