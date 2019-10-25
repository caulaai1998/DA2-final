CREATE PROC GetTotalNewUser
	@fromDate VARCHAR(10),
	@toDate VARCHAR(10)
AS
BEGIN
		  SELECT
    CAST(us.DateCreated AS DATE) as Date,
    COUNT(*) as TotalNewUser
FROM
    AppUsers us
	where us.DateCreated >= cast(@fromDate as date)
				and us.DateCreated <= cast(@toDate as date)
GROUP BY
    CAST(us.DateCreated AS DATE)
END

EXEC dbo.GetTotalNewUser @fromDate = '10/1/2019',
                         @toDate = '10/31/2019' 