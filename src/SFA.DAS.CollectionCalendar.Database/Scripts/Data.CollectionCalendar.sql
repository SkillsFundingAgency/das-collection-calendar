PRINT '********************* Prepping @ACADEMIC_YEAR temp table *****************************'
DECLARE @ACADEMIC_YEAR TABLE
(
    [Id] VARCHAR(10) NOT NULL PRIMARY KEY,
    [StartDate] DATETIME NOT NULL,
    [EndDate] DATETIME NOT NULL,
    [HardCloseDate] DATETIME NOT NULL
)
INSERT INTO @ACADEMIC_YEAR ([Id], [StartDate], [EndDate], [HardCloseDate]) VALUES ('2021', '2020-08-01', '2021-07-31', '2021-10-15')
INSERT INTO @ACADEMIC_YEAR ([Id], [StartDate], [EndDate], [HardCloseDate]) VALUES ('2122', '2021-08-01', '2022-07-31', '2022-10-15')
INSERT INTO @ACADEMIC_YEAR ([Id], [StartDate], [EndDate], [HardCloseDate]) VALUES ('2223', '2022-08-01', '2023-07-31', '2023-10-15')
INSERT INTO @ACADEMIC_YEAR ([Id], [StartDate], [EndDate], [HardCloseDate]) VALUES ('2324', '2023-08-01', '2024-07-31', '2024-10-15')
INSERT INTO @ACADEMIC_YEAR ([Id], [StartDate], [EndDate], [HardCloseDate]) VALUES ('2425', '2024-08-01', '2025-07-31', '2025-10-15')

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