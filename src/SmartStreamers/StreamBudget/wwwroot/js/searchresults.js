$(".add-series-to-watchlist-btn").click(function () {
    let imdbIdOfSeries = $(this).attr('id').substring(32);

    const MUHvalues = getSeriesToAddInfo(imdbIdOfSeries);
    console.log(`watchlist ID (thing): ${MUHvalues.CurWatchlistId}`)
    console.log(`Title (thing): ${MUHvalues.TitleSTA}`);
    console.log(`FirstYear (thing): ${MUHvalues.FirstYearSTA}`);
    console.log(`runtime (thing): ${MUHvalues.RuntimeSTA}`);
    console.log(`totalEpisodes (thing): ${MUHvalues.TotalEpisodeCountSTA}`);

    //The error message still triggers but the HTTP response is 200. Try:
    // 1) Make sure the "$.ajax" method (below) is correct. (Even 1 mistake can trigger the error.)
    $.ajax({
        type: "POST",
        url: `/api/WatchlistInfo/add/series`,
        contentType: "application/json; charset=UTF-8",
        data: JSON.stringify(MUHvalues),
        success: displayItemSavedMsg,
        error: errorSavingItemMsg

    });
});

function getSeriesToAddInfo(imdbId) {
    let watchlistId = $(`#watchlist-id`).val();

    watchlistId = parseInt(watchlistId);
  /*  console.log(`watchlistId (type): ${typeof watchlistId}`);*/

    if (typeof watchlistId != Number)
    {
        watchlistId == "";
    }

    let title = $(`#series-title-${imdbId}`).text();
    let yearAired = $(`#first-air-year-${imdbId}`).text();
    let approxEpisodeTimes = $(`#episode-run-time-${imdbId}`).text();
    let totalEpisodesInSeries = $(`#episode-count-${imdbId}`).text();

    console.log(`epInSeries (): ${totalEpisodesInSeries}`)

    return {
        //"STA" here stands for : "Series To Add".
        CurWatchlistId: watchlistId,
        TitleSTA: title,
        ImdbIDSTA: imdbId,
        FirstYearSTA: Number(yearAired),
        RuntimeSTA: Number(approxEpisodeTimes),
        TotalEpisodeCountSTA: Number(totalEpisodesInSeries)
    }
}

function clearNotification(notificationHtmlID) {
    $(`#${notificationHtmlID}`).remove();
}

function notifyUserItemWasAddedToWatchlist(seriesTitle, seriesImdbId) {
    $(`.watchlist-crud-alert`).remove();

    let alertToDisplay = `
    <div class="alert alert-info watchlist-crud-alert" id="series-${seriesImdbId}-added-to-watchlist-notification" role="alert">
        "${seriesTitle}" added to watchlist.
    </div>`
        ;
    $(`body`).append(alertToDisplay);

    const clearAlertTimeout = setTimeout(clearNotification, 3000, `series-${seriesImdbId}-added-to-watchlist-notification`);
}

function notifyUserItemWasAlreadyInWatchlist(seriesTitle, seriesImdbId) {
    $(`.watchlist-crud-alert`).remove();

    let alertToDisplay = `
    <div class="alert alert-warning watchlist-crud-alert" id="series-${seriesImdbId}-already-in-watchlist-notification" role="alert">
        "${seriesTitle}" already in watchlist.
    </div>`
        ;
    $(`body`).append(alertToDisplay);

    const clearAlertTimeout = setTimeout(clearNotification, 3000, `series-${seriesImdbId}-already-in-watchlist-notification`);
}

//NEED TO RETURN "title" & "imdbId" of item that was removed!
function displayItemSavedMsg(data) {

    if (data["runtimeSTA"] === -304) {
        console.log("NO MODIFICATIONS NECESSARY");
        notifyUserItemWasAlreadyInWatchlist(data["titleSTA"], data["imdbIdSTA"]);

    }
    else {
        console.log(`Data (not modified) :${data}`)
        console.log(`Sub-Data returned: ${data["titleSTA"]}`);

        //NEED TO VERIFY THAT THIS WORKS!
        notifyUserItemWasAddedToWatchlist(data["titleSTA"], data["imdbIdSTA"]);

        console.log(`\n\n---------------`);
    }
}

function errorSavingItemMsg() {
    console.log(`AJAX error. Item was NOT added to watchlist.`);
    $(`.watchlist-crud-alert`).remove();

    let alertToDisplay = `
    <div class="alert alert-danger watchlist-crud-alert" id="error-saving-series-to-watchlist-notification" role="alert">
        There was an error saving to the item your watchlist.
    </div>`
        ;
    $(`body`).append(alertToDisplay);

    const clearAlertTimeout = setTimeout(clearNotification, 3000, `error-saving-series-to-watchlist-notification`);
}