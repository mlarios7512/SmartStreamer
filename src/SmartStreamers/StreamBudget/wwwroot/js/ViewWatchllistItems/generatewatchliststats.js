$(function () {
    $("#watch-list-stats-form").submit(function (event) {
        event.preventDefault();
    })
});

//The function below was taken from here: https://www.geeksforgeeks.org/convert-minutes-to-hours-minutes-with-the-help-of-jquery/
function minToUserFriendlyDisplayString(mins) {
    if (typeof (mins) != 'number') {
        return null;
    }

    // getting the hours.
    let hrs = Math.floor(mins / 60);
    // getting the minutes.
    let min = mins % 60;
    // formatting the hours.
    hrs = hrs < 10 ? '0' + hrs : hrs;
    // formatting the minutes.
    min = min < 10 ? '0' + min : min;
    // returning them as a string.
    return `${hrs} hrs ${min} min`;
}

function getFullWatchlistTimeInMonths(fullWatchlistTimeInHours, formInput)
{
    const AVG_HOURS_PER_MONTH = 730.5;
    const AVG_DAYS_PER_MONTH = 30; //Not really sure if "DAYS_PER_MONTH" is what "30" stood for. (I just guessed based off working old code.)

    //Can't remember why I created 2 of these statements for "monthsToFinishAllSeries". (This lines below were copy & pasted from guest version.)
    let monthsToFinishAllSeries = Math.round(((fullWatchlistTimeInHours / formInput.tvHours) / AVG_HOURS_PER_MONTH) / AVG_DAYS_PER_MONTH); //NOT 100% accurate. (730 is close though.)
    monthsToFinishAllSeries = Math.round((fullWatchlistTimeInHours / formInput.tvHours) / AVG_DAYS_PER_MONTH); //Produces some inaccuracy (trailing decimals).
    return monthsToFinishAllSeries;
}

function calculateFullWatchlistSubscriptionCosts(monthsToCompleteFullWatchlist, monthlySubscriptionCostInput, totalWatchtimeInHours)
{
    let totalSubscriptionCosts = monthsToCompleteFullWatchlist * monthlySubscriptionCostInput;
    if (totalWatchtimeInHours <= 0 || monthsToCompleteFullWatchlist <= 0){
        totalSubscriptionCosts = monthlySubscriptionCostInput;
    }
    return totalSubscriptionCosts;
}

$("#generate-watchlist-stats-btn").click(function () {
    const formValues = getWatchlistStatsFormValues();
    if (!formValues.status) {
        return;
    }

    let watchtimeEstimates = document.getElementsByName("FullSeriesHoursWatchtime");
    let fullWatchllistEstimateInMinutes = 0;
    $.each(watchtimeEstimates, function (index, item) {
        fullWatchllistEstimateInMinutes += parseInt(item.value);
    });

    let fullWatchlistInHours = Math.round(fullWatchllistEstimateInMinutes / 60);
    let monthsToFinishFullWatchlist = getFullWatchlistTimeInMonths(fullWatchlistInHours, formValues);
    let totalSubscriptionCosts = calculateFullWatchlistSubscriptionCosts(monthsToFinishFullWatchlist, formValues.monthlySubscriptionCost, fullWatchlistInHours);


    let formattedTime = minToUserFriendlyDisplayString(fullWatchllistEstimateInMinutes);
    displayWatchlistStats(formattedTime, monthsToFinishFullWatchlist, totalSubscriptionCosts);
});



function getWatchlistStatsFormValues() {
    const form = document.getElementById("watchlist-stats-form");

    const tvHoursPerDayInput = document.getElementById("tv-hours-per-day-input");
    const streamingCostPerMonthInput = document.getElementById("streaming-cost-per-month-input");

    if (!form.checkValidity()) {
        return { status: false };
    }

    return {
        status: true,
        tvHours: Number(tvHoursPerDayInput.value),
        monthlySubscriptionCost: Number(streamingCostPerMonthInput.value)
    }
}
function getMonthsToWatchDisplayValue(monthsToWatchAllItems) {
    if (monthsToWatchAllItems < 1) {
        return "Less than 1 month";
    }
    return "~" + monthsToWatchAllItems.toString();
}

function checkIfNumIsValid(number) {
    if (typeof number != "number" || number === "undefined") {
        return false;
    }
    if (Number.isNaN(number)) {
        return false;
    }
    if (number == Infinity || number < 0) {
        return false;
    }

    return true;
}

function displayWatchlistStats(userFriendlyTimeToFinishWatchlist, monthsToWatchAllItems, totalSubCosts) {
    $("#full-watchlist-time-in-stats-modal").empty();
    $("#full-watchlist-months-to-finish-in-stats-modal").empty();
    $("#full-watchlist-total-sub-costs-in-stats-modal").empty();

    $("#full-watchlist-time-in-stats-modal").text(userFriendlyTimeToFinishWatchlist);
    if (checkIfNumIsValid(monthsToWatchAllItems) === false || checkIfNumIsValid(totalSubCosts) === false) {

        $("#full-watchlist-months-to-finish-in-stats-modal").text("(N/A)");
        $("#full-watchlist-total-sub-costs-in-stats-modal").text("(N/A)");
    }
    else {
        let monthsToWatchDisplay = getMonthsToWatchDisplayValue(monthsToWatchAllItems);
        $("#full-watchlist-months-to-finish-in-stats-modal").text(monthsToWatchDisplay);
        $("#full-watchlist-total-sub-costs-in-stats-modal").text("~$" + totalSubCosts);
    }
}