$(function () {
    $("#stream-avail-title-search").submit(function (event) {
        event.preventDefault();
    })
  /*  console.log("Default prevented");*/

    $("#watch-list-form").submit(function (event) {
        event.preventDefault();
    });
});


$("#stream-avail-search-btn").click(function () {

    console.log(`Title search started..`)
    const values = getTitleSearchInput();
 /*   console.log(`Title searched for:  ${values.titleName}`);*/

    if (values.status) {
    /*    console.log("Form was valid!");*/

        $.ajax({
            type: "GET",
            dataType: "json",
            url: "/api/StreamAvail/search/title/" + values.titleName,
            success: getTitleSearchResults,
            error: errorOnAjax
        });

        //Offline testing (below)---------
 /*       getTitleSearchResults()*/
    }
    else {
        console.log("Invalid input detected in title search.");
    }

});

function errorOnAjax() {
    console.log("Error on Ajax.");
}

function getTitleSearchInput() {

    const newTitleForm = document.getElementById("stream-avail-title-search");
    const searchInput = document.getElementById(`stream-title-input`);

    if (!newTitleForm.checkValidity()) {
        return { status: false };
    }

    return {
        titleName: searchInput.value,
        status: true
    }
}

function getFullSeriesWatchtime(totalEpisodeCount, runtimePerEpisode) {
    let fullSeriesWatchtime = Math.round((totalEpisodeCount * runtimePerEpisode) / 60);
    return fullSeriesWatchtime;
}

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

function notifyUserItemIsAlreadyOnWatchlist(seriesTitle) {
    alert(`"${seriesTitle}" is already on your watchlist!`);

}

