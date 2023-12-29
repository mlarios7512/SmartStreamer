using Newtonsoft.Json.Linq;
using StreamBudget.Models.DTO.StreamAvail;

namespace UnitTests;

public class Tests
{
    [Test]
    public void FromJson_WithOneResult_ShouldParseCorrectly()
    {
        //Arrange
        const string API_Response = @"{
            ""result"": [
                {
                    ""type"": ""series"",
                    ""title"": ""TV-Show-1"",
                    ""overview"": ""The most cliche statement..."",
                    ""firstAirYear"": 1996,
                    ""lastAirYear"": 2006,
                    ""imdbId"": ""tt2379308""
                    ""imdbRating"": 82,
                    ""backdropURLs"":[
                        ""original"": ""theimagetosee""
                    ]
                    ""advisedMinimumAudienceAge"": 16,
                    ""seasonCount"": 3,
                    ""episodeCount"": 41,
                        ""episodeRuntimes"": [
                        28
                    ]



                }
            ]
        }";

        //Act
        IEnumerable<SearchResultDTO> ParsedJson = SearchResultDTO.Parse_GetAllTVShows(API_Response);

        //Assert
        foreach (SearchResultDTO i in ParsedJson)
        {
            Assert.That(i.Type.Equals("series"));
            Assert.That(i.Title.Equals("TV-Show-1"));
            Assert.That(i.Overview.Equals("The most cliche statement..."));
            Assert.That(i.FirstAirYear.Equals(1996));
            Assert.That(i.LastAirYear.Equals(2006));
            Assert.That(i.ImdbId.Equals("tt2379308"));

            Assert.That(i.ImdbRating.Equals(82));
            Assert.That(i.BackdropURL.Equals("theimagetosee"));
            Assert.That(i.AdvisedMinimumAudienceAge.Equals(16));
            Assert.That(i.SeasonCount.Equals(3));
            Assert.That(i.EpisodeCount.Equals(41));
            Assert.That(i.Runtime.Equals(28));
        }
    }

    [Test]
    public void FromJson_WithInvalidAPIResponse_ShouldReturnEnumerableWithZeroItems()
    {
        IEnumerable<SearchResultDTO> ParsedJson = SearchResultDTO.Parse_GetAllTVShows("");
        Assert.That(ParsedJson.ToList().Count.Equals(0));
    }


    [Test]
    public void GetPlatformDetailsFromJson_WithOneResult_ShouldParseCorrectly()
    {
        //Arrange
        const string API_Response = @"{
            ""result"": [
                {
                    ""type"": ""series"",
                    ""title"": ""The robot"",
                    ""streamingInfo"": {
                        ""us"": {
                          ""prime"": [
                            {
                              ""type"": ""subscription""
                            }
                          ]
                        }
                      },
                    ""firstAirYear"": 1996,
                    ""backdropURLs"":[
                        ""original"": ""theimagetosee""
                    ]
                }
            ]
        }";

        //Act
        IEnumerable<SearchResultDTO> SeriesSearchResults = SearchResultDTO.Parse_GetAllTVShows(API_Response);

        //Assert
        foreach (SearchResultDTO mediaItem in SeriesSearchResults)
        {
            Assert.That(mediaItem.StreamingInfo.ElementAtOrDefault((int)Platform.Apple).AvailableOnSubscription == false);
            Assert.That(mediaItem.StreamingInfo.ElementAtOrDefault((int)Platform.Disney).AvailableOnSubscription == false);
            Assert.That(mediaItem.StreamingInfo.ElementAtOrDefault((int)Platform.HBO).AvailableOnSubscription == false);
            Assert.That(mediaItem.StreamingInfo.ElementAtOrDefault((int)Platform.Hulu).AvailableOnSubscription == false);
            Assert.That(mediaItem.StreamingInfo.ElementAtOrDefault((int)Platform.Netflix).AvailableOnSubscription == false);
            Assert.That(mediaItem.StreamingInfo.ElementAtOrDefault((int)Platform.Paramount).AvailableOnSubscription == false);
            Assert.That(mediaItem.StreamingInfo.ElementAtOrDefault((int)Platform.Prime).AvailableOnSubscription == true);
        }
    }

    [Test]
    public void GetPlatformDetailsFromJson_WithNoKnownPlatformsResponse_ShouldReturnNoAvailableSubscriptions()
    {
        //Arrange
        const string API_Response = @"{
            ""result"": [
                {
                    ""type"": ""series"",
                    ""title"": ""The Runaround"",
                    ""overview"": ""simple overview"",
                    ""backdropURLs"":[
                        ""original"": ""theimagetosee""
                    ]
                    ""seasonCount"": 3,
                    ""episodeCount"": 41,
                        ""episodeRuntimes"": [
                        28
                    ]
                }
            ]
        }";

        //Act
        IEnumerable<SearchResultDTO> SeriesSearchResults = SearchResultDTO.Parse_GetAllTVShows(API_Response);

        //Assert
        foreach (SearchResultDTO mediaItem in SeriesSearchResults)
        {
            foreach (StreamingPlatformDTO platformInfo in mediaItem.StreamingInfo)
            {
                Assert.That(platformInfo.AvailableOnSubscription == false);
                Assert.That(platformInfo.PlatformName != "(???)");
            }
        }
    }


    [Test]
    public void GetPlatformDetailsFromJson_WithOneSubscriptionResult_ShouldReturnListWithOneMatchingSubscription()
    {
        //Arrange
        const string API_Response = @"{
            ""result"": [
                {
                    ""type"": ""series"",
                    ""title"": ""The Runaround"",
                    ""streamingInfo"": {
                                ""us"": {
                                  ""hulu"": [
                                    {
                                      ""type"": ""subscription""
                                    }
                                  ]
                                }
                    },
                    ""backdropURLs"":[
                        ""original"": ""theimagetosee""
                    ]
                }
            ]
        }";

        //Act
        IEnumerable<SearchResultDTO> SeriesSearchResults = SearchResultDTO.Parse_GetAllTVShows(API_Response);

        //Assert
        foreach (SearchResultDTO mediaItem in SeriesSearchResults)
        {
            foreach (StreamingPlatformDTO platformInfo in mediaItem.StreamingInfo)
            {
                if (platformInfo.PlatformName == "Hulu")
                {
                    Assert.That(platformInfo.AvailableOnSubscription, Is.True);
                }
                else
                {
                    Assert.That(platformInfo.AvailableOnSubscription == false);
                    Assert.That(platformInfo.PlatformName != "(???)");
                }

            }
        }
    }


    [Test]
    public void GetSeasonDetailsFromJSON_WithOneSeason_ShouldReturnListOfOneSeasonDetails()
    {

        //Arrange
        const string API_Response = @"{
            ""seasons"":[
                {
                    ""type"":""season"",
                    ""title"":""Season 1"",
                    ""episodes"":[
                        {
                            ""type"":""episode"",
                            ""title"":""pilot""
                        },
                        {
                            ""type"":""episode"",
                            ""title"":""Some second episode""
                        }
                    ]
                }
            ]
        }"
        ;

        JObject? jObject = JObject.Parse((string)API_Response);
        List<JToken> seasonsInTVSeries = jObject.SelectToken("seasons").ToList();

        //Act
        List<SeasonDetailsDTO> SeasonInfo = SeasonDetailsDTO.Parse_GetSeasonSpecificDetails(seasonsInTVSeries);

        //Assert
        Assert.That(SeasonInfo.Count == 1);
        Assert.That(SeasonInfo.First().OfficialName == "Season 1");
        Assert.That(SeasonInfo.First().EpisodeCount == 2);
    }

    [Test]
    public void GetSeasonDetailsFromJSON_WithTwoSeasons_ShouldReturnListOfTwoSeasonDetails()
    {

        //Arrange
        const string API_Response = @"{
                ""seasons"":[
                    {
                        ""type"":""season"",
                        ""title"":""Season 1"",
                        ""episodes"":[
                            {
                                ""type"":""episode"",
                                ""title"":""pilot""
                            },
                            {
                                ""type"":""episode"",
                                ""title"":""Fried Delicacy""
                            }
                        ]
                    },
                    {
                        ""type"":""season"",
                        ""title"":""Season 2"",
                        ""episodes"":[
                            {
                                ""type"":""episode"",
                                ""title"":""Good Business""
                            }
                        ]
                    }
                ]
        }"
        ;

        JObject? jObject = JObject.Parse((string)API_Response);
        List<JToken> myScope = jObject.SelectToken("seasons").ToList();

        //Act
        List<SeasonDetailsDTO> SeasonInfo = new List<SeasonDetailsDTO>();
        SeasonInfo = SeasonDetailsDTO.Parse_GetSeasonSpecificDetails(myScope);

        //Assert
        Assert.That(SeasonInfo.Count == 2);
        Assert.That(SeasonInfo.First().OfficialName == "Season 1");
        Assert.That(SeasonInfo.First().EpisodeCount == 2);

        Assert.That(SeasonInfo.ElementAt(1).OfficialName == "Season 2");
        Assert.That(SeasonInfo.ElementAt(1).EpisodeCount == 1);
    }

}