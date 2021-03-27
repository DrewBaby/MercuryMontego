select * from sys.tables 

select * from users
sp_help users


drop table users

select * from Companies
select * from InvolvedCompanies


sp_help InvolvedCompanies

 IDENTITY_INSERT to assign values to the IDENTITY column).

sp_help games
sp_who2
select * into users3 from GameclubDB..users

drop database GameclubDB
create database GameClubDB

select * from games


CREATE TABLE dbo.Tmp_City(Id int NOT NULL IDENTITY(1, 1), Name varchar(50) NULL, Country varchar(50), )  
ON[PRIMARY]  
go  
SET IDENTITY_INSERT dbo.Tmp_City ON  
go  
Alter Table City  
switch to Tmp_City;  
go  
DROP TABLE dbo.City  
go  
Exec sp_rename 'Tmp_City', 'City' 


select * from games

select * from sys.tables
drop table Likes
CREATE TABLE [Likes] (
    [like_id] int NOT NULL identity(1,1),
    [user_id] int NOT NULL,
    [review_id] int NOT NULL,
    [created_at] datetime2 NOT NULL,
    [updated_at] datetime2 NOT NULL,
    CONSTRAINT [PK_Likes] PRIMARY KEY ([like_id]),
    CONSTRAINT [FK_Likes_Reviews_review_id] FOREIGN KEY ([review_id]) REFERENCES [Reviews] ([review_id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Likes_Users_user_id] FOREIGN KEY ([user_id]) REFERENCES [Users] ([user_id]) ON DELETE No Action 
);




sp_help games

select * from likes


drop table review
CREATE TABLE Reviews (
    [review_id] int NOT NULL identity(1,1),
    [text] int NOT NULL,
    [user_id] int NOT NULL,
    [game_id] int NOT NULL,
    [created_at] datetime2 NOT NULL,
    [updated_at] datetime2 NOT NULL,
    CONSTRAINT [PK_Reviews] PRIMARY KEY ([review_id])
);


CREATE TABLE ReviewResponse (
    [reviewResponse_id] int NOT NULL identity(1,1),
    [response] int NOT NULL,
    [user_id] int NOT NULL,
    [review_id] int NOT NULL,
    [created_at] datetime2 NOT NULL,
    [updated_at] datetime2 NOT NULL,
    CONSTRAINT [PK_ReviewResponse] PRIMARY KEY ([reviewResponse_id])
);

ALTER TABLE Reviews drop CONSTRAINT [PK_Reviews]
drop table Reviews

CREATE TABLE [Reviews] (
    [review_id] int NOT NULL,
    [text] nvarchar(200) NOT NULL,
    [user_id] int NOT NULL,
    [gameId] int NOT NULL,
    [created_at] datetime2 NOT NULL,
    [updated_at] datetime2 NOT NULL,
    CONSTRAINT [PK_Reviews] PRIMARY KEY ([review_id]),
    CONSTRAINT [FK_Reviews_Games_gameId] FOREIGN KEY ([gameId]) REFERENCES [Games] ([gameId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Reviews_Users_user_id] FOREIGN KEY ([user_id]) REFERENCES [Users] ([user_id]) ON DELETE CASCADE
);


Drop table likes







/*

EF

CREATE TABLE [Reviews] (
    [review_id] int NOT NULL,
    [text] nvarchar(200) NOT NULL,
    [user_id] int NOT NULL,
    [gameId] int NOT NULL,
    [created_at] datetime2 NOT NULL,
    [updated_at] datetime2 NOT NULL,
    CONSTRAINT [PK_Reviews] PRIMARY KEY ([review_id]),
    CONSTRAINT [FK_Reviews_Games_gameId] FOREIGN KEY ([gameId]) REFERENCES [Games] ([gameId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Reviews_Users_user_id] FOREIGN KEY ([user_id]) REFERENCES [Users] ([user_id]) ON DELETE CASCADE
);




*/









USE [GameclubDB]
GO

CREATE TABLE [dbo].[Users] (
    [user_id]    INT            NOT NULL  identity(1,1),
    [firstname]  NVARCHAR (30)  NOT NULL,
    [lastname]   NVARCHAR (30)  NOT NULL,
    [email]      NVARCHAR (MAX) NOT NULL,
    [password]   NVARCHAR (MAX) NOT NULL,
    [created_at] DATETIME2 (7)  NOT NULL,
    [updated_at] DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([user_id] ASC)
);


CREATE TABLE [dbo].[Games] (
    [gameId]       INT            NOT NULL  identity(1,1),
    [name]         NVARCHAR (MAX) NULL,
    [rating]       FLOAT (53)     NOT NULL,
    [popularity]   FLOAT (53)     NOT NULL,
    [summary]      NVARCHAR (MAX) NULL,
    [user_id]      INT            NOT NULL,
    [release_date] DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_Games] PRIMARY KEY CLUSTERED ([gameId] ASC)
);

CREATE TABLE [dbo].[Genres] (
    [genre_id]   INT            NOT NULL  identity(1,1),
    [name]       NVARCHAR (MAX) NULL,
    [slug]       NVARCHAR (MAX) NULL,
    [created_at] DATETIME2 (7)  NOT NULL,
    [updated_at] DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_Genres] PRIMARY KEY CLUSTERED ([genre_id] ASC)
);



