CREATE TABLE [dbo].[AcademicYearDetails]
(
	[AcademicYear] CHAR(4) NOT NULL PRIMARY KEY, 
    [StartDate] DATE NOT NULL, 
    [EndDate] DATE NOT NULL, 
    [HardCloseDate] DATE NULL
)
