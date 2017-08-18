﻿namespace TraktApiSharp.Objects.Get.Episodes.JsonReader.Factories
{
    using Objects.JsonReader;

    internal class TraktEpisodeWatchedProgressJsonReaderFactory : IJsonReaderFactory<ITraktEpisodeWatchedProgress>
    {
        public ITraktObjectJsonReader<ITraktEpisodeWatchedProgress> CreateObjectReader() => new TraktEpisodeWatchedProgressObjectJsonReader();

        public IArrayJsonReader<ITraktEpisodeWatchedProgress> CreateArrayReader() => new TraktEpisodeWatchedProgressArrayJsonReader();
    }
}