function getTitleSearchResults(data) {
/*      console.log(`Title: ${data[0]["title"]}`);*/

    $("#search-results-container").empty();
    //Hard coded version (below)-----------
    let title = `Psycho Pass`;
    let overview = `Psycho-Pass is set in a futuristic era in Japan...`;
    let firstAirYear = 2012;
    let lastAirYear = 2019;
    let suggestedAge = 16;
    let backdropURL = "https://image.tmdb.org/t/p/original/2HtnTJLs3CDUTu6ug8rib5vNnU2.jpg";
    let episodeCount = 41;
    let episodeRuntime = 28;
    let seasonCount = 3;


    let fullSeries = 19;
    let episodeLength = 28;

    $.each(data, function (index, item) {
        title = item["title"];
        overview = item["overview"];
        firstAirYear = item["firstAirYear"];
        lastAirYear = item["lastAirYear"];
        suggestedAge = item["advisedMinimumAudienceAge"];
        backdropURL = item["backdropURL"];
        episodeCount = item["episodeCount"];
        episodeRuntime = item["runtime"];
        seasonCount = item["seasonCount"];
        //---------Streaming Info (below)------------

        let platformAvailTable = `
         <table class="table text-dark">
                <thead>
                    <tr id="platform-names-for-series-${index}">
                      
                   
                    </tr>
                </thead>
                <tbody>
                    <tr id="platform-avail-status-for-series-${index}">
 

                    </tr>
                </tbody>
            </table>
    `;
        



 
        let imdbId = item["imdbId"];

        let runtimeToDisplay = `(N/A)`;
        if (episodeRuntime != null) {
            runtimeToDisplay = episodeRuntime;
        }

        let fullSeriesWatchtime = getFullSeriesWatchtime(episodeCount, episodeRuntime);

        if (fullSeriesWatchtime == null || fullSeriesWatchtime == undefined) {
            fullSeriesWatchtime = "???";
        }



        let seriesInfoToDisplay = `
                    <div class="container p-4 series-item">
                <div class="row bold-text">
                    <div class="col">
                        <h4>${title}</h4>
                    </div>
                    <div class="col">
                        <h5>${firstAirYear}-${lastAirYear}</h5>
                    </div>
                    <div class="row">
                        Recommended age: ${suggestedAge}
                    </div>
                </div>
                <img src="${backdropURL}" alt="Poster Image" width="500">
                <div class="row bold-text">
                    <div class="col-3">
                        Episodes: ${episodeCount}
                    </div>
                    <div class="col-3">
                        Seasons: ${seasonCount}
                    </div>
                </div>
                <div class="row mt-2">
                    Episode time: ${runtimeToDisplay}
                </div>
                <div class="row my-4 text-wrap">
                    ${overview}
                </div>


                <div class="accordion" id="accordionExample">
                  <div class="accordion-item">
                    <h2 class="accordion-header">
                      <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                        Watch time stats
                      </button>
                    </h2>
                    <div id="collapseOne" class="accordion-collapse collapse show" data-bs-parent="#accordionExample">
                      <div class="accordion-body text-dark">
                                
     
                                      <table class="table table-dark">
                                          <thead>
                                                <th>Description</th>
                                                <th>Time</th>
                                          </thead>
                                          <tbody>
                                            <tr class="table-active">
                                                <td>Full series (in hours)</td>
                                                <td id="full-series-watchtime-series-${imdbId}">${fullSeriesWatchtime}</td>
                                            </tr>
                                          </tbody>
                                       </table>

               
                      </div>
                    </div>
                  </div>
                  <div class="accordion-item">
                    <h2 class="accordion-header">
                      <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                        Platform Availability
                      </button>
                    </h2>
                    <div id="collapseTwo" class="accordion-collapse collapse" data-bs-parent="#accordionExample">
                      <div class="accordion-body" id="series-body-to-add-avail-table-to-${index}">
        

           

                      </div>
                    </div>
                  </div>

                </div>


                <button class="btn btn-primary mt-3" id="add-to-watchlist-btn-series-${imdbId}">Add to watch list</button>
            </div>
    `;


        $("#search-results-container").append(seriesInfoToDisplay);
        $(`#series-body-to-add-avail-table-to-${index}`).append(platformAvailTable);

        $(`#add-to-watchlist-btn-series-${imdbId}`).on("click", function () {

            let watchlistEntryExists = $(`#watchlist-entry-${imdbId}`).length;

            //0 indicates that an entry for the given show does NOT exist on the watchlist.
            //1 indicates it already exists.
            if (watchlistEntryExists == 0) {

                let entryToAdd = null;

                let firstAirToDisplayForWatchlist = ``; //Intenionally blank here.
                if (item["firstAirYear"] != null) {
                    firstAirToDisplayForWatchlist = item["firstAirYear"];
                }

                if (fullSeriesWatchtime <= 0) {

                    entryToAdd = `
                            <tr class="table-active table-danger" id="watchlist-entry-${imdbId}">
                                <td>${item["title"]} (${firstAirToDisplayForWatchlist})</td>
                                <td>(N/A)</td>
                                <td class="hidden"><input type="hidden" name="FullSeriesHoursWatchtime" value="${fullSeriesWatchtime}"></td>
                                <td><span class="hoverable-indication" id="remove-watchlist-item-${imdbId}">X</span></td>
                            </tr>
                `;

                }
                else {

                    entryToAdd = `
                            <tr class="table-active" id="watchlist-entry-${imdbId}">
                                <td>${item["title"]} (${firstAirToDisplayForWatchlist})</td>
                                <td>${fullSeriesWatchtime}</td>
                                <td class="hidden"><input type="hidden" name="FullSeriesHoursWatchtime" value="${fullSeriesWatchtime}"></td>
                                <td><span type="button" class="hoverable-indication" id="remove-watchlist-item-${imdbId}">X</span></td>
                            </tr>
                `;

                }







                //NOTE: This "watchlist" may end up serving as the "try it" version for visitors on the Identity version of the site.
                //Keep the feature basic here. For now, only include enough in each entry that will make it possible to:
                //1) Calculate a monthly plan, based on TV watched (in hours per day).
                //2) Calculate total time (in hours) of how long it would take to watch the entire watchlist.

                //Just include the "fullSeriesWatchtime". Make it '0' if it's null (also give any with 0 a special color. You don't have to let the user know for now.)
                //Add an TV hours per day input
                //Add a monthly streaming cost input (based on)
                //SHOW THE OUTPUT (after the visitor pressed a btn) in Bootstrap's "modal".

                //"No-watchtime-item" is meant to indicate that a watchtime is "0" (because the API did NOT provide one for it.)
                //NEED TO FINISH IMPLEMETNING so that the color coding is correct.





                $(`#watch-list`).append(entryToAdd);
               /* console.log("Item added!");*/
                notifyUserItemWasAddedToWatchlist(item["title"], item["imdbId"]);

                $(`#remove-watchlist-item-${imdbId}`).on("click", function () {
                 /*   console.log("REMOVING ITEM (danger)...")*/
                    $(`#watchlist-entry-${imdbId}`).empty();
                    $(`#watchlist-entry-${imdbId}`).remove();
                    $(`#remove-watchlist-item-${imdbId}`).unbind('click');
                });

            }
            else {
                notifyUserItemIsAlreadyOnWatchlist(item["title"]);
            }
        });





        //---------Streaming Info (below)------------

        let platformNameToDisplay = `<th scope="col">WHY!?</th>`;

        let availToDisplay = `<p class="fst-italic">(Unknown error)</p>`;

        $.each(item["streamingInfo"], function (subindex, subitem) {
            //console.log(`Platform #${subindex + 1}:  ${subitem["platformName"]}`);
            //console.log(`Avail status:  #${subindex + 1}:  ${subitem["availableOnSubscription"]}`);

          /*  console.log(`Type of platform name: ${typeof(subitem["platformName"])}`)*/

            if (subitem["platformName"] == null || subitem["platformName"] == undefined) {
                platformNameToDisplay = `<th scope="col" class="fst-italic">(Unknown platform)</th>`;
            }
            else {
                platformNameToDisplay = `<th scope="col">${subitem["platformName"]}</th>`;
            }

            if (subitem["availableOnSubscription"] == null || subitem["availableOnSubscription"] == undefined) {
                availToDisplay = `<td>???</td>`;
            }
            else if (subitem["availableOnSubscription"]) {
                availToDisplay = `<td> <img src="~/img/check-solid.svg" class="green-check" alt="Yes" /> </td>`;
            }
            else if (subitem["availableOnSubscription"] == false) {
                availToDisplay = `<td> <img src="~/img/xmark-solid.svg" class="red-xmark" alt="No" /> </td>`;
            }

            $(`#platform-names-for-series-${index}`).append(platformNameToDisplay);
            $(`#platform-avail-status-for-series-${index}`).append(availToDisplay);
        });

        

        //---------Streaming Info (above)------------
    });
}

