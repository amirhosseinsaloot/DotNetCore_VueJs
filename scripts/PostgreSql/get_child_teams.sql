DROP FUNCTION IF EXISTS get_child_teams(INT);

CREATE OR REPLACE FUNCTION get_child_teams(root_team_id INTEGER)

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
		
END;$$