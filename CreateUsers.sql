USE [master]
GO
CREATE LOGIN [common] WITH PASSWORD=N'common', DEFAULT_DATABASE=[ModularMonolith], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
CREATE LOGIN [identity] WITH PASSWORD=N'identity', DEFAULT_DATABASE=[ModularMonolith], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
CREATE LOGIN [order] WITH PASSWORD=N'order', DEFAULT_DATABASE=[ModularMonolith], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
USE [ModularMonolith]
GO

CREATE USER [common] FOR LOGIN [common]
ALTER USER [common] WITH DEFAULT_SCHEMA=[Common]
ALTER AUTHORIZATION ON SCHEMA::[Common] TO [common]
GRANT CREATE TABLE TO [common] -- this is needed to update database from visual studio console

CREATE USER [identity] FOR LOGIN [identity]
ALTER USER [identity] WITH DEFAULT_SCHEMA=[Identity]
ALTER AUTHORIZATION ON SCHEMA::[Identity] TO [identity]
GRANT CREATE TABLE TO [identity] -- this is needed to update database from visual studio console

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
