DROP  PROCEDURE IF EXISTS [dbo].[GetRootTeamByUser]
GO
CREATE PROCEDURE GetRootTeamByUser @InputUserId INT
AS
BEGIN

	WITH CTE (TeamId , ParentTeamId)
	AS
	(
	SELECT  Team.Id AS TeamId , Team.ParentId AS ParentTeamId 
	FROM    Team
	INNER   JOIN AspNetUsers ON Team.Id = AspNetUsers.TeamId 
	WHERE   AspNetUsers.Id = @InputUserId 
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