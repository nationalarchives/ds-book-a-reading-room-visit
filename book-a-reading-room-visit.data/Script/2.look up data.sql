--[dbo].[OrderStatus]
INSERT INTO [dbo].[OrderStatus] ([Id], [Description]) VALUES (1, 'Created')
INSERT INTO [dbo].[OrderStatus] ([Id], [Description]) VALUES (2, 'Submitted')
INSERT INTO [dbo].[OrderStatus] ([Id], [Description]) VALUES (3, 'Cancelled')

--[dbo].[SeatType]
INSERT INTO [dbo].[SeatType] ([Id], [Description]) VALUES (1, 'Standard reading room seat')
INSERT INTO [dbo].[SeatType] ([Id], [Description]) VALUES (2, 'Standard reading room seat with camera stand')
INSERT INTO [dbo].[SeatType] ([Id], [Description]) VALUES (3, 'Map and large document room seat')
INSERT INTO [dbo].[SeatType] ([Id], [Description]) VALUES (2, 'Bulk document order seat')
INSERT INTO [dbo].[SeatType] ([Id], [Description]) VALUES (3, 'Not available')

--[dbo].[Seats]
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (1, '1A', 4)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (2, '2A', 4)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (3, '13A', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (4, '13C', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (5, '13E', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (6, '13G', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (7, '14E', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (8, '14H', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (9, '15D', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (10, '15H', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (11, '16D', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (12, '16H', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (13, '17D', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (14, '18D', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (15, '18H', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (16, '19A', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (17, '19C', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (18, '19E', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (19, '19G', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (20, '20A', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (21, '20C', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (22, '20E', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (23, '20G', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (24, '21B', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (25, '21F', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (26, '22H', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (27, '23B', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (28, '23F', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (29, '24B', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (30, '24F', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (31, '25B', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (32, '25D', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (33, '25F', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (34, '25H', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (35, '26B', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (36, '26D', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (37, '26F', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (38, '26H', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (39, '27B', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (40, '27F', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (41, '28B', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (42, '28F', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (43, '29D', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (44, '30D', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (45, '30G', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (46, '31B', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (47, '31F', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (48, '32B', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (49, '32F', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (50, '33B', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (51, '33D', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (52, '33F', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (53, '33H', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (54, '34B', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (55, '34D', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (56, '34F', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (57, '34H', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (58, '35B', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (59, '35F', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (60, '36B', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (61, '36F', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (62, '37D', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (63, '37G', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (64, '38H', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (65, '39B', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (66, '39F', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (67, '40B', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (68, '40F', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (69, '41B', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (70, '41F', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (71, '42B', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (72, '42F', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (73, 'MR001', 3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (74, 'MR002', 3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (75, 'MR003', 3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (76, 'MR004', 3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (77, 'MR005', 3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (78, 'MR006', 3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (79, 'MR007', 3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (80, 'MR008', 3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (81, 'MR009', 3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (82, 'MR010', 3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (83, 'MR011', 3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (84, 'MR012', 3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (85, 'MR013', 3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (86, 'MR014', 3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (87, 'MR015', 3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (88, 'MR016', 3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (89, 'MR017', 3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (90, 'MR018', 3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (91, 'MR019', 3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (92, 'MR020', 3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (93, 'MR021', 3)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (94, '43B', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (95, '43F', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (96, '44B', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (97, '44F', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (98, '45B', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (99, '45F', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (100 , '46D', 2)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (101 , '46H', 1)
INSERT INTO [dbo].[Seats] ([Id], [Number], [SeatTypeId]) VALUES (102 , '29G', 1)
