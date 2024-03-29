USE [QuizDB]
GO
/****** Object:  UserDefinedTableType [dbo].[udt_QuestionOption]    Script Date: 2/12/2018 8:54:26 PM ******/
CREATE TYPE [dbo].[udt_QuestionOption] AS TABLE(
	[OptionId] [int] NULL,
	[QuestionID] [int] NULL,
	[OptionText] [varchar](50) NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedAt] [datetime] NULL
)
GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateQuestionAndOptions]    Script Date: 2/12/2018 8:54:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateQuestionAndOptions] 
	@pQuestionID int = 0,
	@pQuestion varchar(100),
	@pQuizID int,
	@pIsActive bit = 1,
	@CreatedBy int,
	@pCreatedAt datetime,
	@pUpdatedBy int = null,
	@pUpdatedAt datetime = null
	
AS
BEGIN
	
	SET NOCOUNT ON;

    
	
END

GO
/****** Object:  StoredProcedure [dbo].[usp_InsertUpdateQuiz]    Script Date: 2/12/2018 8:54:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_InsertUpdateQuiz] 
	-- Add the parameters for the stored procedure here
	@pID int = 0,
	@pName varchar(50),
	@pDescription varchar(500) = null,
	@pType int = null,
	@pTimer bit = null,
	@pAllowedDuration int = null,
	@pAllowReAttempt bit = null,
	@pReAttemptDuration int = null,
	@pPassMarks int = null,
	@pQuizUrl varchar(50) = null,
	@pCreatedBy int,
	@pCreatedAt datetime,
	@pUpdatedBy int =null,
	@pUpdatedAt datetime=null
AS
BEGIN
	
	if(@pID=0)
	begin
	if not exists(select 1 from tblQuiz where Name = @pName)
	begin
	Insert into tblQuiz(Name,Description,Type,Timer,AllowedDuration,AllowReAttempt,ReAttemptDuration,PassMarks,QuizUrl,CreatedBy,CreatedAt) values (@pName,@pDescription,@pType,@pTimer,@pAllowedDuration,@pAllowReAttempt,@pReAttemptDuration,@pPassMarks,@pQuizUrl,@pCreatedBy,@pCreatedAt)
	end
	else
	begin
	Select 'Quiz already exists'
	end
	end
	else
	begin
	Update tblQuiz set Name=@pName,Description=@pDescription,Type=@pType,Timer=@pTimer,AllowedDuration=@pAllowedDuration,AllowReAttempt=@pAllowReAttempt,ReAttemptDuration=@pAllowedDuration,PassMarks=@pPassMarks,QuizUrl=@pQuizUrl,UpdatedBy=@pUpdatedBy,UpdatedAt=@pUpdatedAt
	Where ID = @pID
	end




	
END

GO
/****** Object:  StoredProcedure [dbo].[uspLoadQuizAndQuestions]    Script Date: 2/12/2018 8:54:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[uspLoadQuizAndQuestions] 
	-- Add the parameters for the stored procedure here
	@pQuizId int
AS
BEGIN
	Select * from tblQuiz
	Where ID = @pQuizId

	Select * from tblQuizQuestions
	Where QuizId = @pQuizId

	select QO.* from tblQuestionOptions QO
	Join tblQuizQuestions QQ on QQ.QuestionId = Qo.QuestionId
	Join tblQuiz Q on Q.ID = QQ.QuizId
	Where Q.ID = @pQuizId
	
END

GO
/****** Object:  StoredProcedure [dbo].[uspLoadQuizByCategory]    Script Date: 2/12/2018 8:54:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[uspLoadQuizByCategory]
@pCategoryName varchar(20)='',
@pCategoryId int=0,
@pPageNo int = 1,
@pPageSize int = 20
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select Q.* from tblQuiz Q
	Join tblQuizCategories QC on Q.Type = QC.Id
	Where (ISNULL(@pCategoryId,0)=0 or Q.Id = @pCategoryId)
	And (ISNULL(@pCategoryName,'')='' or QC.Category like '%'+@pCategoryName+'%')

    
END

GO
/****** Object:  Table [dbo].[tblCorrectOptions]    Script Date: 2/12/2018 8:54:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCorrectOptions](
	[AnswerId] [int] IDENTITY(1,1) NOT NULL,
	[QuestionId] [int] NULL,
	[CorrectOptionId] [int] NULL,
 CONSTRAINT [PK_tblCorrectOptions] PRIMARY KEY CLUSTERED 
(
	[AnswerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblQuestionOptions]    Script Date: 2/12/2018 8:54:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblQuestionOptions](
	[OptionId] [int] IDENTITY(1,1) NOT NULL,
	[QuestionId] [int] NOT NULL,
	[OptionText] [varchar](50) NULL,
	[OptionOrder] [int] NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [PK_tblQuestionOptions] PRIMARY KEY CLUSTERED 
(
	[OptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblQuiz]    Script Date: 2/12/2018 8:54:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblQuiz](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[Description] [varchar](1000) NULL,
	[Type] [int] NULL,
	[Timer] [bit] NULL,
	[Hours] [int] NULL,
	[AllowReAttempt] [bit] NULL,
	[ReAttemptDuration] [int] NULL,
	[PassMarks] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedAt] [datetime2](7) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[QuizUrl] [varchar](50) NULL,
	[IsActive] [bit] NULL,
	[Minutes] [int] NULL,
	[QuizNotes] [varchar](1000) NULL,
	[RequiresLogin] [bit] NULL,
 CONSTRAINT [PK_tblQuiz] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblQuizCategories]    Script Date: 2/12/2018 8:54:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblQuizCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Category] [varchar](50) NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
 CONSTRAINT [PK_tblQuizCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblQuizQuestions]    Script Date: 2/12/2018 8:54:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblQuizQuestions](
	[QuestionId] [int] IDENTITY(1,1) NOT NULL,
	[QuizId] [int] NOT NULL,
	[QuestionText] [varchar](500) NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [PK_tblQuizQuestions] PRIMARY KEY CLUSTERED 
(
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblUsers]    Script Date: 2/12/2018 8:54:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblUsers](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[CreatedBy] [int] NULL,
	[CreatedAt] [datetime2](7) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[IsActive] [bit] NULL,
	[UserRole] [int] NULL,
	[UserEmail] [varchar](50) NULL,
 CONSTRAINT [PK_tblUsers] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
