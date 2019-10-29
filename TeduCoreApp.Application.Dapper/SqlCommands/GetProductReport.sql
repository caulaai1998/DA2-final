CREATE PROC GetProduct
	@fromDate VARCHAR(10),
	@toDate VARCHAR(10)
AS
BEGIN
		  SELECT
    CAST(p.DateCreated AS DATE) as Date,
    COUNT(*) as TotalProduct
FROM
    Products p
	where p.DateCreated >= cast(@fromDate as date)
				and p.DateCreated <= cast(@toDate as date)
GROUP BY
    CAST(p.DateCreated AS DATE)
END

EXEC dbo.GetProduct @fromDate = '10/1/2019',
                         @toDate = '10/31/2019' 