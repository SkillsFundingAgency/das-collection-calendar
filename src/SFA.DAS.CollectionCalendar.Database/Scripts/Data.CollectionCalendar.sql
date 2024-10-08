PRINT '********************* Prepping @ACADEMIC_YEAR temp table *****************************'
DECLARE @ACADEMIC_YEAR TABLE
(
    [Id] VARCHAR(10) NOT NULL PRIMARY KEY,
    [StartDate] DATETIME NOT NULL,
    [EndDate] DATETIME NOT NULL,
    [HardCloseDate] DATETIME NOT NULL
)
INSERT INTO @ACADEMIC_YEAR ([Id], [StartDate], [EndDate], [HardCloseDate]) VALUES ('2324', '2023-08-01', '2024-07-31', '2024-10-17')
INSERT INTO @ACADEMIC_YEAR ([Id], [StartDate], [EndDate], [HardCloseDate]) VALUES ('2425', '2024-08-01', '2025-07-31', '2025-10-15')
INSERT INTO @ACADEMIC_YEAR ([Id], [StartDate], [EndDate], [HardCloseDate]) VALUES ('2526', '2025-08-01', '2026-07-31', '2026-10-15')
INSERT INTO @ACADEMIC_YEAR ([Id], [StartDate], [EndDate], [HardCloseDate]) VALUES ('2627', '2026-08-01', '2027-07-31', '2027-10-15')
INSERT INTO @ACADEMIC_YEAR ([Id], [StartDate], [EndDate], [HardCloseDate]) VALUES ('2728', '2027-08-01', '2028-07-31', '2028-10-15')

PRINT '********************* Updating table [incentives].[AcademicYear] *****************************'

MERGE [AcademicYearDetails] AS tgt
USING (
    SELECT
        [Id],
        [StartDate],
        [EndDate],
        [HardCloseDate]
    FROM @ACADEMIC_YEAR
) AS src 
ON (tgt.[AcademicYear] = src.[Id]) 

WHEN NOT MATCHED BY TARGET THEN 
INSERT 
           ([AcademicYear]
           ,[StartDate]
           ,[EndDate]
           ,[HardCloseDate])
     VALUES
           (src.[Id]
           ,src.[StartDate]
           ,src.[EndDate]
           ,src.[HardCloseDate])

WHEN MATCHED
THEN UPDATE
   SET [StartDate] = src.[StartDate]
       ,[EndDate] = src.[EndDate]
       ,[HardCloseDate] = src.[HardCloseDate]

WHEN NOT MATCHED BY SOURCE THEN
    DELETE;