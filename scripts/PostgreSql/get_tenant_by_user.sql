DROP FUNCTION IF EXISTS get_tenant_by_user(INT);

CREATE OR REPLACE FUNCTION get_tenant_by_user(input_user_id INTEGER)

RETURNS TABLE (
	id INTEGER ,
	name CHARACTER VARYING(50) ,
	created_on timestamp without time zone
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
		
END;$$