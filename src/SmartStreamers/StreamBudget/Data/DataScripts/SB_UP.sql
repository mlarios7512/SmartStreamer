CREATE TABLE [Person](
    [ID]                int             PRIMARY KEY IDENTITY(1, 1),
    [ASPNetIdentityID]  nvarchar(64)    NOT NULL,
);


CREATE TABLE [Watchlist](
    [ID]                    int             PRIMARY KEY IDENTITY(1, 1),
    [Name]                  nvarchar(64)        NOT NULL,
    [StreamingPlatform]     nvarchar(64)        NULL,  --Name of streaming platform this watchlist is meant for.
    [SelectedStreamingCost] decimal(18,2)       NULL,  --Monthly subscription cost attached to the watchlist.
    [OwnerID]               int         NOT NULL
);

CREATE TABLE [WatchlistItem](
    [ID]                    int             PRIMARY KEY IDENTITY(1, 1),
    [ImdbId]                nvarchar(64)        NOT NULL,
    [FirstAirYear]          int         NOT NULL,
    [EpisodeRuntime]        int         NULL, --Should be in minutes
    [TotalEpisodeCount]     int         NULL,  --Full series
    [WatchlistID]           int         NOT NULL 
);

ALTER TABLE [Watchlist] ADD CONSTRAINT [Fk_Watchlist_Person_ID]
    FOREIGN KEY ([OwnerID]) REFERENCES [Person] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [WatchlistItem] ADD CONSTRAINT [Fk_WatchlistItem_Watchlist_ID]
    FOREIGN KEY ([WatchlistID]) REFERENCES [Watchlist] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;