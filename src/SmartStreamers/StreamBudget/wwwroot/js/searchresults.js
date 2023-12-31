﻿$(".add-series-to-watchlist-btn").click(function () {
    let imdbIdOfSeries = $(this).attr('id').substring(32);
    const valuesToSubmit = getSeriesToAddInfo(imdbIdOfSeries);

    $.ajax({
        type: "POST",
        url: `/api/WatchlistInfo/add/series`,
        contentType: "application/json; charset=UTF-8",
        data: JSON.stringify(valuesToSubmit),
        success: displayItemSavedMsg,
        error: errorSavingItemMsg

    });
});

function getSeriesToAddInfo(imdbId) {
    let watchlistId = $(`#watchlist-id`).val();

    watchlistId = parseInt(watchlistId);

    if (typeof watchlistId != Number)
    {
        watchlistId == "";
    }

    let title = $(`#series-title-${imdbId}`).text();
    let yearAired = $(`#first-air-year-${imdbId}`).text();
    let approxEpisodeTimes = $(`#episode-run-time-${imdbId}`).text();
    let totalEpisodesInSeries = $(`#episode-count-${imdbId}`).text();

    return {
        CurWatchlistId: watchlistId,
        Title: title,
        ImdbID: imdbId,
        FirstYear: Number(yearAired),
        Runtime: Number(approxEpisodeTimes),
        TotalEpisodeCount: Number(totalEpisodesInSeries)
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

function notifyUserItemWasAlreadyInWatchlist(seriesImdbId) {
    $(`.watchlist-crud-alert`).remove();

    let alertToDisplay = `
    <div class="alert alert-warning watchlist-crud-alert" id="series-already-in-watchlist-notification" role="alert">
        Item already in watchlist.
    </div>`;
        
    $(`body`).append(alertToDisplay);

    const clearAlertTimeout = setTimeout(clearNotification, 3000, `series-already-in-watchlist-notification`);
}

function displayItemSavedMsg(data) {

    if (data == "preexisting entry") {
        notifyUserItemWasAlreadyInWatchlist();
    }
    else {
        notifyUserItemWasAddedToWatchlist(data["title"], data["imdbId"]);
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