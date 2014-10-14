USE [CinemAr];

SET IDENTITY_INSERT [Roles] ON
INSERT INTO [Roles] ([RoleId],[Name])
VALUES(1,'Administrador'), (2,'Usuario');
SET IDENTITY_INSERT [Roles] OFF

SET IDENTITY_INSERT [Users] ON
INSERT INTO [Users] ([UserId],[Password],[Name],[Surname],[Enabled],[RoleId],[Email])
VALUES(1,'8993c89bafb53ae5e89eb81ff0454c18','Administrador','Administrador',1,1,'admin@sa.com');
SET IDENTITY_INSERT [Users] OFF

SET IDENTITY_INSERT [Multiplexes] ON
INSERT INTO [Multiplexes] ([MultiplexId],[Name],[Address],[City],[Subways],[Buses],[Latitude],[Longitude],[Poster])
VALUES(1,'Cinemar 1','Avenida Callao 1310', 'Capital Federal','A, C', '15, 59', -34.59373, -58.39307,'/uploads/posters/Cinemar1.jpg'),
(2,'Cinemar 2','Avenida Santa Fe 1333', 'Capital Federal','B, D', '152, 168',-34.59563, -58.38628,'/uploads/posters/Cinemar2.jpg');
SET IDENTITY_INSERT [Multiplexes] OFF

SET IDENTITY_INSERT [Screens] ON
INSERT INTO [Screens] ([ScreenId],[Name],[Capacity],[MultiplexId])
VALUES(1,'Sala 1', 120, 1), 
(2,'Sala 2', 220, 1), 
(3,'Sala 3', 330, 1),
(4,'Sala 1', 100, 2),
(5,'Sala 2', 85, 2),
(6,'Sala 3', 115, 2),
(7,'Sala 4', 120, 2);
SET IDENTITY_INSERT [Screens] OFF

SET IDENTITY_INSERT [Rows] ON
INSERT INTO [Rows] ([RowId],[Name],[ScreenId])
VALUES(1,'A', 1),(2,'B', 1),(3,'C', 1),(4,'D', 1),(5,'E', 1),
(6,'A', 2),(7,'B', 2),(8,'C', 2),(9,'D', 2),(10,'E', 2),
(11,'A', 3),(12,'B', 3),(13,'C', 3),(14,'D', 3),(15,'E', 3),
(16,'A', 4),(17,'B', 4),(18,'C', 4),(19,'D', 4),(20,'E', 4),
(21,'A', 5),(22,'B', 5),(23,'C', 5),(24,'D', 5),(25,'E', 5),
(26,'A', 6),(27,'B', 6),(28,'C', 6),(29,'D', 6),(30,'E', 6),
(31,'A', 7),(32,'B', 7),(33,'C', 7),(34,'D', 7),(35,'E', 7);
SET IDENTITY_INSERT [Rows] OFF