CREATE TABLE [dbo].[Companies] (
    [companyId]  INT            NOT NULL  identity(1,1),
    [name]       NVARCHAR (MAX) NULL,
    [created_at] DATETIME2 (7)  NOT NULL,
    [updated_at] DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED ([companyId] ASC)
);


CREATE TABLE [dbo].[Platforms] (
    [platform_id] INT            NOT NULL  identity(1,1),
    [name]        NVARCHAR (MAX) NULL,
    [created_at]  DATETIME2 (7)  NOT NULL,
    [updated_at]  DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_Platforms] PRIMARY KEY CLUSTERED ([platform_id] ASC)
);

CREATE TABLE [dbo].[ReviewResponse] (
    [reviewResponse_id] INT           IDENTITY (1, 1) NOT NULL,
    [response]          INT           NOT NULL,
    [user_id]           INT           NOT NULL,
    [review_id]         INT           NOT NULL,
    [created_at]        DATETIME2 (7) NOT NULL,
    [updated_at]        DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_ReviewResponse] PRIMARY KEY CLUSTERED ([reviewResponse_id] ASC)
);

CREATE TABLE [dbo].[Screenshots] (
    [screenshot_id] INT            NOT NULL IDENTITY (1, 1),
    [gameId]        INT            NOT NULL,
    [image_id]      NVARCHAR (MAX) NULL,
    [created_at]    DATETIME2 (7)  NOT NULL,
    [updated_at]    DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_Screenshots] PRIMARY KEY CLUSTERED ([screenshot_id] ASC),
    CONSTRAINT [FK_Screenshots_Games_gameId] FOREIGN KEY ([gameId]) REFERENCES [dbo].[Games] ([gameId]) ON DELETE CASCADE
);
GO
CREATE NONCLUSTERED INDEX [IX_Screenshots_gameId]
    ON [dbo].[Screenshots]([gameId] ASC);

CREATE TABLE [dbo].[Videos] (
    [TheVideo_id] INT            NOT NULL IDENTITY (1, 1),
    [gameId]      INT            NOT NULL,
    [name]        NVARCHAR (MAX) NULL,
    [video_id]    NVARCHAR (MAX) NULL,
    [created_at]  DATETIME2 (7)  NOT NULL,
    [updated_at]  DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_Videos] PRIMARY KEY CLUSTERED ([TheVideo_id] ASC),
    CONSTRAINT [FK_Videos_Games_gameId] FOREIGN KEY ([gameId]) REFERENCES [dbo].[Games] ([gameId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Videos_gameId]
    ON [dbo].[Videos]([gameId] ASC);









CREATE TABLE [dbo].[Covers] (
    [cover_id]   INT            NOT NULL IDENTITY (1, 1),
    [gameId]     INT            NOT NULL,
    [image_id]   NVARCHAR (MAX) NULL,
    [created_at] DATETIME2 (7)  NOT NULL,
    [updated_at] DATETIME2 (7)  NOT NULL
);

GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Covers_gameId]
    ON [dbo].[Covers]([gameId] ASC);
