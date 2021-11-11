USE [master]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [kbs_logger]    Script Date: 10/11/2021 18:00:14 ******/
CREATE LOGIN [kbs_logger] WITH PASSWORD=N'w4Xp6z5wyQlkDLZToq04cjGF79pvFdSxrOQFq0y15PI=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

ALTER LOGIN [kbs_logger] DISABLE
GO

USE [Logging]
GO

/****** Object:  User [kbs_logger]    Script Date: 10/11/2021 18:01:41 ******/
CREATE USER [kbs_logger] FOR LOGIN [kbs_logger] WITH DEFAULT_SCHEMA=[dbo]
GO

GRANT EXECUTE ON dbo.proc_insert_kbs_nlog_entry TO [kbs_logger]

GO
USE [master]
GO
ALTER LOGIN [kbs_logger] ENABLE