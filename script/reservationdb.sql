USE [master]
GO
/****** Object:  Database [reservationdb]    Script Date: 7/14/2018 8:52:23 PM ******/
CREATE DATABASE [reservationdb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'reservationdb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\reservationdb.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'reservationdb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\reservationdb_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [reservationdb] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [reservationdb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [reservationdb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [reservationdb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [reservationdb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [reservationdb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [reservationdb] SET ARITHABORT OFF 
GO
ALTER DATABASE [reservationdb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [reservationdb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [reservationdb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [reservationdb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [reservationdb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [reservationdb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [reservationdb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [reservationdb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [reservationdb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [reservationdb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [reservationdb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [reservationdb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [reservationdb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [reservationdb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [reservationdb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [reservationdb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [reservationdb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [reservationdb] SET RECOVERY FULL 
GO
ALTER DATABASE [reservationdb] SET  MULTI_USER 
GO
ALTER DATABASE [reservationdb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [reservationdb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [reservationdb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [reservationdb] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [reservationdb] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'reservationdb', N'ON'
GO
USE [reservationdb]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 7/14/2018 8:52:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Phone] [nvarchar](20) NULL,
	[Description] [nvarchar](max) NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ContactTypeId] [int] NOT NULL,
	[Birthdate] [date] NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ContactType]    Script Date: 7/14/2018 8:52:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_ContactType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Place]    Script Date: 7/14/2018 8:52:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Place](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Place] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Reservation]    Script Date: 7/14/2018 8:52:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IsFavorite] [bit] NOT NULL,
	[Ranking] [smallint] NOT NULL,
	[ContactId] [int] NOT NULL,
	[PlaceId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Reservation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Index [IX_FK_contact_typecontact]    Script Date: 7/14/2018 8:52:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_contact_typecontact] ON [dbo].[Contact]
(
	[ContactTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_ContactReservation]    Script Date: 7/14/2018 8:52:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_ContactReservation] ON [dbo].[Reservation]
(
	[ContactId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_PlaceReservation]    Script Date: 7/14/2018 8:52:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_PlaceReservation] ON [dbo].[Reservation]
(
	[PlaceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_contact_typecontact] FOREIGN KEY([ContactTypeId])
REFERENCES [dbo].[ContactType] ([Id])
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_contact_typecontact]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_ContactReservation] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contact] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_ContactReservation]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_PlaceReservation] FOREIGN KEY([PlaceId])
REFERENCES [dbo].[Place] ([Id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_PlaceReservation]
GO
/****** Object:  StoredProcedure [dbo].[sp_AddFavorite]    Script Date: 7/14/2018 8:52:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_AddFavorite]
	-- Add the parameters for the stored procedure here
	@Id as int = null,
	@ErrorCode int output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	begin
		UPDATE [dbo].[Reservation]
		SET [IsFavorite] =1
			
		WHERE [Id] = @Id
	end
SET @ErrorCode = @@ERROR
END

GO
/****** Object:  StoredProcedure [dbo].[sp_CountContact]    Script Date: 7/14/2018 8:52:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[sp_CountContact]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COUNT(Id)
		
	FROM Contact
END


GO
/****** Object:  StoredProcedure [dbo].[sp_CountReservation]    Script Date: 7/14/2018 8:52:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[sp_CountReservation]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COUNT(Id)
		
	FROM Reservation
END


GO
/****** Object:  StoredProcedure [dbo].[sp_Delete_Contact]    Script Date: 7/14/2018 8:52:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_Delete_Contact]
	-- Add the parameters for the stored procedure here
	@Id as int = null,
	@ErrorCode int output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE FROM [dbo].[Contact]
	WHERE Contact.Id = @Id

SET @ErrorCode = @@ERROR
END

GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllContacts]    Script Date: 7/14/2018 8:52:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_GetAllContacts]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		[Id]
		,[Name]
		
	FROM
		Contact

END


GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllPlaces]    Script Date: 7/14/2018 8:52:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_GetAllPlaces]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		[Id]
		,[Name]		
	FROM
		Place
END


GO
/****** Object:  StoredProcedure [dbo].[sp_GetContact]    Script Date: 7/14/2018 8:52:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_GetContact]
	@Sort as int = null,
	@Skip as int=10,
	@Take as int=null,
	@ErrorCode int output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	if(@Sort >0)
      SELECT c.[Id],
	         c.[Name],
			 c.[Description],
			 c.[Phone],
			 c.[Birthdate],
			 c.[ContactTypeId],
			 ct.[Id],
			 ct.[Value]		
	  FROM Contact c
	  INNER JOIN ContactType ct ON c.ContactTypeId=ct.Id
      ORDER BY 
         CASE WHEN @Sort = '1' THEN c.[Name] END,
         CASE WHEN @Sort = '2' THEN c.[Phone] END,
	     CASE WHEN @Sort = '3' THEN c.[Birthdate] END,
	     CASE WHEN @Sort = '4' THEN c.[ContactTypeId] END	
      OFFSET (@Skip) ROWS FETCH NEXT (@Take) ROWS ONLY
   
   else
      SELECT c.[Id],
	         c.[Name],
			 c.[Description],
			 c.[Phone],
			 c.[Birthdate],
			 c.[ContactTypeId],
			 ct.[Id],
			 ct.[Value]		
	  FROM Contact c
	  INNER JOIN ContactType ct ON c.ContactTypeId=ct.Id 
	  ORDER BY  c.[Id]
	  OFFSET (@Skip) ROWS FETCH NEXT (@Take) ROWS ONLY
END


GO
/****** Object:  StoredProcedure [dbo].[sp_GetContactById]    Script Date: 7/14/2018 8:52:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetContactById]
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		 [Id]
		,[Name]
		,[Description]
		,[Phone]
		,[ContactTypeId]
		,[Birthdate]
	FROM
		Contact
	WHERE Id = @Id
END


GO
/****** Object:  StoredProcedure [dbo].[sp_GetContactType]    Script Date: 7/14/2018 8:52:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_GetContactType]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		 [Id]
		,[Value]
	FROM
		ContactType
END


GO
/****** Object:  StoredProcedure [dbo].[sp_GetPlaces]    Script Date: 7/14/2018 8:52:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_GetPlaces]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		[Id]
		,[Name]		
	FROM
		Place
END


GO
/****** Object:  StoredProcedure [dbo].[sp_GetReservation]    Script Date: 7/14/2018 8:52:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_GetReservation]
	@Sort as int = null,
	@Skip as int=null,
	@Take as int=null,
	@ErrorCode int output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	if(@Sort >0)
      SELECT r.[Id],
	         r.[IsFavorite],
			 r.[Ranking],			
			 r.[Date],
			 r.[Description],
			 r.[PlaceId],
			 pa.[Name],
			 pa.[Id],
			 r.[ContactId],
			 c.[Birthdate],
			 c.[ContactTypeId],
			 c.[Description],
			 c.[Id],
			 c.[Name],
			 c.[Phone]
	  FROM Reservation r
	  INNER JOIN Place pa ON r.PlaceId=pa.Id
	  INNER JOIN Contact c ON r.ContactId=c.Id
      ORDER BY 
         CASE WHEN @Sort = '1' THEN Date END,
         CASE WHEN @Sort = '2' THEN Date  END Desc,
	     CASE WHEN @Sort = '3' THEN pa.Name END,
	     CASE WHEN @Sort = '4' THEN pa.Name END Desc	,
		 CASE WHEN @Sort = '5' THEN Ranking END
      OFFSET (@Skip*10) ROWS FETCH NEXT (@Take) ROWS ONLY
   
   else
       SELECT r.[Id],
	         r.[IsFavorite],
			 r.[Ranking],			
			 r.[Date],
			 r.[Description],
			 r.[PlaceId],
			 pa.[Name],
			 pa.[Id],
			 r.[ContactId],
			 c.[Birthdate],
			 c.[ContactTypeId],
			 c.[Description],
			 c.[Id],
			 c.[Name],
			 c.[Phone]	
	  FROM Reservation r
	  INNER JOIN Place pa ON r.PlaceId=pa.Id
	  INNER JOIN Contact c ON r.ContactId=c.Id
	  ORDER BY r.[Id]
	  OFFSET (@Skip*10) ROWS FETCH NEXT (@Take) ROWS ONLY
END


GO
/****** Object:  StoredProcedure [dbo].[sp_GetReservationById]    Script Date: 7/14/2018 8:52:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetReservationById]
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		 [Id]
		,[IsFavorite]
		,[Ranking]
		,[Description]
		,[ContactId]
		,[PlaceId]
		,[Date]
	FROM
		Reservation
	WHERE Id = @Id
END


GO
/****** Object:  StoredProcedure [dbo].[sp_Insert_Contact]    Script Date: 7/14/2018 8:52:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_Insert_Contact]
	-- Add the parameters for the stored procedure here
	@Name as varchar(50),
	@Description as nvarchar(max),
    @Phone as varchar(20),
	@ContactTypeId as int,  
	@Birthdate as datetime,
	@ReturnValue int output,
	@ErrorCode int output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO [dbo].[Contact]
           ([Name]
           ,[Description]
           ,[Phone]
           ,[ContactTypeId]
           ,[Birthdate])     
     VALUES
           (@Name
           ,@Description
           ,@Phone
           ,@ContactTypeId
           ,@Birthdate
           )

Set @ReturnValue = @@IDENTITY
SET @ErrorCode = @@ERROR
END


GO
/****** Object:  StoredProcedure [dbo].[sp_Update_Contact]    Script Date: 7/14/2018 8:52:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_Update_Contact]
	-- Add the parameters for the stored procedure here
	@Id as int = null,
	@Name as varchar(50),
	@Description as nvarchar(max),
    @Phone as varchar(20),
	@ContactTypeId as int,  
	@Birthdate as datetime,
	@ReturnValue int output,
	@ErrorCode int output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	if @Id is null 
	begin
		INSERT INTO [dbo].[Contact]
          ([Name]
           ,[Description]
           ,[Phone]
           ,[ContactTypeId]
           ,[Birthdate]) 
		VALUES
           (@Name
           ,@Description
           ,@Phone
           ,@ContactTypeId
           ,@Birthdate
           )
		Set @ReturnValue = @@IDENTITY
	end
	else
	begin
		UPDATE [dbo].[Contact]
		SET [Name] = @Name
			,[Description] = @Description
			,[Phone] = @Phone
			,[ContactTypeId] = @ContactTypeId
			,[Birthdate] = @Birthdate
		WHERE [Id] = @Id
		Set @ReturnValue = @Id
	end
SET @ErrorCode = @@ERROR
END

GO
/****** Object:  StoredProcedure [dbo].[sp_Update_Reservation]    Script Date: 7/14/2018 8:52:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_Update_Reservation]
	-- Add the parameters for the stored procedure here
	@Id as int = null,
	@IsFavorite as bit,
	@Ranking as smallint,
	@Description as nvarchar(max),
    @ContactId as int,
	@PlaceId as int,  
	@Date as datetime,
	@ReturnValue int output,
	@ErrorCode int output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	if @Id is null 
	begin
		INSERT INTO [dbo].[Reservation]
          ([IsFavorite]
           ,[Ranking]
		   ,[Description]
           ,[ContactId]
           ,[PlaceId]
           ,[Date]) 
		VALUES
           (@IsFavorite
           ,@Ranking
		   ,@Description
           ,@ContactId
           ,@PlaceId
           ,@Date
           )
		Set @ReturnValue = @@IDENTITY
	end
	else
	begin
		UPDATE [dbo].[Reservation]
		SET [IsFavorite] = @IsFavorite
			,[Ranking] = @Ranking
			,[Description]=@Description
			,[ContactId] = @ContactId
			,[PlaceId] = @PlaceId
			,[Date] = @Date
		WHERE [Id] = @Id
		Set @ReturnValue = @Id
	end
SET @ErrorCode = @@ERROR
END

GO
USE [master]
GO
ALTER DATABASE [reservationdb] SET  READ_WRITE 
GO
