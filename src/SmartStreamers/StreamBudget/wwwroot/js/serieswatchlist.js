$(".remove-watchlist-series-btn").click(function () {
    let imdbId = $(this).attr('id').substring(22);
    console.log(`Attempting to remove: ${imdbId}`);

    $.ajax({
        method: "POST",
        url: `/api/watchlistinfo/remove/series/${imdbId}`,
        contentType: "application/json; charset=UTF-8",
        data: String(imdbId),
        success: watchlistItemSuccessfullyRemoved,
        error: failedWatchlistItemRemoval
    });
});

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
