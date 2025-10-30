USE [MyGuitarShop]
GO
/****** Object:  StoredProcedure [dbo].[GetAllProducts]    Script Date: 10/30/2025 10:41:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Leena Komenski
-- Create date: 10/30/2025
-- Description:	Gets all categories
-- exec GetAllCategories
-- =============================================
CREATE PROCEDURE [dbo].[GetAllCategories]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP (10)
		  [CategoryID], [CategoryName]
	FROM [MyGuitarShop].[dbo].[Categories]
END