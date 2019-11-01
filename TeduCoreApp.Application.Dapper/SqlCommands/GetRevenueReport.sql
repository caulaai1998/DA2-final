CREATE PROC GetRevenueDaily
	@fromDate VARCHAR(10),
	@toDate VARCHAR(10)
AS
BEGIN
		 	  select
                CAST(b.DateCreated AS DATE) as Date,
                sum(bd.Quantity*bd.Price) as Revenue
                from Bills b
                inner join dbo.BillDetails bd
                on b.Id = bd.BillId
                where b.DateCreated <= cast(@toDate as date) 
				AND b.DateCreated >= cast(@fromDate as date)
              group by Cast(b.DateCreated as DATE)
END
EXEC dbo.GetRevenueDaily @fromDate = '12/01/2017',
                         @toDate = '10/24/2019' 