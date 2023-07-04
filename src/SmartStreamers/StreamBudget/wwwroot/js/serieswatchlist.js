$(".remove-watchlist-series-btn").click(function () {
    let itemToRemoveImdbId = $(this).attr('id').substring(22);
    const values = getInfoRequiredToDeleteWatchlistItem(itemToRemoveImdbId);

    console.log(`imdbID: ${values.imdbId}`);
    console.log(`watchlistId: ${values.watchlistId}`);

    console.log(`Attempting to remove: ${itemToRemoveImdbId}`);

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
    console.log(`watchlistId (getInfo): ${curWatchlistId}`)

    return {
        imdbId: itemToRemoveimdbId,
        watchlistId: curWatchlistId
    }
}

//NEED TO RETURN THE IMDBID OF THE item removed!
function watchlistItemSuccessfullyRemoved(data) {

    if (data == "tt-ERROR-DELETION") {
        failedWatchlistItemRemoval();
    }
    else
    {
        console.log(`Watchlist item successfully removed!`);

        console.log(`'Data' returned: ${data}`);

        $(`#watchlist-entry-${data}`).empty();
        $(`#watchlist-entry-${data}`).remove();
    }

}


function failedWatchlistItemRemoval() {
    console.log(`Error removing watchlist item`);

    $(`.watchlist-crud-alert`).remove();

    let alertToDisplay = `
    <div class="alert alert-danger watchlist-crud-alert" id="error-saving-series-to-watchlist-notification" role="alert">
        Failed to remove item from watchlist.
    </div>`
        ;
    $(`body`).append(alertToDisplay);

    const clearAlertTimeout = setTimeout(clearNotification, 3000, `error-saving-series-to-watchlist-notification`);
}
