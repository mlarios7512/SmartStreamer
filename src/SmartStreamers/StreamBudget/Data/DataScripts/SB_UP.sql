CREATE TABLE [Person](
    [ID]                int             PRIMARY KEY IDENTITY(1, 1),
    [ASPNetIdentityID]  nvarchar(64)    NOT NULL,
);

--Need to update table schemes below. The current one was only meant to be used by one person on a local machine.

CREATE TABLE [Watchlist](
    [ID]                    int             PRIMARY KEY IDENTITY(1, 1),
    [Name]                  nvarchar(64)        NOT NULL,
    [StreamingPlatform]     nvarchar(64)        NULL,
    [SelectedStreamingCost] decimal(18,2)       NULL,
    [OwnerID]               int         NOT NULL
);

CREATE TABLE [WatchlistItem](
    [ID]                    int             PRIMARY KEY IDENTITY(1, 1),
    [Title]                 nvarchar(64)        NOT NULL,
    [ImdbId]                nvarchar(64)        NOT NULL,
    [FirstAirYear]          int         NOT NULL,
    [EpisodeRuntime]        int         NULL, --Avg runtime in minutes.
    [TotalEpisodeCount]     int         NULL,  --Full series
    [WatchlistID]           int         NOT NULL 
);

ALTER TABLE [Watchlist] ADD CONSTRAINT [Fk_Watchlist_Person_ID]
    FOREIGN KEY ([OwnerID]) REFERENCES [Person] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [WatchlistItem] ADD CONSTRAINT [Fk_WatchlistItem_Watchlist_ID]
    FOREIGN KEY ([WatchlistID]) REFERENCES [Watchlist] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;