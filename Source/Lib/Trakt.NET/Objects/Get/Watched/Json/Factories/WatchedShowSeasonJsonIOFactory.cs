﻿namespace TraktNet.Objects.Get.Watched.Json.Factories
{
    using Get.Watched.Json.Reader;
    using Get.Watched.Json.Writer;
    using Objects.Json;

    internal class WatchedShowSeasonJsonIOFactory : IJsonIOFactory<ITraktWatchedShowSeason>
    {
        public IObjectJsonReader<ITraktWatchedShowSeason> CreateObjectReader() => new WatchedShowSeasonObjectJsonReader();

        public IObjectJsonWriter<ITraktWatchedShowSeason> CreateObjectWriter() => new WatchedShowSeasonObjectJsonWriter();
    }
}
