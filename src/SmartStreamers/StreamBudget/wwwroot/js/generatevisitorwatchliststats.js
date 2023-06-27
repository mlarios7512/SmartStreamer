$("#generate-watchlist-stats-btn").click(function () {
    const fullWatchtimes = document.getElementsByName('FullSeriesHoursWatchtime');
    const tvHoursPerDay = document.getElementById('tv-hours-per-day-input');
    const streamingCostPerMonthInput = document.getElementById('streaming-cost-per-month');

    const HOURS_PER_MONTH = 730.5;



    let totalWatchlistWatchtimeInHours = null;
    $.each(fullWatchtimes, function (index, item) {
        totalWatchlistWatchtimeInHours += parseInt(item.value);
    });

    let monthsToFinishAllSeries = Math.round(((totalWatchlistWatchtimeInHours / tvHoursPerDay.value) / HOURS_PER_MONTH) / 30); //NOT 100% accurate. (730 is close though.)
    monthsToFinishAllSeries = Math.round((totalWatchlistWatchtimeInHours / tvHoursPerDay.value) / 30); //Produces some inaccuracy (trailing decimals).
    /*    monthsToFinishAllSeries = Math.round(monthsToFinishAllSeries);*/


    let totalSubscriptionCosts = monthsToFinishAllSeries * streamingCostPerMonthInput.value; //NEEDS FIXING! (Should be diplayed in USD).

    console.log(`monthsToFinishAllSeries: ${monthsToFinishAllSeries}`);
    console.log(`Streaming Cost per month: ${streamingCostPerMonthInput.value}`);




    //------------------DISPLAYING STATS (below)--------------------

    let monthsNeededToDisplay = null;
    let totalSubCostToDisplay = null;
    if (monthsToFinishAllSeries <= 0 && totalWatchlistWatchtimeInHours > 0) {
        monthsNeededToDisplay = "Less than 1 month";
        totalSubCostToDisplay = `$${streamingCostPerMonthInput.value.toString()}`;
    }
    else {
        monthsNeededToDisplay = `~${monthsToFinishAllSeries} month(s)`;
        totalSubCostToDisplay = `$${totalSubscriptionCosts.toString()}`;
    }

    let allStatsToDisplay = `
        <p><span class="fw-bold">Total hours to watch:</span> ${totalWatchlistWatchtimeInHours}</p>
        <p><span class="fw-bold">Months to finish:</span> ${monthsNeededToDisplay}</p>
        <p><span class="fw-bold">Total subscription costs:</span> ${totalSubCostToDisplay}</p>
        
    
    `;

    $("#watchlist-stats-display").empty();


    $(`#watchlist-stats-display`).append(allStatsToDisplay)
  /*  $(`#watchlist-stats-display`)*/


});