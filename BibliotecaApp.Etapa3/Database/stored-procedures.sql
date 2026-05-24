-- SQL Server
CREATE OR ALTER PROCEDURE dbo.sp_GetAuthorStockReport
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Author,
        COUNT(*) AS BooksCount,
        SUM(Stock) AS TotalStock
    FROM Books
    GROUP BY Author
    ORDER BY Author;
END;
GO

-- PostgreSQL
CREATE OR REPLACE FUNCTION public.sp_get_author_stock_report()
RETURNS TABLE(
    "Author" text,
    "BooksCount" integer,
    "TotalStock" integer
)
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT
        b."Author",
        COUNT(*)::int,
        COALESCE(SUM(b."Stock"), 0)::int
    FROM "Books" b
    GROUP BY b."Author"
    ORDER BY b."Author";
END;
$$;
