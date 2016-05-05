﻿namespace TraktApiSharp.Objects.Get.Users
{
    using Enums;
    using Movies;
    using Newtonsoft.Json;
    using Shows;
    using Shows.Episodes;
    using System;

    public class TraktUserWatchingItem
    {
        [JsonProperty(PropertyName = "expires_at")]
        public DateTime ExpiresAt { get; set; }

        [JsonProperty(PropertyName = "started_at")]
        public DateTime StartedAt { get; set; }

        [JsonProperty(PropertyName = "action")]
        [JsonConverter(typeof(TraktSyncHistoryActionTypeConverter))]
        public TraktSyncHistoryActionType Action { get; set; }

        [JsonProperty(PropertyName = "type")]
        [JsonConverter(typeof(TraktSyncTypeConverter))]
        public TraktSyncType Type { get; set; }

        [JsonProperty(PropertyName = "movie")]
        public TraktMovie Movie { get; set; }

        [JsonProperty(PropertyName = "show")]
        public TraktShow Show { get; set; }

        [JsonProperty(PropertyName = "episode")]
        public TraktEpisode Episode { get; set; }
    }
}