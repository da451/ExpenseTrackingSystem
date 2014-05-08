﻿GO
DROP TABLE [EXPENSES];
GO
DROP TABLE [TAGS];
GO
DROP TABLE [USERS];
GO
CREATE TABLE [USERS] (
  [USER_ID] int IDENTITY (1,1) NOT NULL
, [FIO] nchar(100) NOT NULL
, [LOGIN] nvarchar(100) NOT NULL
, [PASSWORD] nvarchar(100) NOT NULL
);
GO
ALTER TABLE [USERS] ADD CONSTRAINT [PK_USERS] PRIMARY KEY ([USER_ID]);
GO
CREATE TABLE [TAGS] (
  [TAG_ID] int IDENTITY (1,1) NOT NULL
, [USER_ID] int NOT NULL
, [NAME] nvarchar(100) NOT NULL
);
GO
ALTER TABLE [TAGS] ADD CONSTRAINT [PK_TAGS] PRIMARY KEY ([TAG_ID]);
GO
CREATE TABLE [EXPENSES] (
  [EXPENSE_ID] int IDENTITY (1,1) NOT NULL
, [SPEND] float NOT NULL
, [DATE] datetime NOT NULL
, [COMMENT] nvarchar(100) NOT NULL
, [TAG_ID] int NOT NULL
, [USER_ID] int NOT NULL
);
GO
ALTER TABLE [EXPENSES] ADD CONSTRAINT [PK_EXPENSES] PRIMARY KEY ([EXPENSE_ID]);
GO
ALTER TABLE [TAGS] ADD CONSTRAINT [FK_TAG_USER] FOREIGN KEY ([USER_ID]) REFERENCES [USERS]([USER_ID]) ON DELETE CASCADE ON UPDATE CASCADE;
GO
ALTER TABLE [EXPENSES] ADD CONSTRAINT [FK_EXPENSE_TAG] FOREIGN KEY ([TAG_ID]) REFERENCES [TAGS]([TAG_ID]) ON DELETE CASCADE ON UPDATE CASCADE;
GO
ALTER TABLE [EXPENSES] ADD CONSTRAINT [FK_EXPENSE_USER] FOREIGN KEY ([USER_ID]) REFERENCES [USERS]([USER_ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO