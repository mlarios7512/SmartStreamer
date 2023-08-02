using Newtonsoft.Json.Linq;
using StreamBudget.Models.DTO.StreamAvail;

namespace UnitTests;

public class Tests
{
    [Test]
    public void FromJson_WithOneResult_ShouldParseCorrectly()
    {
        //Arrange
        string responseFromAPI = @"{
            ""result"": [
                {
                    ""type"": ""series"",
                    ""title"": ""TV-Show-1"",
                    ""overview"": ""simple overview"",
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

        string correctType = "series";
        string correctTitle = "TV-Show-1";
        string correctOverview = "simple overview";

        int correctImdbRating = 82;
        string correctImdbId = "tt2379308";
        int correctFirstAir = 1996;
        int correctLastAir = 2006;


        string correctBackdropURL = "theimagetosee";
        int correctMinAudience = 16;
        int correctSeasonCount = 3;
        int correctEpisodeCount = 41;
        int correctEpisodeRuntimes = 28;


        //Act
        IEnumerable<SearchResultDTO> ParsedJson = SearchResultDTO.FromJSON(responseFromAPI);

        //Assert
        foreach (SearchResultDTO i in ParsedJson)
        {
            Assert.That(i.Type.Equals(correctType));
            Assert.That(i.Title.Equals(correctTitle));
            Assert.That(i.Overview.Equals(correctOverview));
            Assert.That(i.FirstAirYear.Equals(correctFirstAir));
            Assert.That(i.LastAirYear.Equals(correctLastAir));
            Assert.That(i.ImdbId.Equals(correctImdbId));

            Assert.That(i.ImdbRating.Equals(correctImdbRating));
            Assert.That(i.BackdropURL.Equals(correctBackdropURL));
            Assert.That(i.AdvisedMinimumAudienceAge.Equals(correctMinAudience));
            Assert.That(i.SeasonCount.Equals(correctSeasonCount));
            Assert.That(i.EpisodeCount.Equals(correctEpisodeCount));
            Assert.That(i.Runtime.Equals(correctEpisodeRuntimes));
        }
    }

    [Test]
    public void FromJson_WithInvalidAPIResponse_ShouldReturnEnumerableWithZeroItems()
    {
        //Arrange
        IEnumerable<SearchResultDTO> ParsedJson = SearchResultDTO.FromJSON("");

        //Act

        //Assert
        Assert.That(ParsedJson.ToList().Count.Equals(0));
    }


    [Test]
    public void GetPlatformDetailsFromJson_WithOneResult_ShouldParseCorrectly()
    {
        //Arrange
        string responseFromAPI = @"{
            ""result"": [
                {
                    ""type"": ""series"",
                    ""title"": ""The robot"",
                    ""overview"": ""simple overview"",
                    ""streamingInfo"": {
                        ""us"": {
                          ""prime"": [
                            {
                              ""type"": ""subscription"",
                              ""quality"": ""sd"",
                              ""link"": ""NOT_A_REAL_LINK_HERE"",
                              ""watchLink"": """",
                              ""audios"": [
                                {
                                  ""language"": ""eng"",
                                  ""region"": """"
                                }
                              ],
                              ""subtitles"": [
                                {
                                  ""locale"": {
                                    ""language"": ""eng"",
                                    ""region"": """"
                                  },
                                  ""closedCaptions"": true
                                }
                              ],
                              ""leaving"": 0,
                              ""availableSince"": 0880874391
                            }
                          ]
                        }
                      },
                    ""firstAirYear"": 1996,
                    ""lastAirYear"": 2001,
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
        IEnumerable<SearchResultDTO> SeriesSearchResults = SearchResultDTO.FromJSON(responseFromAPI);

        //Assert
        foreach (SearchResultDTO mediaItem in SeriesSearchResults)
        {

            Assert.That(mediaItem.StreamingInfo.ElementAtOrDefault(0).AvailableOnSubscription == false);
            Assert.That(mediaItem.StreamingInfo.ElementAtOrDefault(1).AvailableOnSubscription == false);
            Assert.That(mediaItem.StreamingInfo.ElementAtOrDefault(2).AvailableOnSubscription == false);
            Assert.That(mediaItem.StreamingInfo.ElementAtOrDefault(3).AvailableOnSubscription == false);
            Assert.That(mediaItem.StreamingInfo.ElementAtOrDefault(4).AvailableOnSubscription == false);
            Assert.That(mediaItem.StreamingInfo.ElementAtOrDefault(5).AvailableOnSubscription == false);
            Assert.That(mediaItem.StreamingInfo.ElementAtOrDefault(6).AvailableOnSubscription == true);
        }
    }

    [Test]
    public void GetPlatformDetailsFromJson_WitNoResults_ShouldReturnArrayWithNoSubscriptions()
    {
        //Arrange
        string responseFromAPI = @"{
            ""result"": [
                {
                    ""type"": ""series"",
                    ""title"": ""The Runaround"",
                    ""overview"": ""simple overview"",
                    ""streamingInfo"": {
                    },
                    ""firstAirYear"": 1996,
                    ""lastAirYear"": 2001,
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
        IEnumerable<SearchResultDTO> SeriesSearchResults = SearchResultDTO.FromJSON(responseFromAPI);

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
        string responseFromAPI = @"{
            ""result"": [
                {
                    ""type"": ""series"",
                    ""title"": ""The Runaround"",
                    ""overview"": ""simple overview"",
                    ""streamingInfo"": {
                        
                                ""us"": {
                                  ""hulu"": [
                                    {
                                      ""type"": ""subscription"",
                                      ""quality"": """",
                                      ""addOn"": """",
                                      ""link"": ""https://www.hulu.com/series/psycho-pass-2b02607f-fc22-456f-8655-685d689d6701"",
                                      ""watchLink"": ""https://www.hulu.com/watch/627b2f80-adbd-424a-88b1-8ae81cdf149f"",
                                      ""audios"": [
                                        {
                                          ""language"": ""eng"",
                                          ""region"": """"
                                        },
                                        {
                                          ""language"": ""jpn"",
                                          ""region"": """"
                                        }
                                      ],
  
                                      ""leaving"": 1727765943,
                                      ""availableSince"": 1648527352
                                    }
                                  ]

                                }

                    },
                    ""firstAirYear"": 1996,
                    ""lastAirYear"": 2001,
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
        IEnumerable<SearchResultDTO> SeriesSearchResults = SearchResultDTO.FromJSON(responseFromAPI);


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
        string responseFromAPI = @"{
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

        JObject? jObject = JObject.Parse((string)responseFromAPI);
        List<JToken> myScope = jObject.SelectToken("seasons").ToList();

        //Act
        List<SeasonDetailsDTO> SeasonInfo = new List<SeasonDetailsDTO>();
        SeasonInfo = SeasonDetailsDTO.GetSeasonDetails_FromJSON(myScope);

        //Assert
        Assert.That(SeasonInfo.Count == 1);
        Assert.That(SeasonInfo.First().OfficialName == "Season 1");
        Assert.That(SeasonInfo.First().EpisodeCount == 2);
    }

    [Test]
    public void GetSeasonDetailsFromJSON_WithTwoSeasons_ShouldReturnListOfTwoSeasonDetails()
    {

        //Arrange
        string responseFromAPI = @"{
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

        JObject? jObject = JObject.Parse((string)responseFromAPI);
        List<JToken> myScope = jObject.SelectToken("seasons").ToList();

        //Act
        List<SeasonDetailsDTO> SeasonInfo = new List<SeasonDetailsDTO>();
        SeasonInfo = SeasonDetailsDTO.GetSeasonDetails_FromJSON(myScope);

        //Assert
        Assert.That(SeasonInfo.Count == 2);
        Assert.That(SeasonInfo.First().OfficialName == "Season 1");
        Assert.That(SeasonInfo.First().EpisodeCount == 2);

        Assert.That(SeasonInfo.ElementAt(1).OfficialName == "Season 2");
        Assert.That(SeasonInfo.ElementAt(1).EpisodeCount == 1);
    }

}