SET IDENTITY_INSERT [Seats] ON
INSERT INTO [Seats] ([SeatId],[Number],[RowId])
VALUES (1,1,1),	 (2,2,1),	 (3,3,1),	 (4,4,1),	 (5,5,1),	 (6,6,1),	 (7,7,1),	 (8,8,1),	 (9,9,1),	 (10,10,1),	 (11,11,1),	 (12,12,1),	 (13,13,1),	 (14,14,1),	 (15,15,1),	 (16,16,1),	 (17,17,1),	 (18,18,1),	 (19,19,1),	 (20,20,1),	 (21,21,1),	 (22,22,1),
 (23,1,2),	 (24,2,2),	 (25,3,2),	 (26,4,2),	 (27,5,2),	 (28,6,2),	 (29,7,2),	 (30,8,2),	 (31,9,2),	 (32,10,2),	 (33,11,2),	 (34,12,2),	 (35,13,2),	 (36,14,2),	 (37,15,2),	 (38,16,2),	 (39,17,2),	 (40,18,2),	 (41,19,2),	 (42,20,2),	 (43,21,2),	 (44,22,2),
 (45,1,3),	 (46,2,3),	 (47,3,3),	 (48,4,3),	 (49,5,3),	 (50,6,3),	 (51,7,3),	 (52,8,3),	 (53,9,3),	 (54,10,3),	 (59,15,3),	 (60,16,3),	 (61,17,3),	 (62,18,3),	 (63,19,3),	 (64,20,3),	 (65,21,3),	 (66,22,3),				
 (89,1,5),	 (94,6,5),	 (95,7,5),	 (96,8,5),	 (97,9,5),	 (98,10,5),	 (103,15,5),	 (104,16,5),	 (105,17,5),													
 (111,1,6),	 (116,6,6),	 (117,7,6),	 (118,8,6),	 (119,9,6),	 (120,10,6),	 (125,15,6),	 (126,16,6),	 (127,17,6),													
 (133,1,7),	 (138,6,7),	 (139,7,7),	 (140,8,7),	 (141,9,7),	 (142,10,7),	 (147,15,7),	 (148,16,7),	 (149,17,7),													
 (155,1,8),	 (160,6,8),	 (161,7,8),	 (162,8,8),	 (163,9,8),	 (164,10,8),	 (169,15,8),	 (170,16,8),	 (171,17,8),													
 (177,1,9),	 (182,6,9),	 (183,7,9),	 (184,8,9),	 (185,9,9),	 (186,10,9),	 (191,15,9),	 (192,16,9),	 (193,17,9),													
 (199,1,10),	 (204,6,10),	 (205,7,10),	 (206,8,10),	 (207,9,10),	 (208,10,10),	 (213,15,10),	 (214,16,10),	 (215,17,10),													
 (243,1,12),	 (248,6,12),	 (249,7,12),	 (250,8,12),	 (251,9,12),	 (252,10,12),	 (257,15,12),	 (258,16,12),	 (259,17,12),													
 (265,1,13),	 (270,6,13),	 (271,7,13),	 (272,8,13),	 (273,9,13),	 (274,10,13),	 (275,11,13),	 (276,12,13),	 (277,13,13),	 (278,14,13),	 (279,15,13),	 (280,16,13),	 (281,17,13),									
 (287,1,14),	 (292,6,14),	 (293,7,14),	 (294,8,14),	 (295,9,14),	 (296,10,14),	 (297,11,14),	 (298,12,14),	 (299,13,14),	 (300,14,14),	 (301,15,14),	 (302,16,14),	 (303,17,14),									
 (309,1,15),	 (314,6,15),	 (315,7,15),	 (316,8,15),	 (317,9,15),	 (318,10,15),	 (319,11,15),	 (320,12,15),	 (321,13,15),	 (322,14,15),	 (323,15,15),	 (324,16,15),	 (325,17,15),									
 (331,1,16),	 (332,2,16),	 (333,3,16),	 (334,4,16),	 (335,5,16),	 (336,6,16),	 (337,7,16),	 (338,8,16),	 (339,9,16),	 (340,10,16),	 (341,11,16),	 (342,12,16),	 (343,13,16),	 (344,14,16),	 (345,15,16),	 (346,16,16),	 (347,17,16),					
 (353,1,17),	 (354,2,17),	 (355,3,17),	 (356,4,17),	 (357,5,17),	 (358,6,17),	 (359,7,17),	 (364,12,17),	 (365,13,17),	 (366,14,17),	 (367,15,17),	 (368,16,17),	 (369,17,17),									
 (375,1,18),	 (376,2,18),	 (377,3,18),	 (378,4,18),	 (379,5,18),	 (380,6,18),	 (381,7,18),	 (386,12,18),	 (387,13,18),	 (388,14,18),	 (389,15,18),	 (390,16,18),	 (391,17,18),	 (392,18,18),	 (393,19,18),	 (394,20,18),	 (395,21,18),	 (396,22,18),				
 (402,6,19),	 (403,7,19),	 (408,12,19),	 (409,13,19),	 (410,14,19),	 (411,15,19),	 (412,16,19),	 (413,17,19),	 (414,18,19),	 (415,19,19),	 (416,20,19),	 (417,21,19),	 (418,22,19),									
 (446,6,21),	 (447,7,21),	 (452,12,21),	 (453,13,21),	 (454,14,21),	 (455,15,21),	 (456,16,21),	 (457,17,21),	 (458,18,21),	 (459,19,21),	 (460,20,21),	 (461,21,21),	 (462,22,21),									
 (468,6,22),	 (469,7,22),	 (474,12,22),	 (475,13,22),	 (476,14,22),	 (477,15,22),	 (478,16,22),	 (479,17,22),	 (480,18,22),	 (481,19,22),	 (482,20,22),	 (483,21,22),	 (484,22,22),									
 (490,6,23),	 (491,7,23),	 (496,12,23),	 (497,13,23),	 (498,14,23),	 (499,15,23),	 (505,21,23),	 (506,22,23),														
 (512,6,24),	 (513,7,24),	 (518,12,24),	 (519,13,24),	 (520,14,24),	 (521,15,24),	 (527,21,24),	 (528,22,24),														
 (534,6,25),	 (535,7,25),	 (540,12,25),	 (541,13,25),	 (542,14,25),	 (543,15,25),	 (549,21,25),	 (550,22,25),														
 (578,6,27),	 (579,7,27),	 (584,12,27),	 (585,13,27),	 (586,14,27),	 (587,15,27),	 (593,21,27),	 (594,22,27),														
 (600,6,28),	 (601,7,28),	 (606,12,28),	 (607,13,28),	 (608,14,28),	 (609,15,28),	 (615,21,28),	 (616,22,28),														
 (622,6,29),	 (623,7,29),	 (628,12,29),	 (629,13,29),	 (630,14,29),	 (631,15,29),	 (637,21,29),	 (638,22,29),														
 (644,6,30),	 (645,7,30),	 (650,12,30),	 (651,13,30),	 (652,14,30),	 (653,15,30),	 (659,21,30),	 (660,22,30),														
 (688,6,32),	 (689,7,32),	 (690,8,32),	 (691,9,32),	 (692,10,32),	 (693,11,32),	 (694,12,32),	 (695,13,32),	 (696,14,32),	 (697,15,32),	 (703,21,32),	 (704,22,32),										
 (710,6,33),	 (711,7,33),	 (712,8,33),	 (713,9,33),	 (714,10,33),	 (715,11,33),	 (716,12,33),	 (717,13,33),	 (718,14,33),	 (719,15,33),	 (720,16,33),	 (721,17,33),	 (722,18,33),	 (723,19,33),	 (724,20,33),	 (725,21,33),	 (726,22,33),					
 (732,6,34),	 (733,7,34),	 (734,8,34),	 (735,9,34),	 (736,10,34),	 (737,11,34),	 (738,12,34),	 (739,13,34),	 (740,14,34),	 (741,15,34),	 (742,16,34),	 (743,17,34),	 (744,18,34),	 (745,19,34),	 (746,20,34),	 (747,21,34),	 (748,22,34),					
 (749,1,35),	 (750,2,35),	 (751,3,35),	 (752,4,35),	 (753,5,35),	 (754,6,35),	 (755,7,35),	 (756,8,35),	 (757,9,35),	 (758,10,35),	 (759,11,35),	 (760,12,35),	 (761,13,35),	 (762,14,35),	 (763,15,35),	 (764,16,35),	 (765,17,35),	 (766,18,35),	 (767,19,35),	 (768,20,35),	 (769,21,35),	 (770,22,35);
