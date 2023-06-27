CREATE TABLE [Person](
    [ID]                int             PRIMARY KEY IDENTITY(1, 1),
    [ASPNetIdentityID]  nvarchar(64)    NOT NULL,
);

CREATE TABLE [StreamingPlatform](
    [ID]                        int             PRIMARY KEY IDENTITY(1, 1),
    [Name]                      NVARCHAR(64)    NOT NULL,
    [SelectedStreamingCost]     DECIMAL(18,2)   NULL,         --Unfinished. Look into decimal types later.
    [FullWatchlistTime]         int    NULL,   --# of hours it will take to complete all series within the platform's watchlist.
);
