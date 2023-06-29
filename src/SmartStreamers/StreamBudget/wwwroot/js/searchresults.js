$(".add-series-to-watchlist-btn").click(function () {
    let imdbIdOfSeries = $(this).attr('id').substring(28);

    $.ajax({
        type: "POST",
        url: `/api/`,
        dataType: "json",
        contentType: "application/json; charset=UTF-8",
        data: JSON.stringify(imdbIdOfSeries),
        success: displayItemSavedMsg,
        error: errorSavingItemMsg

    });
});

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


function displayItemSavedMsg(data) {
    console.log(`displayItemSavedMsg`);

    //NEED TO VERIFY THAT THIS WORKS!
  /*  displayItemSavedMsg(data["title"], data["imdbId"]);*/
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