SET IDENTITY_INSERT [Seats] OFF
SET IDENTITY_INSERT [Seats] OFF

SET IDENTITY_INSERT [Ratings] ON
INSERT INTO [Ratings] ([RatingId],[Name],[Description])
VALUES(1,'ATP','Sin violencia, infantil, lenguaje seguro'),
(2,'AM5','Un poco de sangre, comedia especial'),
(3,'AM9','Lenguaje adulto, sangre, violencia'),
(4,'AM13','Violencia, alcohol, sangre, lenguaje no tan moderado'),
(5,'AM16','Drogas, muertes, narcotrafico'),
(6,'AM18','Guerra, sexo, drogas, sangre, lenguaje extremo, no se permiten menores de edad');
SET IDENTITY_INSERT [Ratings] OFF

SET IDENTITY_INSERT [Movies] ON
INSERT INTO [Movies]([MovieId],[Title],[OriginalTitle],[YearOfRelease],[Director],[MainCast],[Trailer],[Synopsis],[Runtime],[SmallPoster],[Poster],[RatingId]) 
VALUES (1,'La guerra de las galaxias','Star Wars',1977,'George Lucas','Mark Hamill, Harrison Ford, Carrie Fisher','http://youtu.be/9gvqpFbRKtQ','Luke Skywalker joins forces with a Jedi Knight, a cocky pilot, a wookiee and two droids to save the universe from the Empires world-destroying battle-station, while also attempting to rescue Princess Leia from the evil Darth Vader.',121,'/uploads/posters/starwarssmall.jpg','/uploads/posters/starwars.jpg',4),
 (2,'Volver al futuro','Back to the Future',1985,'Robert Zemeckis','Michael J. Fox, Christopher Lloyd, Lea Thompson','http://youtu.be/yosuvf7Unmg','A teenager is accidentally sent 30 years into the past in a time-traveling DeLorean invented by his friend, Dr. Emmett Brown, and must make sure his high-school-age parents unite in order to save his own existence.',116,'/uploads/posters/backtothefuturesmall.jpg','/uploads/posters/backtothefuture.jpg',1);
