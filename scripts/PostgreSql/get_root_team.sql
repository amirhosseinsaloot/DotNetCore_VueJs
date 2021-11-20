DROP FUNCTION IF EXISTS get_root_team(INT);

CREATE OR REPLACE FUNCTION get_root_team(child_team_id INTEGER)

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
		
END;$$