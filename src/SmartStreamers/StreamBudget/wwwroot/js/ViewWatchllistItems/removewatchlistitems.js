$(".remove-watchlist-series-btn").click(function () {
    let itemToRemoveImdbId = $(this).attr('id').substring(22);
    const values = getInfoRequiredToDeleteWatchlistItem(itemToRemoveImdbId);

    $.ajax({
        method: "POST",
        url: `/api/watchlistinfo/remove/series/${values.imdbId}/${values.watchlistId}`,
        contentType: "application/json; charset=UTF-8",
        data: JSON.stringify(values),
        success: watchlistItemSuccessfullyRemoved,
        error: failedWatchlistItemRemoval
    });
});

function getInfoRequiredToDeleteWatchlistItem(itemToRemoveimdbId) {
    let curWatchlistId = $("#watchlist-id").text();
    curWatchlistId = Number(curWatchlistId);

    return {
        imdbId: itemToRemoveimdbId,
        watchlistId: curWatchlistId
    }
}

function watchlistItemSuccessfullyRemoved(data) {
    $(`#watchlist-entry-${data}`).empty();
    $(`#watchlist-entry-${data}`).remove();
}


function failedWatchlistItemRemoval() {

    $(`.watchlist-crud-alert`).remove();

    let alertToDisplay = `
    <div class="alert alert-danger watchlist-crud-alert" id="error-saving-series-to-watchlist-notification" role="alert">
        Failed to remove item from watchlist.
    </div>`
        ;
    $(`body`).append(alertToDisplay);
    const clearAlertTimeout = setTimeout(clearNotification, 3000, `error-saving-series-to-watchlist-notification`);
}
