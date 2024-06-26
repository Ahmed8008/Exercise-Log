USE [ExerciseLog]
GO
/****** Object:  Table [dbo].[img]    Script Date: 5/13/2024 10:22:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[img](
	[Img_id] [int] NULL,
	[f_img] [varbinary](max) NULL,
	[f_type] [varchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Log]    Script Date: 5/13/2024 10:22:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Log](
	[Log_id] [int] IDENTITY(1,1) NOT NULL,
	[Exercise_id] [int] NULL,
	[date] [varchar](max) NULL,
	[sett] [int] NULL,
	[rep] [int] NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[Log_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NewExercise]    Script Date: 5/13/2024 10:22:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NewExercise](
	[Exercise_id] [int] IDENTITY(1,1) NOT NULL,
	[Excercise_name] [varchar](max) NULL,
	[Exercise_img] [varbinary](max) NULL,
	[img_type] [varchar](max) NULL,
 CONSTRAINT [PK_NewExercise] PRIMARY KEY CLUSTERED 
(
	[Exercise_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