SET IDENTITY_INSERT [Movies] OFF

SET IDENTITY_INSERT [Screenings] ON
INSERT INTO [Screenings] ([ScreeningId],[StartDate],[ScreenId],[MovieId])
VALUES(1,'2013-09-26 18:05:00',1,1),
(2,'2013-09-26 20:40:00',2,1),
(3,'2013-09-27 20:25:00',3,2),
(4,'2013-09-28 19:15:00',6,2),
(5,'2013-09-28 21:15:00',6,2),
(6,'2013-09-28 19:10:00',5,2),
(7,'2013-09-28 19:15:00',6,2),
(8,'2013-09-29 22:15:00',6,2),
(9,'2013-09-29 15:10:00',6,2),
(10,'2013-09-29 19:10:00',5,2),
(11,'2013-09-29 19:15:00',6,2);
SET IDENTITY_INSERT [Screenings] OFF

INSERT INTO [MovieGenres] ([MovieId],[Genre])
VALUES(1,1),
(1,2),
(1,10),
(1,16),
(2,2),
(2,5),
(2,16);

INSERT INTO [Prices] ([Value],[Type])
VALUES (40, 'General');

INSERT INTO [Promotions]([Name],[Description],[Value],[StartDate],[EndDate],[Type],[Active],[Poster])
VALUES ('Jubilados','Lunes a miércoles descuento para Jubilados', '20', '2013-09-23 00:00:000',  '2013-12-31 00:00:000', 'FixedPrice', 1, null),
('La Nación 2x1','Miércoles a Viernes 2x1 con La Nación', '2x1', '2013-10-10 00:00:000',  '2013-12-31 00:00:000', 'NxM', 1, '/uploads/posters/lanacion.png'),
('Finde','25% descuento los findes de semana', '25', '2013-09-23 00:00:000',  '2013-12-31 00:00:000', 'Percent', 1, null);

INSERT INTO [PromotionDays] ([PromotionId],[DayOfWeek])
VALUES(1,1),
(1,2),
(1,3),
(2,3),
(2,4),
(2,5),
(3,6),
(3,0);
