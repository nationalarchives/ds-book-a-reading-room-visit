USE [Logging]
GO

/****** Object:  StoredProcedure [dbo].[proc_insert_kbs_nlog_entry]    Script Date: 10/11/2021 17:58:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Brian O Reilly
-- Create date: 10th November 2021
-- Description:	Inserts an NLog entry
-- =============================================
CREATE PROCEDURE [dbo].[proc_insert_kbs_nlog_entry]
    @level nvarchar(255),
	@custom_message nvarchar(max),
    @ex_message nvarchar(max),
    @stack_trace nvarchar(max),
    @exception_type nvarchar(255),
    @logger nvarchar(255),
	@url nvarchar(255)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


	INSERT INTO [dbo].[KBS_Log]
    (
		   created_on,
           [level],
		   custom_message,
           exception_message,
           stack_trace,
           exception_type,
           logger,
           [url]
	)
     VALUES
	 (
           GETUTCDATE(),
           @level,
		   @custom_message,
           @ex_message,
           @stack_trace,
           @exception_type,
           @logger,
           @url
	)
END
GO


