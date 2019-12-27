USE MyPlayground
GO
TRUNCATE TABLE CitiesList
GO

Declare @JSON varchar(max)

SELECT @JSON = BulkColumn FROM OPENROWSET (BULK 'C:\MyPlayground\city.list.JSON', SINGLE_CLOB) as J


SELECT *
 FROM OPENROWSET (BULK 'C:\MyPlayground\city.list.JSON', SINGLE_CLOB) as J
 INSERT INTO CitiesList
	select * from openjson (@JSON)
	with (Id  int '$.id',
	Name varchar(100) '$.name',
	CountryCode nchar(2) '$.country',
	Lat float '$.coord.lat',
	Long float '$.coord.lon') 
	GO


			