function enableWatchlistItemTowardTotalWatchtimeCalc(togglableItemIconElement) {
    let showId = togglableItemIconElement.attr('id').substring(36);
    let curCountedTowardWatchtimeCalc = $(`#total-watchtime-${showId}-input`).attr("name");

    if (typeof curCountedTowardWatchtimeCalc !== true) {
        $(`#total-watchtime-${showId}-input`).attr("name", "FullSeriesHoursWatchtime");
        $(`#toggle-watchtime-inclusion-btn-item-${showId}`).text("No");
        togglableItemIconElement.removeClass("excluded-from-watchlist-stats");
    }
}

function disableWatchlistItemTowardTotalWatchtimeCalc(togglableItemIconElement) {
    let showId = togglableItemIconElement.attr('id').substring(36);
    let curCountedTowardWatchtimeCalc = $(`#total-watchtime-${showId}-input`).attr("name");

    // For some browsers, `attr` is undefined; for others, `attr` is false.  Check for both.
    //Taken from the user "strager" here: https://stackoverflow.com/questions/1318076/jquery-hasattr-checking-to-see-if-there-is-an-attribute-on-an-element
    if (typeof curCountedTowardWatchtimeCalc !== 'undefined' && curCountedTowardWatchtimeCalc !== false) {
        $(`#total-watchtime-${showId}-input`).removeAttr("name");
        $(`#toggle-watchtime-inclusion-btn-item-${showId}`).text("Yes");
        togglableItemIconElement.addClass("excluded-from-watchlist-stats");
    }
}

$(".togglable-watchlist-item-indication").click(function () {

    if ($(this).hasClass("excluded-from-watchlist-stats") == false) {
        disableWatchlistItemTowardTotalWatchtimeCalc($(this));
    }
    else {
        enableWatchlistItemTowardTotalWatchtimeCalc($(this));
    }
});