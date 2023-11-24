$(function () {
    $("#watch-list-stats-form").submit(function (event) {
        event.preventDefault();
    })
});

$("#generate-watchlist-stats-btn").click(function () {
    const formValues = getWatchlistStatsFormValues();

    if (!formValues.status) {
     /*   console.log(`Form returned "false" status. Discontinuing.`)*/
        return;
    }

    let watchtimesArray = document.getElementsByName("FullSeriesHoursWatchtime");

    let totalWatchlistTimeInHours = 0;
    $.each(watchtimesArray, function (index, item) {
  /*      console.log(`item: ${item.value}`);*/
        totalWatchlistTimeInHours += parseInt(item.value);
    });

/*    console.log(`total: ${totalWatchlistTimeInHours}`)*/

    //------------

    const HOURS_PER_MONTH = 730.5;

    //Can't remember why I created 2 of these statements for "monthsToFinishAllSeries". (This lines below were copy & pasted from guest version.)
    let monthsToFinishAllSeries = Math.round(((totalWatchlistTimeInHours / formValues.tvHours) / HOURS_PER_MONTH) / 30); //NOT 100% accurate. (730 is close though.)
    monthsToFinishAllSeries = Math.round((totalWatchlistTimeInHours / formValues.tvHours) / 30); //Produces some inaccuracy (trailing decimals).

    let totalSubscriptionCosts = monthsToFinishAllSeries * formValues.monthlySubscriptionCost;

    //console.log(`Months to finish all series: ${monthsToFinishAllSeries}`);
    /*console.log(`Total subscription costs: ~ $${totalSubscriptionCosts}`);*/
    //------
    if (totalWatchlistTimeInHours <= 0) {
        totalSubscriptionCosts = formValues.monthlySubscriptionCost;
    }

    displayWatchlistStats(totalWatchlistTimeInHours, monthsToFinishAllSeries, totalSubscriptionCosts);
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
    else if ((monthsToWatchAllItems != NaN) && (monthsToWatchAllItems != Infinity)) {
        return monthsToWatchAllItems.toString();
    }
    else {
        return "(N/A)";
    }
    
}

function getTotalSubCostsDisplayValue(monthlySubCost, totalWatchlistTimeInHours) {
    if (typeof monthlySubCost == 'number' && typeof totalWatchlistTimeInHours == 'number') {
        if (monthlySubCost > 0 && totalWatchlistTimeInHours > 0) {
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