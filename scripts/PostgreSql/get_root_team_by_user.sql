DROP FUNCTION IF EXISTS get_root_team_by_user(INT);

CREATE OR REPLACE FUNCTION get_root_team_by_user(input_user_id INTEGER)

RETURNS TABLE (
	id INTEGER ,
	name CHARACTER VARYING(50) ,
	description CHARACTER VARYING(100) ,
	parent_id INTEGER ,
	tenant_id INTEGER , 
	created_on timestamp without time zone
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
		
END;$$