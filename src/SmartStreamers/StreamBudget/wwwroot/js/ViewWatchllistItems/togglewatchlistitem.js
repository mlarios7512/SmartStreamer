function enableWatchlistItemTowardTotalWatchtimeCalc(togglableItemIconElement) {
    let showId = togglableItemIconElement.attr('id').substring(36);
    let curCountedTowardWatchtimeCalc = $(`#total-watchtime-${showId}-input`).attr("name");

    // For some browsers, `attr` is undefined; for others, `attr` is false.  Check for both. 
    //Taken from the user "strager" here: https://stackoverflow.com/questions/1318076/jquery-hasattr-checking-to-see-if-there-is-an-attribute-on-an-element
    if (typeof curCountedTowardWatchtimeCalc !== true) {
        $(`#total-watchtime-${showId}-input`).attr("name", "FullSeriesHoursWatchtime");
        togglableItemIconElement.removeClass("disabled-togglable-watchlist-item-icon");
    }

}

function disableWatchlistItemTowardTotalWatchtimeCalc(togglableItemIconElement) {
    let showId = togglableItemIconElement.attr('id').substring(36);
    let curCountedTowardWatchtimeCalc = $(`#total-watchtime-${showId}-input`).attr("name");

    if (typeof curCountedTowardWatchtimeCalc !== 'undefined' && curCountedTowardWatchtimeCalc !== false) {
        $(`#total-watchtime-${showId}-input`).removeAttr("name");
        togglableItemIconElement.addClass("disabled-togglable-watchlist-item-icon");
    }
}

$(".togglable-watchlist-item-icon").click(function () {

    if ($(this).hasClass("disabled-togglable-watchlist-item-icon") == false) {
        console.log("disabling...");
        disableWatchlistItemTowardTotalWatchtimeCalc($(this));
        
    }
    else {
        console.log("enabling...");
        enableWatchlistItemTowardTotalWatchtimeCalc($(this));
    }
});