GO
ALTER TABLE [dbo].[Covers]
    ADD CONSTRAINT [PK_Covers] PRIMARY KEY CLUSTERED ([cover_id] ASC);
GO
ALTER TABLE [dbo].[Covers]
    ADD CONSTRAINT [FK_Covers_Games_gameId] FOREIGN KEY ([gameId]) REFERENCES [dbo].[Games] ([gameId]) ON DELETE CASCADE;



CREATE TABLE [dbo].[InvolvedCompanies] (
    [involveCompany_id] INT           NOT NULL IDENTITY (1, 1),
    [companyId]         INT           NOT NULL,
    [gameId]            INT           NOT NULL,
    [created_at]        DATETIME2 (7) NOT NULL,
    [updated_at]        DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_InvolvedCompanies] PRIMARY KEY CLUSTERED ([involveCompany_id] ASC),
    CONSTRAINT [FK_InvolvedCompanies_Companies_companyId] FOREIGN KEY ([companyId]) REFERENCES [dbo].[Companies] ([companyId]) ON DELETE CASCADE,
    CONSTRAINT [FK_InvolvedCompanies_Games_gameId] FOREIGN KEY ([gameId]) REFERENCES [dbo].[Games] ([gameId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_InvolvedCompanies_companyId]
    ON [dbo].[InvolvedCompanies]([companyId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_InvolvedCompanies_gameId]
    ON [dbo].[InvolvedCompanies]([gameId] ASC);






-------------------------------------------------------------

CREATE TABLE [dbo].[Expansions] (
    [id]         INT            NOT NULL IDENTITY (1, 1),
    [name]       NVARCHAR (MAX) NULL,
    [cover_id]   INT            NULL,
    [gameId]     INT            NOT NULL,
    [created_at] DATETIME2 (7)  NOT NULL,
    [updated_at] DATETIME2 (7)  NOT NULL
);


CREATE TABLE [dbo].[GameGenres] (
    [gameGenre_id] INT           NOT NULL IDENTITY (1, 1),
    [gameId]       INT           NOT NULL,
    [genre_id]     INT           NOT NULL,
    [created_at]   DATETIME2 (7) NOT NULL,
    [updated_at]   DATETIME2 (7) NOT NULL
);
GO
CREATE NONCLUSTERED INDEX [IX_GameGenres_gameId]
    ON [dbo].[GameGenres]([gameId] ASC);
GO
CREATE NONCLUSTERED INDEX [IX_GameGenres_genre_id]
    ON [dbo].[GameGenres]([genre_id] ASC);
GO
ALTER TABLE [dbo].[GameGenres]
    ADD CONSTRAINT [PK_GameGenres] PRIMARY KEY CLUSTERED ([gameGenre_id] ASC);
GO
ALTER TABLE [dbo].[GameGenres]
    ADD CONSTRAINT [FK_GameGenres_Games_gameId] FOREIGN KEY ([gameId]) REFERENCES [dbo].[Games] ([gameId]) ON DELETE CASCADE;
GO
ALTER TABLE [dbo].[GameGenres]
    ADD CONSTRAINT [FK_GameGenres_Genres_genre_id] FOREIGN KEY ([genre_id]) REFERENCES [dbo].[Genres] ([genre_id]) ON DELETE CASCADE;

CREATE TABLE [dbo].[GamePlatforms] (
    [gamePlatform_id] INT           NOT NULL IDENTITY (1, 1),
    [gameId]          INT           NOT NULL,
    [platform_id]     INT           NOT NULL,
    [created_at]      DATETIME2 (7) NOT NULL,
    [updated_at]      DATETIME2 (7) NOT NULL
);
GO
CREATE NONCLUSTERED INDEX [IX_GamePlatforms_gameId]
    ON [dbo].[GamePlatforms]([gameId] ASC);
GO
CREATE NONCLUSTERED INDEX [IX_GamePlatforms_platform_id]
    ON [dbo].[GamePlatforms]([platform_id] ASC);
GO
ALTER TABLE [dbo].[GamePlatforms]
    ADD CONSTRAINT [PK_GamePlatforms] PRIMARY KEY CLUSTERED ([gamePlatform_id] ASC);
GO
ALTER TABLE [dbo].[GamePlatforms]
    ADD CONSTRAINT [FK_GamePlatforms_Games_gameId] FOREIGN KEY ([gameId]) REFERENCES [dbo].[Games] ([gameId]) ON DELETE CASCADE;
GO
ALTER TABLE [dbo].[GamePlatforms]
    ADD CONSTRAINT [FK_GamePlatforms_Platforms_platform_id] FOREIGN KEY ([platform_id]) REFERENCES [dbo].[Platforms] ([platform_id]) ON DELETE CASCADE;








[RazorPagesMoviesContext-2100c266-e260-4b59-8946-a50469c7f2db]


sp_help users
sp_helpdb 

SET IDENTITY_INSERT tbl_content ON
select firstname,lastname,email,password,created_at,updated_at from [RazorPagesMoviesContext-2100c266-e260-4b59-8946-a50469c7f2db]..[users3]
SET IDENTITY_INSERT tbl_content Off


select * from users
insert into users
select firstname,lastname,email,password,created_at,updated_at from [RazorPagesMoviesContext-2100c266-e260-4b59-8946-a50469c7f2db]..[users3]







sp_help companies 


EXEC sp_rename 'Expansions.cover_id', 'coverexpansionCover_id', 'COLUMN';




DROP TABLE [dbo].[GamePlatforms] 
CREATE TABLE [dbo].[GamePlatforms] (
    [gamePlatform_id] INT           NOT NULL  identity(1,1),
    [gameId]          INT           NOT NULL,
    [platform_id]     INT           NOT NULL,
    [created_at]      DATETIME2 (7) NOT NULL,
    [updated_at]      DATETIME2 (7) NOT NULL
);
GO
CREATE NONCLUSTERED INDEX [IX_GamePlatforms_gameId]
    ON [dbo].[GamePlatforms]([gameId] ASC);
GO
CREATE NONCLUSTERED INDEX [IX_GamePlatforms_platform_id]
    ON [dbo].[GamePlatforms]([platform_id] ASC);
GO
ALTER TABLE [dbo].[GamePlatforms]
    ADD CONSTRAINT [PK_GamePlatforms] PRIMARY KEY CLUSTERED ([gamePlatform_id] ASC);
GO
ALTER TABLE [dbo].[GamePlatforms]
    ADD CONSTRAINT [FK_GamePlatforms_Games_gameId] FOREIGN KEY ([gameId]) REFERENCES [dbo].[Games] ([gameId]) ON DELETE CASCADE;
GO
ALTER TABLE [dbo].[GamePlatforms]
    ADD CONSTRAINT [FK_GamePlatforms_Platforms_platform_id] FOREIGN KEY ([platform_id]) REFERENCES [dbo].[Platforms] ([platform_id]) ON DELETE CASCADE;




CREATE TABLE [dbo].[ExpansionCover] (
    [expansionCover_id]  INT  NOT NULL identity(1,1) ,
    [gameid]  INT  NULL,  
    [expansion_id]   INT            NULL,
    [image_id]     INT             NULL,
    [created_at] DATETIME2 (7)  NOT NULL,
    [updated_at] DATETIME2 (7)  NOT NULL
);





drop database GameClubDB
-----------------------------------------------------------------------------------
-----------------------------------------------------------------------------------
create database GameClubDB




USE [GameclubDB]
GO

CREATE TABLE [dbo].[Users] (
    [user_id]    INT            NOT NULL  identity(1,1),
    [firstname]  NVARCHAR (30)  NOT NULL,
    [lastname]   NVARCHAR (30)  NOT NULL,
    [email]      NVARCHAR (MAX) NOT NULL,
    [password]   NVARCHAR (MAX) NOT NULL,
    [created_at] DATETIME2 (7)  NOT NULL,
    [updated_at] DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([user_id] ASC)
);


CREATE TABLE [dbo].[Games] (
    [gameId]       INT            NOT NULL  ,
    [name]         NVARCHAR (MAX) NULL,
    [rating]       FLOAT (53)     NOT NULL,
    [popularity]   FLOAT (53)     NOT NULL,
    [summary]      NVARCHAR (MAX) NULL,
    [user_id]      INT            NOT NULL,
    [release_date] DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_Games] PRIMARY KEY CLUSTERED ([gameId] ASC)
);

drop table [GameCompanies]
CREATE TABLE [dbo].[GameCompanies] (
    [gameCompany_id]       INT            NOT NULL identity(1,1) ,
    [company_id]         INT	 NULL,
    [gameId]       INT     NOT NULL,
    [created_at] DATETIME2 (7)  NOT NULL,
    [updated_at] DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_GameCompanies] PRIMARY KEY CLUSTERED ([gameCompany_id] ASC)
)



