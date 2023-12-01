$(function () {
    $("#watch-list-stats-form").submit(function (event) {
        event.preventDefault();
    })
});

//The function below was taken from here: https://www.geeksforgeeks.org/convert-minutes-to-hours-minutes-with-the-help-of-jquery/
function conversion(mins) {
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
    const HOURS_PER_MONTH = 730.5;

    //Can't remember why I created 2 of these statements for "monthsToFinishAllSeries". (This lines below were copy & pasted from guest version.)
    let monthsToFinishAllSeries = Math.round(((fullWatchlistTimeInHours / formInput.tvHours) / HOURS_PER_MONTH) / 30); //NOT 100% accurate. (730 is close though.)
    monthsToFinishAllSeries = Math.round((fullWatchlistTimeInHours / formInput.tvHours) / 30); //Produces some inaccuracy (trailing decimals).
    return monthsToFinishAllSeries;
}

function calculateFullWatchlistSubscriptionCosts(monthsToCompleteFullWatchlist, monthlySubscriptionCostInput, totalWatchtimeInHours)
{
    let totalSubscriptionCosts = monthsToCompleteFullWatchlist * monthlySubscriptionCostInput
    if (totalWatchtimeInHours <= 0)
    {
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

    let formattedTime = conversion(fullWatchllistEstimateInMinutes);
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

    if ((monthsToWatchAllItems !== NaN) && (monthsToWatchAllItems !== Infinity)) {
        if (monthsToWatchAllItems < 1) {
            return "Less than 1 month";
        }
        return "~" + monthsToWatchAllItems.toString();
    }
    else {
        return "(N/A)";
    }
    
}
function displayWatchlistStats(userFriendlyTimeToFinishWatchlist, monthsToWatchAllItems, totalSubCosts) {

    let stats = `
        <p><span class="fw-bold">Total watchtime:</span> <span id="full-watchlist-time-in-stats-modal"><span></p>
        <p><span class="fw-bold">Months to finish:</span> <span id="full-watchlist-months-to-finish-in-stats-modal"><span></p>
        <p><span class="fw-bold">Total subscription costs:</span> <span id="full-watchlist-total-sub-costs-in-stats-modal"><span></p>
    `;

    $("#watchlist-stats-modal-body").empty();
    $("#watchlist-stats-modal-body").append(stats);

    $("#full-watchlist-time-in-stats-modal").text(userFriendlyTimeToFinishWatchlist);

    let monthsToWatchDisplay = getMonthsToWatchDisplayValue(monthsToWatchAllItems);
    $("#full-watchlist-months-to-finish-in-stats-modal").text(monthsToWatchDisplay);
    $("#full-watchlist-total-sub-costs-in-stats-modal").text("~$" + totalSubCosts);
}