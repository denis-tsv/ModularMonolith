USE [master]
GO
CREATE LOGIN [communication] WITH PASSWORD=N'communication', DEFAULT_DATABASE=[ModularMonolith_MicroservicesInTheFuture], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
CREATE LOGIN [order] WITH PASSWORD=N'order', DEFAULT_DATABASE=[ModularMonolith_MicroservicesInTheFuture], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
USE [ModularMonolith_MicroservicesInTheFuture]
GO

CREATE USER [communication] FOR LOGIN [communication]
ALTER USER [communication] WITH DEFAULT_SCHEMA=[Communication]
ALTER AUTHORIZATION ON SCHEMA::[Communication] TO [communication]
GRANT CREATE TABLE TO [communication] -- this is needed to update database from visual studio console

CREATE USER [order] FOR LOGIN [order]
ALTER USER [order] WITH DEFAULT_SCHEMA=[Order]
ALTER AUTHORIZATION ON SCHEMA::[Order] TO [order]
GRANT CREATE TABLE TO [order] -- this is needed to update database from visual studio console

GO

-- Replace [USER] to username, PASSWORD to passwod, [DATABASE] to database name, [SCHEMA] to schema
-- USE [master]
-- GO
-- CREATE LOGIN [USER] WITH PASSWORD=N'PASSWORD', DEFAULT_DATABASE=[DATABASE], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
-- GO
-- USE [DATABASE]
-- GO
-- CREATE USER [USER] FOR LOGIN [USER]
-- ALTER USER [USER] WITH DEFAULT_SCHEMA=[SCHEMA]
-- ALTER AUTHORIZATION ON SCHEMA::[SCHEMA] TO [USER]
-- GO