CREATE TABLE [dbo].[Genres] (
    [genre_id]   INT            NOT NULL  ,
    [name]       NVARCHAR (MAX) NULL,
    [slug]       NVARCHAR (MAX) NULL,
    [created_at] DATETIME2 (7)  NOT NULL,
    [updated_at] DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_Genres] PRIMARY KEY CLUSTERED ([genre_id] ASC)
);



CREATE TABLE [dbo].[Companies] (
    [companyId]  INT            NOT NULL  ,
    [name]       NVARCHAR (MAX) NULL,
    [created_at] DATETIME2 (7)  NOT NULL,
    [updated_at] DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED ([companyId] ASC)
);


CREATE TABLE [dbo].[Platforms] (
    [platform_id] INT            NOT NULL ,
    [name]        NVARCHAR (MAX) NULL,
    [created_at]  DATETIME2 (7)  NOT NULL,
    [updated_at]  DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_Platforms] PRIMARY KEY CLUSTERED ([platform_id] ASC)
);

CREATE TABLE [dbo].[ReviewResponse] (
    [reviewResponse_id] INT            NOT NULL,
    [response]          INT           NOT NULL,
    [user_id]           INT           NOT NULL,
    [review_id]         INT           NOT NULL,
    [created_at]        DATETIME2 (7) NOT NULL,
    [updated_at]        DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_ReviewResponse] PRIMARY KEY CLUSTERED ([reviewResponse_id] ASC)
);

