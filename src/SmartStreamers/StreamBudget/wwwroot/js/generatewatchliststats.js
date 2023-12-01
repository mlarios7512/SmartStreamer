$(function () {
    $("#watch-list-stats-form").submit(function (event) {
        event.preventDefault();
    })
});

//------------NEW DISPLAY, not yet implemented (below)---------------------

//The function below was taken from here: https://www.geeksforgeeks.org/convert-minutes-to-hours-minutes-with-the-help-of-jquery/
function conversion(mins) { //Might be the new "getTotalWatchtimeInHoursDisplayValue()";
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

//This code below probably doesn't need an entire function for it. Just doing this for now
//because it helps me think.
function calculateFullWatchlistEstimateOfMonths_NEW(formattedTotalWatchtime, formInput) //Intended to be the new "getMonthsToWatchDisplayValue()".
{
    let hours = formattedTotalWatchtime.substring(0, formattedTotalWatchtime.indexOf('h'));
    let minutes = formattedTotalWatchtime.substring(formattedTotalWatchtime.indexOf('s')+1, formattedTotalWatchtime.indexOf('m'))
    console.log(`hours (as number): ${Number(hours)}`);
/*    console.log(`minutes (as number): ${Number(minutes)}`);*/

    const HOURS_PER_MONTH = 730.5;
    let totalWatchlistTimeInHours = hours;
    ////Can't remember why I created 2 of these statements for "monthsToFinishAllSeries". (This lines below were copy & pasted from guest version.)
    let monthsToFinishAllSeries = Math.round(((totalWatchlistTimeInHours / formInput.tvHours) / HOURS_PER_MONTH) / 30); //NOT 100% accurate. (730 is close though.)
    monthsToFinishAllSeries = Math.round((totalWatchlistTimeInHours / formInput.tvHours) / 30); //Produces some inaccuracy (trailing decimals).

/*    console.log(`Total months to finish watchlist: ${monthsToFinishAllSeries}`);*/
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
        console.log(`Form returned "false" status. Discontinuing.`)
        return;
    }

    let watchtimeEstimates = document.getElementsByName("FullSeriesHoursWatchtime");
    let fullWatchllistEstimateInMinutes = 0;
    $.each(watchtimeEstimates, function (index, item) {
        /*      console.log(`item: ${item.value}`);*/
        fullWatchllistEstimateInMinutes += parseInt(item.value);
    });


    let formattedTime = conversion(fullWatchllistEstimateInMinutes);
    let monthsToFinishFullWatchlist = calculateFullWatchlistEstimateOfMonths_NEW(formattedTime, formValues);
    console.log(`\n\n------------------\n`)

    let fullWatchlistInHours = Math.round(fullWatchllistEstimateInMinutes / 60);
    console.log(`Total hours to watch (rounded): ${fullWatchlistInHours}`);
    console.log(`months to finish: ${monthsToFinishFullWatchlist}`);

    //Need to get the total number of hours watched (for the last parameter).
    let totalSubscriptionCosts = calculateFullWatchlistSubscriptionCosts(monthsToFinishFullWatchlist, formValues.monthlySubscriptionCost, Math.round(fullWatchllistEstimateInMinutes / 60));
    console.log(`Total subscription costs: ${totalSubscriptionCosts}`);

    displayWatchlistStats(fullWatchlistInHours, monthsToFinishFullWatchlist, totalSubscriptionCosts);
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
function getTotalWatchtimeInHoursDisplayValue(totalWatchtimeInHours) {
    if (typeof (totalWatchtimeInHours) == 'number') {
        return totalWatchtimeInHours.toString();
    }
    return "(N/A)";
}
function getMonthsToWatchDisplayValue(monthsToWatchAllItems) {
    if (monthsToWatchAllItems < 1) {
        return "Less than 1 month";
    }
    else if ((monthsToWatchAllItems != NaN) && (monthsToWatchAllItems != Infinity) && (monthsToWatchAllItems != NaN)) {
        return monthsToWatchAllItems.toString();
    }
    else {
        return "(N/A)";
    }
    
}

function getTotalSubCostsDisplayValue(monthlySubCost, totalWatchlistTimeInHours) {
    if (typeof monthlySubCost == 'number' && typeof totalWatchlistTimeInHours == 'number') {
        if ((monthlySubCost > 0) && (totalWatchlistTimeInHours > 0) && (monthlySubCost !== Infinity)) {
            return "$" + monthlySubCost.toString();
        }
    }
    return "(N/A)";
}

function displayWatchlistStats(totalWatchtimeInHours, monthsToWatchAllItems, totalSubCosts) {
    totalWatchtimeInHours = Number(totalWatchtimeInHours);
    monthsToWatchAllItems = Number(monthsToWatchAllItems);
    totalSubCosts = Number(totalSubCosts);

    let totalWatchtimeInHoursDisplay = "(N/A)";
    let monthsToWatchDisplay = "(N/A)";
    let totalSubCostsDisplay = "(N/A)";


    totalWatchtimeInHoursDisplay = getTotalWatchtimeInHoursDisplayValue(totalWatchtimeInHours);
    monthsToWatchDisplay = getMonthsToWatchDisplayValue(monthsToWatchAllItems);
    totalSubCostsDisplay = getTotalSubCostsDisplayValue(totalSubCosts, totalWatchtimeInHours);
   

    let stats = `
        <p><span class="fw-bold">Total hours to watch:</span> ${totalWatchtimeInHoursDisplay}</p>
        <p><span class="fw-bold">Months to finish:</span> ${monthsToWatchDisplay}</p>
        <p><span class="fw-bold">Total subscription costs:</span> ${totalSubCostsDisplay}</p>
    `;

    $("#watchlist-stats-modal-body").empty();
    $("#watchlist-stats-modal-body").append(stats);
}