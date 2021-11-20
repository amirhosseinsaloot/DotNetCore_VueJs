DROP  PROCEDURE IF EXISTS [dbo].[GetRootTeam]
GO
CREATE PROCEDURE GetRootTeam @ChildTeamId INT
AS
BEGIN

	WITH CTE (TeamId , ParentTeamId)
	AS
	(
	SELECT  Id AS TeamId , ParentId AS ParentTeamId 
	FROM    Team
	WHERE   Id = @ChildTeamId
	UNION   ALL
	SELECT  team.Id AS TeamId , team.ParentId AS ParentTeamId 
	FROM    CTE
	INNER   JOIN Team AS team ON CTE.ParentTeamId = team.Id
	)

	SELECT Team.Id , Team .[Name] , Team .[Description] , Team .ParentId , Team .TenantId , Team .CreatedOn
	FROM CTE
	INNER JOIN Team ON CTE.TeamId = Team.Id 
	WHERE ParentTeamId IS NULL
	OPTION(MAXRECURSION 0)

END