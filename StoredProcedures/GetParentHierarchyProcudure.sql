Use Auction
GO

CREATE PROCEDURE categories_GetCatParentsNew
@lCategoryID INT
AS
set nocount on

Declare @CurrentParent INT

Declare @theTable Table (theIndex int IDENTITY(1,1), CategoryID int)

DECLARE @CategoryID INT

-- Added this so I'll be able to get the breadcrumb which includes the current category I'm requesting

INSERT INTO @theTable (CategoryID) VALUES (@lCategoryID)

SELECT @CurrentParent = IdParentCategory FROM Auction.dbo.Categories WHERE IdCategory = @lCategoryID 

IF @@ROWCOUNT <= 0 RETURN -1



INSERT INTO @theTable (CategoryID) VALUES (@CategoryID)



WHILE @CurrentParent IS NOT NULL BEGIN

  SET @CategoryID = @CurrentParent

  INSERT INTO @theTable (CategoryID) VALUES (@CategoryID)



  SELECT @CurrentParent = IdParentCategory FROM Auction.dbo.Categories WHERE IdCategory = @CategoryID

  IF @@ROWCOUNT <= 0 SET @CurrentParent = NULL

END



-- Return the values from parent->child->child...

SELECT c.IdCategory, c.Name, c.Description, c.IdParentCategory FROM Categories c INNER JOIN @theTable t ON c.IdCategory = t.CategoryID ORDER BY theIndex DESC

GO

exec Auction.dbo.categories_GetCatParentsNew @lCategoryID = 1;

