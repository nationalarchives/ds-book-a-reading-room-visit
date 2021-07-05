--[dbo].[OrderStatus]
INSERT INTO [dbo].[BookingStatus] ([Id], [Description]) VALUES (1, 'Created')
INSERT INTO [dbo].[BookingStatus] ([Id], [Description]) VALUES (2, 'Submitted')
INSERT INTO [dbo].[BookingStatus] ([Id], [Description]) VALUES (3, 'Cancelled')

--[dbo].[SeatType]
INSERT INTO [dbo].[SeatType] ([Id], [Description]) VALUES (1, 'Standard reading room seat')
INSERT INTO [dbo].[SeatType] ([Id], [Description]) VALUES (2, 'Standard reading room seat with camera stand')
INSERT INTO [dbo].[SeatType] ([Id], [Description]) VALUES (3, 'Map and large document room seat')
INSERT INTO [dbo].[SeatType] ([Id], [Description]) VALUES (4, 'Bulk document order seat')
INSERT INTO [dbo].[SeatType] ([Id], [Description]) VALUES (5, 'Map and large document room seat with camera stand')
INSERT INTO [dbo].[SeatType] ([Id], [Description]) VALUES (6, 'Bulk document order seat with camera stand')
INSERT INTO [dbo].[SeatType] ([Id], [Description]) VALUES (7, 'Not available')
INSERT INTO [dbo].[SeatType] ([Id], [Description]) VALUES (8, 'Managerial Discretion')
INSERT INTO [dbo].[SeatType] ([Id], [Description]) VALUES (8, 'Multi day visit standard reading room seat')

--[dbo].[BookingType]
INSERT INTO [dbo].[BookingType] ([Id], [Description]) VALUES (1, 'Standard order visit')
INSERT INTO [dbo].[BookingType] ([Id], [Description]) VALUES (2, 'Bulk order visit')
INSERT INTO [dbo].[BookingType] ([Id], [Description]) VALUES (3, 'Computer use visit')

--[dbo].[Seats]
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (1, '14H', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (2, '15D', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (3, '15H', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (4, '16D', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (5, '16H', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (6, '17D', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (7, '18D', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (8, '18H', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (9, '27B', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (10, '27F',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (11, '28B',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (12, '28F',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (13, '29D',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (14, '30D',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (15, '30G',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (16, '31B',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (17, '31F',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (18, '32B',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (19, '32F',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (20, '21B',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (21, '21F',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (22, '22H',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (23, '23B',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (24, '23F',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (25, '24B',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (26, '24F',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (27, '35B',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (28, '39B',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (29, '39F',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (30, '40B',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (31, '40F',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (32, '41B',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (33, '41F',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (34, '42B',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (35, '42F',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (36, '36B',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (37, '36F',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (38, '37D',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (39, '43B',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (40, '43F',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (41, '44F',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (42, '45F',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (43, '46H',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (44, '29G',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (45, '46D',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (46, '45B',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (47, '44B',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (48, '37G',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (49, '38H',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (50, '35F',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (51, '13A',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (52, '13C',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (53, '13E',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (54, '13G',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (55, '14E',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (56, '25B',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (57, '25D',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (58, '25F',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (59, '25H',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (60, '26B',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (61, '26D',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (62, '26F',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (63, '26H',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (64, '33B',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (65, '33D',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (66, '33F',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (67, '33H',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (68, '34B',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (69, '34D',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (70, '34F',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (71, '34H',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (72, '19A',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (73, '19C',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (74, '19E',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (75, '19G',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (76, '20A',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (77, '20C',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (78, '20E',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (79, '20G',	1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (80, 'MR001' ,3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (81, 'MR002' ,3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (82, 'MR003' ,3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (83, 'MR004' ,3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (84, 'MR005' ,3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (85, 'MR006' ,3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (86, 'MR007' ,3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (87, 'MR008' ,3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (88, 'MR009' ,3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (89, 'MR010' ,3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (90, 'MR011' ,3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (91, 'MR012' ,3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (92, 'MR013' ,3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (93, 'MR014' ,3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (94, 'MR015' ,3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (95, 'MR016' ,3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (96, 'MR017' ,3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (97, 'MR018' ,3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (98, 'MR019' ,3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (99, 'MR020' ,3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (100, 'Bulk1', 4)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (101, 'Bulk2', 4)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (102, 'Bulk3', 4)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (103, 'Bulk4', 4)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (104, 'MD1', 8)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (105, 'MD2', 8)