CREATE TABLE [dbo].[Screenshots] (
    [screenshot_id] INT            NOT NULL ,
    [gameId]        INT            NOT NULL,
    [image_id]      NVARCHAR (MAX) NULL,
    [created_at]    DATETIME2 (7)  NOT NULL,
    [updated_at]    DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_Screenshots] PRIMARY KEY CLUSTERED ([screenshot_id] ASC),
    CONSTRAINT [FK_Screenshots_Games_gameId] FOREIGN KEY ([gameId]) REFERENCES [dbo].[Games] ([gameId]) ON DELETE CASCADE
);
GO
CREATE NONCLUSTERED INDEX [IX_Screenshots_gameId]
    ON [dbo].[Screenshots]([gameId] ASC);

CREATE TABLE [dbo].[Videos] (
    [TheVideo_id] INT            NOT NULL ,
    [gameId]      INT            NOT NULL,
    [name]        NVARCHAR (MAX) NULL,
    [video_id]    NVARCHAR (MAX) NULL,
    [created_at]  DATETIME2 (7)  NOT NULL,
    [updated_at]  DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_Videos] PRIMARY KEY CLUSTERED ([TheVideo_id] ASC),
    CONSTRAINT [FK_Videos_Games_gameId] FOREIGN KEY ([gameId]) REFERENCES [dbo].[Games] ([gameId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Videos_gameId]
    ON [dbo].[Videos]([gameId] ASC);







CREATE TABLE [dbo].[Covers] (
    [cover_id]   INT            NOT NULL ,
    [gameId]     INT            NOT NULL,
    [image_id]   NVARCHAR (MAX) NULL,
    [created_at] DATETIME2 (7)  NOT NULL,
    [updated_at] DATETIME2 (7)  NOT NULL
);

GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Covers_gameId]
    ON [dbo].[Covers]([gameId] ASC);
GO
ALTER TABLE [dbo].[Covers]
    ADD CONSTRAINT [PK_Covers] PRIMARY KEY CLUSTERED ([cover_id] ASC);
GO
ALTER TABLE [dbo].[Covers]
    ADD CONSTRAINT [FK_Covers_Games_gameId] FOREIGN KEY ([gameId]) REFERENCES [dbo].[Games] ([gameId]) ON DELETE CASCADE;



