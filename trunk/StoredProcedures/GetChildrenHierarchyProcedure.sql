Use Auction
GO

CREATE PROC categories_FindChildren
@parent_id INT
AS
;WITH r as (
     SELECT IdCategory
     FROM Categories
     WHERE IdParentCategory = @parent_id

     UNION ALL

     SELECT d.IdCategory 
     FROM Categories d
        INNER JOIN r 
           ON d.IdParentCategory = r.IdCategory
)
SELECT c.IdCategory, c.Name, c.Description, c.IdParentCategory from Categories c Join r on c.IdCategory = r.IdCategory 
GO

exec Auction.dbo.categories_FindChildren @parent_id = 1;