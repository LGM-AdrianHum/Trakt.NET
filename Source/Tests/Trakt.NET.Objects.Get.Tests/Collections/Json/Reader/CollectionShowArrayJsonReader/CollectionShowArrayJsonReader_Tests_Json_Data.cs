﻿namespace TraktNet.Objects.Get.Tests.Collections.Json.Reader
{
    public partial class CollectionShowArrayJsonReader_Tests
    {
        private const string JSON_EMPTY_ARRAY = @"[]";

        private const string JSON_COMPLETE =
            @"[
                {
                  ""last_collected_at"": ""2014-07-14T01:00:00.000Z"",
                  ""last_updated_at"": ""2014-07-14T01:00:00.000Z"",
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
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        },
                        {
                          ""number"": 2,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        }
                      ]
                    },
                    {
                      ""number"": 2,
                      ""episodes"": [
                        {
                          ""number"": 1,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        },
                        {
                          ""number"": 2,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        }
                      ]
                    }
                  ]
                },
                {
                  ""last_collected_at"": ""2014-07-14T01:00:00.000Z"",
                  ""last_updated_at"": ""2014-07-14T01:00:00.000Z"",
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
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        },
                        {
                          ""number"": 2,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        }
                      ]
                    },
                    {
                      ""number"": 2,
                      ""episodes"": [
                        {
                          ""number"": 1,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        },
                        {
                          ""number"": 2,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        }
                      ]
                    }
                  ]
                }
              ]";

        private const string JSON_INCOMPLETE_1 =
            @"[
                {
                  ""last_collected_at"": ""2014-07-14T01:00:00.000Z"",
                  ""last_updated_at"": ""2014-07-14T01:00:00.000Z"",
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
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        },
                        {
                          ""number"": 2,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        }
                      ]
                    },
                    {
                      ""number"": 2,
                      ""episodes"": [
                        {
                          ""number"": 1,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        },
                        {
                          ""number"": 2,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        }
                      ]
                    }
                  ]
                },
                {
                  ""last_updated_at"": ""2014-07-14T01:00:00.000Z"",
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
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        },
                        {
                          ""number"": 2,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        }
                      ]
                    },
                    {
                      ""number"": 2,
                      ""episodes"": [
                        {
                          ""number"": 1,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        },
                        {
                          ""number"": 2,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        }
                      ]
                    }
                  ]
                }
              ]";

        private const string JSON_INCOMPLETE_2 =
            @"[
                {
                  ""last_updated_at"": ""2014-07-14T01:00:00.000Z"",
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
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        },
                        {
                          ""number"": 2,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        }
                      ]
                    },
                    {
                      ""number"": 2,
                      ""episodes"": [
                        {
                          ""number"": 1,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        },
                        {
                          ""number"": 2,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        }
                      ]
                    }
                  ]
                },
                {
                  ""last_collected_at"": ""2014-07-14T01:00:00.000Z"",
                  ""last_updated_at"": ""2014-07-14T01:00:00.000Z"",
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
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        },
                        {
                          ""number"": 2,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        }
                      ]
                    },
                    {
                      ""number"": 2,
                      ""episodes"": [
                        {
                          ""number"": 1,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        },
                        {
                          ""number"": 2,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        }
                      ]
                    }
                  ]
                }
              ]";

        private const string JSON_NOT_VALID_1 =
            @"[
                {
                  ""last_collected_at"": ""2014-07-14T01:00:00.000Z"",
                  ""last_updated_at"": ""2014-07-14T01:00:00.000Z"",
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
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        },
                        {
                          ""number"": 2,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        }
                      ]
                    },
                    {
                      ""number"": 2,
                      ""episodes"": [
                        {
                          ""number"": 1,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        },
                        {
                          ""number"": 2,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        }
                      ]
                    }
                  ]
                },
                {
                  ""lca"": ""2014-07-14T01:00:00.000Z"",
                  ""last_updated_at"": ""2014-07-14T01:00:00.000Z"",
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
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        },
                        {
                          ""number"": 2,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        }
                      ]
                    },
                    {
                      ""number"": 2,
                      ""episodes"": [
                        {
                          ""number"": 1,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        },
                        {
                          ""number"": 2,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        }
                      ]
                    }
                  ]
                }
              ]";

        private const string JSON_NOT_VALID_2 =
            @"[
                {
                  ""lca"": ""2014-07-14T01:00:00.000Z"",
                  ""last_updated_at"": ""2014-07-14T01:00:00.000Z"",
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
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        },
                        {
                          ""number"": 2,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        }
                      ]
                    },
                    {
                      ""number"": 2,
                      ""episodes"": [
                        {
                          ""number"": 1,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        },
                        {
                          ""number"": 2,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        }
                      ]
                    }
                  ]
                },
                {
                  ""last_collected_at"": ""2014-07-14T01:00:00.000Z"",
                  ""last_updated_at"": ""2014-07-14T01:00:00.000Z"",
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
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        },
                        {
                          ""number"": 2,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        }
                      ]
                    },
                    {
                      ""number"": 2,
                      ""episodes"": [
                        {
                          ""number"": 1,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        },
                        {
                          ""number"": 2,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        }
                      ]
                    }
                  ]
                }
              ]";

        private const string JSON_NOT_VALID_3 =
            @"[
                {
                  ""lca"": ""2014-07-14T01:00:00.000Z"",
                  ""last_updated_at"": ""2014-07-14T01:00:00.000Z"",
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
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        },
                        {
                          ""number"": 2,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        }
                      ]
                    },
                    {
                      ""number"": 2,
                      ""episodes"": [
                        {
                          ""number"": 1,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        },
                        {
                          ""number"": 2,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        }
                      ]
                    }
                  ]
                },
                {
                  ""lca"": ""2014-07-14T01:00:00.000Z"",
                  ""last_updated_at"": ""2014-07-14T01:00:00.000Z"",
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
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        },
                        {
                          ""number"": 2,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        }
                      ]
                    },
                    {
                      ""number"": 2,
                      ""episodes"": [
                        {
                          ""number"": 1,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        },
                        {
                          ""number"": 2,
                          ""collected_at"": ""2014-09-01T09:10:11.000Z"",
                          ""metadata"": {
                            ""media_type"": ""digital"",
                            ""resolution"": ""hd_720p"",
                            ""audio"": ""aac"",
                            ""audio_channels"": ""5.1"",
                            ""3d"": false
                          }
                        }
                      ]
                    }
                  ]
                }
              ]";
    }
}
