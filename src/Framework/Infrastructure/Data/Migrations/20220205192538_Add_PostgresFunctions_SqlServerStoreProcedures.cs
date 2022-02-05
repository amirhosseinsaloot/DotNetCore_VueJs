using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class Add_PostgresFunctions_SqlServerStoreProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (ActiveProvider == "Npgsql.EntityFrameworkCore.PostgreSQL")
            {
                var get_child_teams = @"DROP FUNCTION IF EXISTS get_child_teams(INT);

										CREATE OR REPLACE FUNCTION get_child_teams(root_team_id INTEGER)

										RETURNS TABLE (
											id INTEGER ,
											name CHARACTER VARYING(50) ,
											description CHARACTER VARYING(100) ,
											parent_id INTEGER ,
											tenant_id INTEGER , 
											created_on timestamp with time zone
										)  
										LANGUAGE plpgsql
										AS $$

										BEGIN

										RETURN QUERY 
											WITH RECURSIVE cte (id)
												AS
												(
												SELECT  team.id
												FROM    team
												WHERE   team.id = root_team_id 
												UNION ALL
												SELECT  team.id 
												FROM    team
												INNER   JOIN cte ON team.parent_id = cte.id
												)

												SELECT team.id , team .name , team .description , team .parent_id , team .tenant_id , team .created_on 
												FROM cte
												INNER JOIN team ON cte.id = team.id;
		
										END;$$";
                migrationBuilder.Sql(get_child_teams);

                var get_root_team = @"DROP FUNCTION IF EXISTS get_root_team(INT);

									CREATE OR REPLACE FUNCTION get_root_team(child_team_id INTEGER)

									RETURNS TABLE (
										id INTEGER ,
										name CHARACTER VARYING(50) ,
										description CHARACTER VARYING(100) ,
										parent_id INTEGER ,
										tenant_id INTEGER , 
										created_on timestamp with time zone
									)  

									LANGUAGE plpgsql
									AS $$

									BEGIN

									RETURN QUERY 
										WITH RECURSIVE cte (team_id , parent_team_id)
											AS
											(
											SELECT  team.id AS team_id , team.parent_id AS parent_team_id 
											FROM    team
											WHERE   team.id = child_team_id
											UNION   ALL
											SELECT  team.id AS team_id , team.parent_id AS parent_team_id
											FROM    cte
											INNER   JOIN team ON cte.parent_team_id = team.id
											)

											SELECT team.id , team .name , team .description , team .parent_id , team .tenant_id , team .created_on 
											FROM cte
											INNER JOIN team ON cte.team_id = team.id
											WHERE cte.parent_team_id IS NULL;
		
									END;$$";
                migrationBuilder.Sql(get_root_team);

                var get_root_team_by_user = @"DROP FUNCTION IF EXISTS get_root_team_by_user(INT);

											CREATE OR REPLACE FUNCTION get_root_team_by_user(input_user_id INTEGER)

											RETURNS TABLE (
												id INTEGER ,
												name CHARACTER VARYING(50) ,
												description CHARACTER VARYING(100) ,
												parent_id INTEGER ,
												tenant_id INTEGER , 
												created_on timestamp with time zone
											)  

											LANGUAGE plpgsql
											AS $$

											DECLARE root_team INTEGER;

											BEGIN

											RETURN QUERY 
												WITH RECURSIVE cte (team_id , parent_team_id)
													AS
													(
													SELECT  team.id AS team_id , team.parent_id AS parent_team_id 
													FROM    team
													INNER   JOIN asp_net_users ON team.id = asp_net_users.team_id 
													WHERE   asp_net_users.id = input_user_id 
													UNION   ALL
													SELECT  team.Id AS team_id , team.parent_id AS parent_team_id 
													FROM    cte
													INNER   JOIN team ON CTE.parent_team_id = team.Id
													)

													SELECT team.id , team .name , team .description , team .parent_id , team .tenant_id , team .created_on 
													FROM cte
													INNER JOIN team ON cte.team_id = team.id
													WHERE cte.parent_team_id IS NULL;
		
											END;$$";
                migrationBuilder.Sql(get_root_team_by_user);

                var get_tenant_by_user = @"DROP FUNCTION IF EXISTS get_tenant_by_user(INT);

										CREATE OR REPLACE FUNCTION get_tenant_by_user(input_user_id INTEGER)

										RETURNS TABLE (
											id INTEGER ,
											name CHARACTER VARYING(50) ,
											created_on timestamp with time zone
										)  
										LANGUAGE plpgsql
										AS $$

										BEGIN

										RETURN QUERY 
											WITH RECURSIVE cte (team_id , parent_team_id , tenant_id)
												AS
												(
												SELECT  team.id AS team_id , team.parent_id AS parent_team_id , team.tenant_id AS tenant_id
												FROM    team
												INNER JOIN asp_net_users ON team.id = asp_net_users.team_id 
												WHERE  asp_net_users.id = input_user_id 
												UNION ALL
												SELECT  team.Id AS team_id , team.parent_id AS parent_team_id , team.tenant_id AS tenant_id
												FROM    cte
												INNER   JOIN team ON CTE.parent_team_id = team.Id
												)

												SELECT tenant.id , tenant.name , tenant.created_on
												FROM cte
												INNER JOIN tenant ON cte.tenant_id = tenant.Id
												WHERE cte.parent_team_id IS NULL;
		
										END;$$";
                migrationBuilder.Sql(get_tenant_by_user);
            }

            if (ActiveProvider == "Microsoft.EntityFrameworkCore.SqlServer")
            {
                var GetChildTeams = @"DROP  PROCEDURE IF EXISTS [dbo].[GetChildTeams]
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

									END";
                migrationBuilder.Sql(GetChildTeams);

                var GetRootTeam = @"DROP  PROCEDURE IF EXISTS [dbo].[GetRootTeam]
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
								  
								  END";
                migrationBuilder.Sql(GetRootTeam);

                var GetRootTeamByUser = @"DROP  PROCEDURE IF EXISTS [dbo].[GetRootTeamByUser]
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

										END";
                migrationBuilder.Sql(GetRootTeamByUser);

                var GetTenantByUser = @"DROP  PROCEDURE IF EXISTS [dbo].[GetTenantByUser]
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
									  
									  END";
                migrationBuilder.Sql(GetTenantByUser);
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            if (ActiveProvider == "Npgsql.EntityFrameworkCore.PostgreSQL")
            {
                var get_child_teams = @"DROP FUNCTION IF EXISTS get_child_teams(INT)";
                migrationBuilder.Sql(get_child_teams);

                var get_root_team = @"DROP FUNCTION IF EXISTS get_root_team(INT);";
                migrationBuilder.Sql(get_root_team);

                var get_root_team_by_user = @"DROP FUNCTION IF EXISTS get_root_team_by_user(INT);";
                migrationBuilder.Sql(get_root_team_by_user);

                var get_tenant_by_user = @"DROP FUNCTION IF EXISTS get_tenant_by_user(INT);";
                migrationBuilder.Sql(get_tenant_by_user);
            }

            if (ActiveProvider == "Microsoft.EntityFrameworkCore.SqlServer")
            {
                var GetChildTeams = @"DROP  PROCEDURE IF EXISTS [dbo].[GetChildTeams]";
                migrationBuilder.Sql(GetChildTeams);

                var GetRootTeam = @"DROP  PROCEDURE IF EXISTS [dbo].[GetRootTeam]";
                migrationBuilder.Sql(GetRootTeam);

                var GetRootTeamByUser = @"DROP  PROCEDURE IF EXISTS [dbo].[GetRootTeamByUser]";
                migrationBuilder.Sql(GetRootTeamByUser);

                var GetTenantByUser = @"DROP  PROCEDURE IF EXISTS [dbo].[GetTenantByUser]";
                migrationBuilder.Sql(GetTenantByUser);
            }
        }
    }
}
