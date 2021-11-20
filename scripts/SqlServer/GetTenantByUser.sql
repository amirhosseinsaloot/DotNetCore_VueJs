DROP  PROCEDURE IF EXISTS [dbo].[GetTenantByUser]
GO
CREATE PROCEDURE GetTenantByUser @InputUserId INT
AS
BEGIN

WITH CTE (TeamId , ParentTeamId , TenantId)
	AS
	(
	SELECT  Team.Id AS TeamId ,Team.ParentId AS ParentTeamId  , Team.TenantId
	FROM    Team
	INNER   JOIN AspNetUsers ON Team.Id = AspNetUsers.TeamId 
	WHERE   AspNetUsers.Id = @InputUserId 
	UNION   ALL
	SELECT  team.Id AS TeamId , team.ParentId AS ParentTeamId , team.TenantId
	FROM    CTE
	INNER   JOIN Team AS team ON CTE.ParentTeamId = team.Id
	)

	SELECT Tenant.Id , Tenant.[Name] , Tenant.CreatedOn
	FROM CTE
	INNER JOIN Tenant ON CTE.TenantId = Tenant.Id
	WHERE ParentTeamId IS NULL
	OPTION(MAXRECURSION 0)

END