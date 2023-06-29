$(".remove-watchlist-series-btn").click(function () {
    let imdbIdOfSeries = $(this).attr('id').substring(22);
    console.log(`Attempting to remove: ${imdbIdOfSeries}`);

    $.ajax({
        type: "POST",
        url: `/api/`,
        dataType: "json",
        contentType: "application/json; charset=UTF-8",
        data: JSON.stringify(imdbIdOfSeries),
        success: watchlistItemSuccessfullyRemoved,
        error: failedWatchlistItemRemoval
    });
});

//NEED TO RETURN THE IMDBID OF THE item removed!
function watchlistItemSuccessfullyRemoved(data) {
    console.log(`Watchlist item successfully removed!`);

    $(`#watchlist-entry-${data["imdbId"]}`).empty();
    $(`#watchlist-entry-${data["imdbId"]}`).remove();

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
