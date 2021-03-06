﻿namespace TraktNet.Objects.Get.Tests.Watched.Json.Reader
{
    public partial class WatchedShowObjectJsonReader_Tests
    {
        private const string JSON_COMPLETE =
            @"{
                ""plays"": 1,
                ""last_watched_at"": ""2014-09-01T09:10:11.000Z"",
                ""last_updated_at"": ""2014-09-01T09:10:11.000Z"",
                ""reset_at"": ""2014-09-01T09:10:11.000Z"",
                ""show"": {
                  ""title"": ""Game of Thrones"",
                  ""year"": 2011,
                  ""ids"": {
                    ""trakt"": 1390,
                    ""slug"": ""game-of-thrones"",
                    ""tvdb"": 121361,
                    ""imdb"": ""tt0944947"",
                    ""tmdb"": 1399,
                    ""tvrage"": 24493
                  }
                },
                ""seasons"": [
                  {
                    ""number"": 1,
                    ""episodes"": [
                      {
                        ""number"": 1,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      },
                      {
                        ""number"": 2,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      }
                    ]
                  },
                  {
                    ""number"": 2,
                    ""episodes"": [
                      {
                        ""number"": 1,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      },
                      {
                        ""number"": 2,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      }
                    ]
                  }
                ]
              }";

        private const string JSON_INCOMPLETE_1 =
            @"{
                ""last_watched_at"": ""2014-09-01T09:10:11.000Z"",
                ""last_updated_at"": ""2014-09-01T09:10:11.000Z"",
                ""reset_at"": ""2014-09-01T09:10:11.000Z"",
                ""show"": {
                  ""title"": ""Game of Thrones"",
                  ""year"": 2011,
                  ""ids"": {
                    ""trakt"": 1390,
                    ""slug"": ""game-of-thrones"",
                    ""tvdb"": 121361,
                    ""imdb"": ""tt0944947"",
                    ""tmdb"": 1399,
                    ""tvrage"": 24493
                  }
                },
                ""seasons"": [
                  {
                    ""number"": 1,
                    ""episodes"": [
                      {
                        ""number"": 1,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      },
                      {
                        ""number"": 2,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      }
                    ]
                  },
                  {
                    ""number"": 2,
                    ""episodes"": [
                      {
                        ""number"": 1,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      },
                      {
                        ""number"": 2,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      }
                    ]
                  }
                ]
              }";

        private const string JSON_INCOMPLETE_2 =
            @"{
                ""plays"": 1,
                ""last_updated_at"": ""2014-09-01T09:10:11.000Z"",
                ""reset_at"": ""2014-09-01T09:10:11.000Z"",
                ""show"": {
                  ""title"": ""Game of Thrones"",
                  ""year"": 2011,
                  ""ids"": {
                    ""trakt"": 1390,
                    ""slug"": ""game-of-thrones"",
                    ""tvdb"": 121361,
                    ""imdb"": ""tt0944947"",
                    ""tmdb"": 1399,
                    ""tvrage"": 24493
                  }
                },
                ""seasons"": [
                  {
                    ""number"": 1,
                    ""episodes"": [
                      {
                        ""number"": 1,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      },
                      {
                        ""number"": 2,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      }
                    ]
                  },
                  {
                    ""number"": 2,
                    ""episodes"": [
                      {
                        ""number"": 1,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      },
                      {
                        ""number"": 2,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      }
                    ]
                  }
                ]
              }";

        private const string JSON_INCOMPLETE_3 =
            @"{
                ""plays"": 1,
                ""last_watched_at"": ""2014-09-01T09:10:11.000Z"",
                ""reset_at"": ""2014-09-01T09:10:11.000Z"",
                ""show"": {
                  ""title"": ""Game of Thrones"",
                  ""year"": 2011,
                  ""ids"": {
                    ""trakt"": 1390,
                    ""slug"": ""game-of-thrones"",
                    ""tvdb"": 121361,
                    ""imdb"": ""tt0944947"",
                    ""tmdb"": 1399,
                    ""tvrage"": 24493
                  }
                },
                ""seasons"": [
                  {
                    ""number"": 1,
                    ""episodes"": [
                      {
                        ""number"": 1,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      },
                      {
                        ""number"": 2,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      }
                    ]
                  },
                  {
                    ""number"": 2,
                    ""episodes"": [
                      {
                        ""number"": 1,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      },
                      {
                        ""number"": 2,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      }
                    ]
                  }
                ]
              }";

        private const string JSON_INCOMPLETE_4 =
            @"{
                ""plays"": 1,
                ""last_watched_at"": ""2014-09-01T09:10:11.000Z"",
                ""last_updated_at"": ""2014-09-01T09:10:11.000Z"",
                ""show"": {
                  ""title"": ""Game of Thrones"",
                  ""year"": 2011,
                  ""ids"": {
                    ""trakt"": 1390,
                    ""slug"": ""game-of-thrones"",
                    ""tvdb"": 121361,
                    ""imdb"": ""tt0944947"",
                    ""tmdb"": 1399,
                    ""tvrage"": 24493
                  }
                },
                ""seasons"": [
                  {
                    ""number"": 1,
                    ""episodes"": [
                      {
                        ""number"": 1,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      },
                      {
                        ""number"": 2,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      }
                    ]
                  },
                  {
                    ""number"": 2,
                    ""episodes"": [
                      {
                        ""number"": 1,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      },
                      {
                        ""number"": 2,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      }
                    ]
                  }
                ]
              }";

        private const string JSON_INCOMPLETE_5 =
            @"{
                ""plays"": 1,
                ""last_watched_at"": ""2014-09-01T09:10:11.000Z"",
                ""last_updated_at"": ""2014-09-01T09:10:11.000Z"",
                ""reset_at"": ""2014-09-01T09:10:11.000Z"",
                ""seasons"": [
                  {
                    ""number"": 1,
                    ""episodes"": [
                      {
                        ""number"": 1,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      },
                      {
                        ""number"": 2,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      }
                    ]
                  },
                  {
                    ""number"": 2,
                    ""episodes"": [
                      {
                        ""number"": 1,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      },
                      {
                        ""number"": 2,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      }
                    ]
                  }
                ]
              }";

        private const string JSON_INCOMPLETE_6 =
            @"{
                ""plays"": 1,
                ""last_watched_at"": ""2014-09-01T09:10:11.000Z"",
                ""last_updated_at"": ""2014-09-01T09:10:11.000Z"",
                ""reset_at"": ""2014-09-01T09:10:11.000Z"",
                ""show"": {
                  ""title"": ""Game of Thrones"",
                  ""year"": 2011,
                  ""ids"": {
                    ""trakt"": 1390,
                    ""slug"": ""game-of-thrones"",
                    ""tvdb"": 121361,
                    ""imdb"": ""tt0944947"",
                    ""tmdb"": 1399,
                    ""tvrage"": 24493
                  }
                }
              }";

        private const string JSON_INCOMPLETE_7 =
            @"{
                ""plays"": 1
              }";

        private const string JSON_INCOMPLETE_8 =
            @"{
                ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
              }";

        private const string JSON_INCOMPLETE_9 =
            @"{
                ""last_updated_at"": ""2014-09-01T09:10:11.000Z""
              }";

        private const string JSON_INCOMPLETE_10 =
            @"{
                ""reset_at"": ""2014-09-01T09:10:11.000Z""
              }";

        private const string JSON_INCOMPLETE_11 =
            @"{
                ""show"": {
                  ""title"": ""Game of Thrones"",
                  ""year"": 2011,
                  ""ids"": {
                    ""trakt"": 1390,
                    ""slug"": ""game-of-thrones"",
                    ""tvdb"": 121361,
                    ""imdb"": ""tt0944947"",
                    ""tmdb"": 1399,
                    ""tvrage"": 24493
                  }
                }
              }";

        private const string JSON_INCOMPLETE_12 =
            @"{
                ""seasons"": [
                  {
                    ""number"": 1,
                    ""episodes"": [
                      {
                        ""number"": 1,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      },
                      {
                        ""number"": 2,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      }
                    ]
                  },
                  {
                    ""number"": 2,
                    ""episodes"": [
                      {
                        ""number"": 1,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      },
                      {
                        ""number"": 2,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      }
                    ]
                  }
                ]
              }";

        private const string JSON_NOT_VALID_1 =
            @"{
                ""pl"": 1,
                ""last_watched_at"": ""2014-09-01T09:10:11.000Z"",
                ""last_updated_at"": ""2014-09-01T09:10:11.000Z"",
                ""reset_at"": ""2014-09-01T09:10:11.000Z"",
                ""show"": {
                  ""title"": ""Game of Thrones"",
                  ""year"": 2011,
                  ""ids"": {
                    ""trakt"": 1390,
                    ""slug"": ""game-of-thrones"",
                    ""tvdb"": 121361,
                    ""imdb"": ""tt0944947"",
                    ""tmdb"": 1399,
                    ""tvrage"": 24493
                  }
                },
                ""seasons"": [
                  {
                    ""number"": 1,
                    ""episodes"": [
                      {
                        ""number"": 1,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      },
                      {
                        ""number"": 2,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      }
                    ]
                  },
                  {
                    ""number"": 2,
                    ""episodes"": [
                      {
                        ""number"": 1,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      },
                      {
                        ""number"": 2,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      }
                    ]
                  }
                ]
              }";

        private const string JSON_NOT_VALID_2 =
            @"{
                ""plays"": 1,
                ""lwa"": ""2014-09-01T09:10:11.000Z"",
                ""last_updated_at"": ""2014-09-01T09:10:11.000Z"",
                ""reset_at"": ""2014-09-01T09:10:11.000Z"",
                ""show"": {
                  ""title"": ""Game of Thrones"",
                  ""year"": 2011,
                  ""ids"": {
                    ""trakt"": 1390,
                    ""slug"": ""game-of-thrones"",
                    ""tvdb"": 121361,
                    ""imdb"": ""tt0944947"",
                    ""tmdb"": 1399,
                    ""tvrage"": 24493
                  }
                },
                ""seasons"": [
                  {
                    ""number"": 1,
                    ""episodes"": [
                      {
                        ""number"": 1,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      },
                      {
                        ""number"": 2,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      }
                    ]
                  },
                  {
                    ""number"": 2,
                    ""episodes"": [
                      {
                        ""number"": 1,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      },
                      {
                        ""number"": 2,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      }
                    ]
                  }
                ]
              }";

        private const string JSON_NOT_VALID_3 =
            @"{
                ""plays"": 1,
                ""last_watched_at"": ""2014-09-01T09:10:11.000Z"",
                ""lua"": ""2014-09-01T09:10:11.000Z"",
                ""reset_at"": ""2014-09-01T09:10:11.000Z"",
                ""show"": {
                  ""title"": ""Game of Thrones"",
                  ""year"": 2011,
                  ""ids"": {
                    ""trakt"": 1390,
                    ""slug"": ""game-of-thrones"",
                    ""tvdb"": 121361,
                    ""imdb"": ""tt0944947"",
                    ""tmdb"": 1399,
                    ""tvrage"": 24493
                  }
                },
                ""seasons"": [
                  {
                    ""number"": 1,
                    ""episodes"": [
                      {
                        ""number"": 1,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      },
                      {
                        ""number"": 2,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      }
                    ]
                  },
                  {
                    ""number"": 2,
                    ""episodes"": [
                      {
                        ""number"": 1,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      },
                      {
                        ""number"": 2,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      }
                    ]
                  }
                ]
              }";

        private const string JSON_NOT_VALID_4 =
            @"{
                ""plays"": 1,
                ""last_watched_at"": ""2014-09-01T09:10:11.000Z"",
                ""last_updated_at"": ""2014-09-01T09:10:11.000Z"",
                ""ra"": ""2014-09-01T09:10:11.000Z"",
                ""show"": {
                  ""title"": ""Game of Thrones"",
                  ""year"": 2011,
                  ""ids"": {
                    ""trakt"": 1390,
                    ""slug"": ""game-of-thrones"",
                    ""tvdb"": 121361,
                    ""imdb"": ""tt0944947"",
                    ""tmdb"": 1399,
                    ""tvrage"": 24493
                  }
                },
                ""seasons"": [
                  {
                    ""number"": 1,
                    ""episodes"": [
                      {
                        ""number"": 1,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      },
                      {
                        ""number"": 2,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      }
                    ]
                  },
                  {
                    ""number"": 2,
                    ""episodes"": [
                      {
                        ""number"": 1,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      },
                      {
                        ""number"": 2,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      }
                    ]
                  }
                ]
              }";

        private const string JSON_NOT_VALID_5 =
            @"{
                ""plays"": 1,
                ""last_watched_at"": ""2014-09-01T09:10:11.000Z"",
                ""last_updated_at"": ""2014-09-01T09:10:11.000Z"",
                ""reset_at"": ""2014-09-01T09:10:11.000Z"",
                ""sh"": {
                  ""title"": ""Game of Thrones"",
                  ""year"": 2011,
                  ""ids"": {
                    ""trakt"": 1390,
                    ""slug"": ""game-of-thrones"",
                    ""tvdb"": 121361,
                    ""imdb"": ""tt0944947"",
                    ""tmdb"": 1399,
                    ""tvrage"": 24493
                  }
                },
                ""seasons"": [
                  {
                    ""number"": 1,
                    ""episodes"": [
                      {
                        ""number"": 1,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      },
                      {
                        ""number"": 2,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      }
                    ]
                  },
                  {
                    ""number"": 2,
                    ""episodes"": [
                      {
                        ""number"": 1,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      },
                      {
                        ""number"": 2,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      }
                    ]
                  }
                ]
              }";

        private const string JSON_NOT_VALID_6 =
            @"{
                ""plays"": 1,
                ""last_watched_at"": ""2014-09-01T09:10:11.000Z"",
                ""last_updated_at"": ""2014-09-01T09:10:11.000Z"",
                ""reset_at"": ""2014-09-01T09:10:11.000Z"",
                ""show"": {
                  ""title"": ""Game of Thrones"",
                  ""year"": 2011,
                  ""ids"": {
                    ""trakt"": 1390,
                    ""slug"": ""game-of-thrones"",
                    ""tvdb"": 121361,
                    ""imdb"": ""tt0944947"",
                    ""tmdb"": 1399,
                    ""tvrage"": 24493
                  }
                },
                ""sea"": [
                  {
                    ""number"": 1,
                    ""episodes"": [
                      {
                        ""number"": 1,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      },
                      {
                        ""number"": 2,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      }
                    ]
                  },
                  {
                    ""number"": 2,
                    ""episodes"": [
                      {
                        ""number"": 1,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      },
                      {
                        ""number"": 2,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      }
                    ]
                  }
                ]
              }";

        private const string JSON_NOT_VALID_7 =
            @"{
                ""pl"": 1,
                ""lwa"": ""2014-09-01T09:10:11.000Z"",
                ""lua"": ""2014-09-01T09:10:11.000Z"",
                ""ra"": ""2014-09-01T09:10:11.000Z"",
                ""sh"": {
                  ""title"": ""Game of Thrones"",
                  ""year"": 2011,
                  ""ids"": {
                    ""trakt"": 1390,
                    ""slug"": ""game-of-thrones"",
                    ""tvdb"": 121361,
                    ""imdb"": ""tt0944947"",
                    ""tmdb"": 1399,
                    ""tvrage"": 24493
                  }
                },
                ""sea"": [
                  {
                    ""number"": 1,
                    ""episodes"": [
                      {
                        ""number"": 1,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      },
                      {
                        ""number"": 2,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      }
                    ]
                  },
                  {
                    ""number"": 2,
                    ""episodes"": [
                      {
                        ""number"": 1,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      },
                      {
                        ""number"": 2,
                        ""plays"": 1,
                        ""last_watched_at"": ""2014-09-01T09:10:11.000Z""
                      }
                    ]
                  }
                ]
              }";
    }
}
