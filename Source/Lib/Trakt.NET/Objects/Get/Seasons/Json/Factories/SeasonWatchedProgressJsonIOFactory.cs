﻿namespace TraktNet.Objects.Get.Seasons.Json.Factories
{
    using Get.Seasons.Json.Reader;
    using Get.Seasons.Json.Writer;
    using Objects.Json;

    internal class SeasonWatchedProgressJsonIOFactory : IJsonIOFactory<ITraktSeasonWatchedProgress>
    {
        public IObjectJsonReader<ITraktSeasonWatchedProgress> CreateObjectReader() => new SeasonWatchedProgressObjectJsonReader();

        public IObjectJsonWriter<ITraktSeasonWatchedProgress> CreateObjectWriter() => new SeasonWatchedProgressObjectJsonWriter();
    }
}
