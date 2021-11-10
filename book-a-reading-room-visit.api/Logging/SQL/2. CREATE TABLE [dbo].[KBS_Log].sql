USE [Logging]
GO

/****** Object:  Table [dbo].[KBS_Log]    Script Date: 10/11/2021 17:53:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[KBS_Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[created_on] [datetimeoffset](7) NOT NULL,
	[level] [nvarchar](255) NOT NULL,
	[custom_message] [nvarchar](max) NOT NULL,
	[exception_message] [nvarchar](max) NOT NULL,
	[stack_trace] [nvarchar](max) NULL,
	[exception_type] [nvarchar](255) NOT NULL,
	[logger] [nvarchar](255) NULL,
	[url] [nvarchar](255) NULL,
 CONSTRAINT [PK__KBS_Log__3214EC07FBD49FCF] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


