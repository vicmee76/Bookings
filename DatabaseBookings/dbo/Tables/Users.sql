CREATE TABLE [dbo].[Users] (
    [UsersId]   INT           NOT NULL,
    [FirstName] VARCHAR (50)  NOT NULL,
    [LastName]  VARCHAR (50)  NOT NULL,
    [Email]     VARCHAR (200) NOT NULL,
    [Phone]     VARCHAR (50)  NULL,
    [Address]   VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UsersId] ASC)
);

