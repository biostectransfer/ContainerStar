






	SET IDENTITY_INSERT [ContainerStar].[dbo].[PERMISSION] ON;
	
	IF NOT EXISTS (SELECT ID FROM [ContainerStar].[dbo].[PERMISSION] WHERE ID = 11 )
	BEGIN
		INSERT INTO [ContainerStar].[dbo].[PERMISSION] ([Id], [Name], [Description], [CreateDate], [ChangeDate], [DeleteDate])
		VALUES(11, 'Orders', 'Auftrag' ,GETDATE() ,GETDATE() ,NULL);
		INSERT INTO [ContainerStar].dbo.ROLE_PERMISSION_RSP(RoleId ,PermissionId ,CreateDate ,ChangeDate) 
		VALUES (1 ,11 ,getdate() ,getdate());
	END
	ELSE
	BEGIN
		UPDATE [ContainerStar].[dbo].[PERMISSION]
		SET [DESCRIPTION] = 'Auftrag'
		WHERE ID = 11
	END
	
	IF NOT EXISTS (SELECT ID FROM [ContainerStar].[dbo].[PERMISSION] WHERE ID = 12 )
	BEGIN
		INSERT INTO [ContainerStar].[dbo].[PERMISSION] ([Id], [Name], [Description], [CreateDate], [ChangeDate], [DeleteDate])
		VALUES(12, 'Invoices', 'Rechnung' ,GETDATE() ,GETDATE() ,NULL);
		INSERT INTO [ContainerStar].dbo.ROLE_PERMISSION_RSP(RoleId ,PermissionId ,CreateDate ,ChangeDate) 
		VALUES (1 ,12 ,getdate() ,getdate());
	END
	ELSE
	BEGIN
		UPDATE [ContainerStar].[dbo].[PERMISSION]
		SET [DESCRIPTION] = 'Rechnung'
		WHERE ID = 12
	END
	
	IF NOT EXISTS (SELECT ID FROM [ContainerStar].[dbo].[PERMISSION] WHERE ID = 1 )
	BEGIN
		INSERT INTO [ContainerStar].[dbo].[PERMISSION] ([Id], [Name], [Description], [CreateDate], [ChangeDate], [DeleteDate])
		VALUES(1, 'Permission', 'Berechtigung' ,GETDATE() ,GETDATE() ,NULL);
		INSERT INTO [ContainerStar].dbo.ROLE_PERMISSION_RSP(RoleId ,PermissionId ,CreateDate ,ChangeDate) 
		VALUES (1 ,1 ,getdate() ,getdate());
	END
	ELSE
	BEGIN
		UPDATE [ContainerStar].[dbo].[PERMISSION]
		SET [DESCRIPTION] = 'Berechtigung'
		WHERE ID = 1
	END
	
	IF NOT EXISTS (SELECT ID FROM [ContainerStar].[dbo].[PERMISSION] WHERE ID = 2 )
	BEGIN
		INSERT INTO [ContainerStar].[dbo].[PERMISSION] ([Id], [Name], [Description], [CreateDate], [ChangeDate], [DeleteDate])
		VALUES(2, 'Role', 'Rolle' ,GETDATE() ,GETDATE() ,NULL);
		INSERT INTO [ContainerStar].dbo.ROLE_PERMISSION_RSP(RoleId ,PermissionId ,CreateDate ,ChangeDate) 
		VALUES (1 ,2 ,getdate() ,getdate());
	END
	ELSE
	BEGIN
		UPDATE [ContainerStar].[dbo].[PERMISSION]
		SET [DESCRIPTION] = 'Rolle'
		WHERE ID = 2
	END
	
	IF NOT EXISTS (SELECT ID FROM [ContainerStar].[dbo].[PERMISSION] WHERE ID = 13 )
	BEGIN
		INSERT INTO [ContainerStar].[dbo].[PERMISSION] ([Id], [Name], [Description], [CreateDate], [ChangeDate], [DeleteDate])
		VALUES(13, 'InvoiceStornos', 'Storno Rechnung' ,GETDATE() ,GETDATE() ,NULL);
		INSERT INTO [ContainerStar].dbo.ROLE_PERMISSION_RSP(RoleId ,PermissionId ,CreateDate ,ChangeDate) 
		VALUES (1 ,13 ,getdate() ,getdate());
	END
	ELSE
	BEGIN
		UPDATE [ContainerStar].[dbo].[PERMISSION]
		SET [DESCRIPTION] = 'Storno Rechnung'
		WHERE ID = 13
	END
	
	IF NOT EXISTS (SELECT ID FROM [ContainerStar].[dbo].[PERMISSION] WHERE ID = 3 )
	BEGIN
		INSERT INTO [ContainerStar].[dbo].[PERMISSION] ([Id], [Name], [Description], [CreateDate], [ChangeDate], [DeleteDate])
		VALUES(3, 'User', 'Benutzer' ,GETDATE() ,GETDATE() ,NULL);
		INSERT INTO [ContainerStar].dbo.ROLE_PERMISSION_RSP(RoleId ,PermissionId ,CreateDate ,ChangeDate) 
		VALUES (1 ,3 ,getdate() ,getdate());
	END
	ELSE
	BEGIN
		UPDATE [ContainerStar].[dbo].[PERMISSION]
		SET [DESCRIPTION] = 'Benutzer'
		WHERE ID = 3
	END
	
	IF NOT EXISTS (SELECT ID FROM [ContainerStar].[dbo].[PERMISSION] WHERE ID = 4 )
	BEGIN
		INSERT INTO [ContainerStar].[dbo].[PERMISSION] ([Id], [Name], [Description], [CreateDate], [ChangeDate], [DeleteDate])
		VALUES(4, 'Equipments', 'Ausstattung' ,GETDATE() ,GETDATE() ,NULL);
		INSERT INTO [ContainerStar].dbo.ROLE_PERMISSION_RSP(RoleId ,PermissionId ,CreateDate ,ChangeDate) 
		VALUES (1 ,4 ,getdate() ,getdate());
	END
	ELSE
	BEGIN
		UPDATE [ContainerStar].[dbo].[PERMISSION]
		SET [DESCRIPTION] = 'Ausstattung'
		WHERE ID = 4
	END
	
	IF NOT EXISTS (SELECT ID FROM [ContainerStar].[dbo].[PERMISSION] WHERE ID = 5 )
	BEGIN
		INSERT INTO [ContainerStar].[dbo].[PERMISSION] ([Id], [Name], [Description], [CreateDate], [ChangeDate], [DeleteDate])
		VALUES(5, 'AdditionalCosts', 'Nebenkosten' ,GETDATE() ,GETDATE() ,NULL);
		INSERT INTO [ContainerStar].dbo.ROLE_PERMISSION_RSP(RoleId ,PermissionId ,CreateDate ,ChangeDate) 
		VALUES (1 ,5 ,getdate() ,getdate());
	END
	ELSE
	BEGIN
		UPDATE [ContainerStar].[dbo].[PERMISSION]
		SET [DESCRIPTION] = 'Nebenkosten'
		WHERE ID = 5
	END
	
	IF NOT EXISTS (SELECT ID FROM [ContainerStar].[dbo].[PERMISSION] WHERE ID = 6 )
	BEGIN
		INSERT INTO [ContainerStar].[dbo].[PERMISSION] ([Id], [Name], [Description], [CreateDate], [ChangeDate], [DeleteDate])
		VALUES(6, 'Taxes', 'Umsatzsteuer' ,GETDATE() ,GETDATE() ,NULL);
		INSERT INTO [ContainerStar].dbo.ROLE_PERMISSION_RSP(RoleId ,PermissionId ,CreateDate ,ChangeDate) 
		VALUES (1 ,6 ,getdate() ,getdate());
	END
	ELSE
	BEGIN
		UPDATE [ContainerStar].[dbo].[PERMISSION]
		SET [DESCRIPTION] = 'Umsatzsteuer'
		WHERE ID = 6
	END
	
	IF NOT EXISTS (SELECT ID FROM [ContainerStar].[dbo].[PERMISSION] WHERE ID = 7 )
	BEGIN
		INSERT INTO [ContainerStar].[dbo].[PERMISSION] ([Id], [Name], [Description], [CreateDate], [ChangeDate], [DeleteDate])
		VALUES(7, 'TransportProducts', 'Transport-Leistung' ,GETDATE() ,GETDATE() ,NULL);
		INSERT INTO [ContainerStar].dbo.ROLE_PERMISSION_RSP(RoleId ,PermissionId ,CreateDate ,ChangeDate) 
		VALUES (1 ,7 ,getdate() ,getdate());
	END
	ELSE
	BEGIN
		UPDATE [ContainerStar].[dbo].[PERMISSION]
		SET [DESCRIPTION] = 'Transport-Leistung'
		WHERE ID = 7
	END
	
	IF NOT EXISTS (SELECT ID FROM [ContainerStar].[dbo].[PERMISSION] WHERE ID = 8 )
	BEGIN
		INSERT INTO [ContainerStar].[dbo].[PERMISSION] ([Id], [Name], [Description], [CreateDate], [ChangeDate], [DeleteDate])
		VALUES(8, 'Customers', 'Kunde' ,GETDATE() ,GETDATE() ,NULL);
		INSERT INTO [ContainerStar].dbo.ROLE_PERMISSION_RSP(RoleId ,PermissionId ,CreateDate ,ChangeDate) 
		VALUES (1 ,8 ,getdate() ,getdate());
	END
	ELSE
	BEGIN
		UPDATE [ContainerStar].[dbo].[PERMISSION]
		SET [DESCRIPTION] = 'Kunde'
		WHERE ID = 8
	END
	
	IF NOT EXISTS (SELECT ID FROM [ContainerStar].[dbo].[PERMISSION] WHERE ID = 14 )
	BEGIN
		INSERT INTO [ContainerStar].[dbo].[PERMISSION] ([Id], [Name], [Description], [CreateDate], [ChangeDate], [DeleteDate])
		VALUES(14, 'TransportOrders', 'Transport Auftrag' ,GETDATE() ,GETDATE() ,NULL);
		INSERT INTO [ContainerStar].dbo.ROLE_PERMISSION_RSP(RoleId ,PermissionId ,CreateDate ,ChangeDate) 
		VALUES (1 ,14 ,getdate() ,getdate());
	END
	ELSE
	BEGIN
		UPDATE [ContainerStar].[dbo].[PERMISSION]
		SET [DESCRIPTION] = 'Transport Auftrag'
		WHERE ID = 14
	END
	
	IF NOT EXISTS (SELECT ID FROM [ContainerStar].[dbo].[PERMISSION] WHERE ID = 9 )
	BEGIN
		INSERT INTO [ContainerStar].[dbo].[PERMISSION] ([Id], [Name], [Description], [CreateDate], [ChangeDate], [DeleteDate])
		VALUES(9, 'ContainerTypes', 'Ausführung' ,GETDATE() ,GETDATE() ,NULL);
		INSERT INTO [ContainerStar].dbo.ROLE_PERMISSION_RSP(RoleId ,PermissionId ,CreateDate ,ChangeDate) 
		VALUES (1 ,9 ,getdate() ,getdate());
	END
	ELSE
	BEGIN
		UPDATE [ContainerStar].[dbo].[PERMISSION]
		SET [DESCRIPTION] = 'Ausführung'
		WHERE ID = 9
	END
	
	IF NOT EXISTS (SELECT ID FROM [ContainerStar].[dbo].[PERMISSION] WHERE ID = 10 )
	BEGIN
		INSERT INTO [ContainerStar].[dbo].[PERMISSION] ([Id], [Name], [Description], [CreateDate], [ChangeDate], [DeleteDate])
		VALUES(10, 'Containers', 'Container' ,GETDATE() ,GETDATE() ,NULL);
		INSERT INTO [ContainerStar].dbo.ROLE_PERMISSION_RSP(RoleId ,PermissionId ,CreateDate ,ChangeDate) 
		VALUES (1 ,10 ,getdate() ,getdate());
	END
	ELSE
	BEGIN
		UPDATE [ContainerStar].[dbo].[PERMISSION]
		SET [DESCRIPTION] = 'Container'
		WHERE ID = 10
	END
	SET IDENTITY_INSERT [ContainerStar].[dbo].[PERMISSION] OFF;
