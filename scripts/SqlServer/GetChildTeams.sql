DROP  PROCEDURE IF EXISTS [dbo].[GetChildTeams]
GO
CREATE PROCEDURE GetChildTeams @RootTeamId INT
AS
BEGIN

	WITH CTE (Id)
	AS
	(
	SELECT  Id
	FROM    Team
	WHERE   Id = @RootTeamId 
	UNION   ALL
	SELECT  team.Id 
	FROM    Team team
	INNER   JOIN CTE ON team.ParentId = CTE.Id
	)

	SELECT Team.Id , Team .[Name] , Team .[Description] , Team .ParentId , Team .TenantId , Team .CreatedOn
	FROM CTE
	INNER JOIN Team ON CTE.Id = Team.Id
	OPTION(MAXRECURSION 0)

END