CREATE TABLE [dbo].[InvolvedCompanies] (
    [involveCompany_id] INT           NOT NULL ,
    [companyId]         INT           NOT NULL,
    [gameId]            INT           NOT NULL,
    [created_at]        DATETIME2 (7) NOT NULL,
    [updated_at]        DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_InvolvedCompanies] PRIMARY KEY CLUSTERED ([involveCompany_id] ASC),
    CONSTRAINT [FK_InvolvedCompanies_Companies_companyId] FOREIGN KEY ([companyId]) REFERENCES [dbo].[Companies] ([companyId]) ON DELETE CASCADE,
    CONSTRAINT [FK_InvolvedCompanies_Games_gameId] FOREIGN KEY ([gameId]) REFERENCES [dbo].[Games] ([gameId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_InvolvedCompanies_companyId]
    ON [dbo].[InvolvedCompanies]([companyId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_InvolvedCompanies_gameId]
    ON [dbo].[InvolvedCompanies]([gameId] ASC);






CREATE TABLE [dbo].[Expansions] (
    [id]         INT            NOT NULL ,
    [name]       NVARCHAR (MAX) NULL,
    [cover_id]   INT            NULL,
    [gameId]     INT            NOT NULL,
    [created_at] DATETIME2 (7)  NOT NULL,
    [updated_at] DATETIME2 (7)  NOT NULL
);


CREATE TABLE [dbo].[GameGenres] (
    [gameGenre_id] INT           NOT NULL ,
    [gameId]       INT           NOT NULL,
    [genre_id]     INT           NOT NULL,
    [created_at]   DATETIME2 (7) NOT NULL,
    [updated_at]   DATETIME2 (7) NOT NULL
);
GO
CREATE NONCLUSTERED INDEX [IX_GameGenres_gameId]
    ON [dbo].[GameGenres]([gameId] ASC);
GO
CREATE NONCLUSTERED INDEX [IX_GameGenres_genre_id]
    ON [dbo].[GameGenres]([genre_id] ASC);
GO
ALTER TABLE [dbo].[GameGenres]
    ADD CONSTRAINT [PK_GameGenres] PRIMARY KEY CLUSTERED ([gameGenre_id] ASC);
GO
ALTER TABLE [dbo].[GameGenres]
    ADD CONSTRAINT [FK_GameGenres_Games_gameId] FOREIGN KEY ([gameId]) REFERENCES [dbo].[Games] ([gameId]) ON DELETE CASCADE;
GO
ALTER TABLE [dbo].[GameGenres]
    ADD CONSTRAINT [FK_GameGenres_Genres_genre_id] FOREIGN KEY ([genre_id]) REFERENCES [dbo].[Genres] ([genre_id]) ON DELETE CASCADE;

CREATE TABLE [dbo].[GamePlatforms] (
    [gamePlatform_id] INT           NOT NULL,
    [gameId]          INT           NOT NULL,
    [platform_id]     INT           NOT NULL,
    [created_at]      DATETIME2 (7) NOT NULL,
    [updated_at]      DATETIME2 (7) NOT NULL
);
GO
CREATE NONCLUSTERED INDEX [IX_GamePlatforms_gameId]
    ON [dbo].[GamePlatforms]([gameId] ASC);
GO
CREATE NONCLUSTERED INDEX [IX_GamePlatforms_platform_id]
    ON [dbo].[GamePlatforms]([platform_id] ASC);
GO
ALTER TABLE [dbo].[GamePlatforms]
    ADD CONSTRAINT [PK_GamePlatforms] PRIMARY KEY CLUSTERED ([gamePlatform_id] ASC);
GO
ALTER TABLE [dbo].[GamePlatforms]
    ADD CONSTRAINT [FK_GamePlatforms_Games_gameId] FOREIGN KEY ([gameId]) REFERENCES [dbo].[Games] ([gameId]) ON DELETE CASCADE;
GO
ALTER TABLE [dbo].[GamePlatforms]
    ADD CONSTRAINT [FK_GamePlatforms_Platforms_platform_id] FOREIGN KEY ([platform_id]) REFERENCES [dbo].[Platforms] ([platform_id]) ON DELETE CASCADE;







