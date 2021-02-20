CREATE TABLE [userAccount] (
  [pKey] int PRIMARY KEY IDENTITY(1, 1),
  [userID] nvarchar(255) UNIQUE NOT NULL,
  [userName] nvarchar(255) UNIQUE NOT NULL,
  [membershipStatusID] int NOT NULL,
  [firstName] nvarchar(255),
  [lastName] nvarchar(255),
  [email] nvarchar(255) UNIQUE NOT NULL,
  [addressStreet] nvarchar(255),
  [addressStreet2] nvarchar(255),
  [addressCity] nvarchar(255),
  [addressState] nvarchar(255),
  [addressZipCode] nvarchar(255),
  [created_at] timestamp NOT NULL
)
GO

CREATE TABLE [membershipStatus] (
  [pKey] int PRIMARY KEY IDENTITY(1, 1),
  [membershipStatusID] int UNIQUE NOT NULL,
  [description] nvarchar(255) UNIQUE NOT NULL
)
GO

CREATE TABLE [userGamerTags] (
  [pKey] int PRIMARY KEY IDENTITY(1, 1),
  [userID] nvarchar(255) NOT NULL,
  [gamerTag] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [personalUserTrackedGames] (
  [pKey] int PRIMARY KEY IDENTITY(1, 1),
  [userID] nvarchar(255) NOT NULL,
  [gameID] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [videoGameTable] (
  [pKey] int PRIMARY KEY IDENTITY(1, 1),
  [gameID] nvarchar(255) UNIQUE NOT NULL,
  [gameTitle] nvarchar(255) NOT NULL,
  [description] nvarchar(255),
  [rating] decimal,
  [totalReviews] int
)
GO

CREATE TABLE [videoGamePlatforms] (
  [pKey] int PRIMARY KEY IDENTITY(1, 1),
  [gameID] nvarchar(255) NOT NULL,
  [platform] nvarchar(255)
)
GO

CREATE TABLE [videoGameGenres] (
  [pKey] int PRIMARY KEY IDENTITY(1, 1),
  [gameID] nvarchar(255) NOT NULL,
  [genre] nvarchar(255)
)
GO

CREATE TABLE [videoGameThemes] (
  [pKey] int PRIMARY KEY IDENTITY(1, 1),
  [gameID] nvarchar(255) NOT NULL,
  [theme] nvarchar(255)
)
GO

CREATE TABLE [videoGameKeywords] (
  [pKey] int PRIMARY KEY IDENTITY(1, 1),
  [gameID] nvarchar(255) NOT NULL,
  [keyword] nvarchar(255)
)
GO

CREATE TABLE [videoGameInvolvedCompanies] (
  [pKey] int PRIMARY KEY IDENTITY(1, 1),
  [gameID] nvarchar(255) NOT NULL,
  [involvedCompanies] nvarchar(255)
)
GO

ALTER TABLE [userAccount] ADD FOREIGN KEY ([membershipStatusID]) REFERENCES [membershipStatus] ([membershipStatusID])
GO

ALTER TABLE [userGamerTags] ADD FOREIGN KEY ([userID]) REFERENCES [userAccount] ([userID])
GO

ALTER TABLE [personalUserTrackedGames] ADD FOREIGN KEY ([userID]) REFERENCES [userAccount] ([userID])
GO

ALTER TABLE [personalUserTrackedGames] ADD FOREIGN KEY ([gameID]) REFERENCES [videoGameTable] ([gameID])
GO

ALTER TABLE [videoGamePlatforms] ADD FOREIGN KEY ([gameID]) REFERENCES [videoGameTable] ([gameID])
GO

ALTER TABLE [videoGameGenres] ADD FOREIGN KEY ([gameID]) REFERENCES [videoGameTable] ([gameID])
GO

ALTER TABLE [videoGameThemes] ADD FOREIGN KEY ([gameID]) REFERENCES [videoGameTable] ([gameID])
GO

ALTER TABLE [videoGameKeywords] ADD FOREIGN KEY ([gameID]) REFERENCES [videoGameTable] ([gameID])
GO

ALTER TABLE [videoGameInvolvedCompanies] ADD FOREIGN KEY ([gameID]) REFERENCES [videoGameTable] ([gameID])
GO
