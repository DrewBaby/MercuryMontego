select * from sys.sysusers

select * from sys.tables

CREATE USER gamedbacct FROM LOGIN gamedbacct;

EXEC sp_addrolemember 'db_datareader', 'gamedbacct';

EXEC sp_addrolemember 'db_datawriter', 'gamedbacct';


create role gamdbCustomPerms
ALTER SERVER ROLE programadmin ADD MEMBER mary;


SELECT 
   SP1.name AS ServerRoleName, 
   ISNULL(SP2.name, 'No members') AS LoginName
FROM sys.server_role_members AS SRM
RIGHT OUTER JOIN sys.server_principals AS SP1 ON SRM.role_principal_id = SP1.principal_id
LEFT OUTER JOIN sys.server_principals AS SP2 ON SRM.member_principal_id = SP2.principal_id
WHERE SP1.is_fixed_role = 1 and SP1.name = 'sysadmin'
ORDER BY SP1.name;
GO



USE [master]
GO
CREATE SERVER ROLE [dbaadmin]
GO

GRANT CONTROL SERVER TO [dbaadmin]
GO 


execute as LOGIN ='isitadmin';

execute as LOGIN ='gamedbacct';



ALTER ROLE gamdbCustomPerms ADD MEMBER gamedbacct;

grant execute,insert,delete,update to gamdbCustomPerms
sp_helprole gamdbCustomPerms


exec sp_who2

ALTER ROLE db_ddladmin ADD MEMBER gamedbacct;

select * from userGamerTags