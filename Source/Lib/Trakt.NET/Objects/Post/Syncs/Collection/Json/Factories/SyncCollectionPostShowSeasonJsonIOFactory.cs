﻿namespace TraktNet.Objects.Post.Syncs.Collection.Json.Factories
{
    using Objects.Json;
    using Reader;
    using Writer;

    internal class SyncCollectionPostShowSeasonJsonIOFactory : IJsonIOFactory<ITraktSyncCollectionPostShowSeason>
    {
        public IObjectJsonReader<ITraktSyncCollectionPostShowSeason> CreateObjectReader() => new SyncCollectionPostShowSeasonObjectJsonReader();

        public IObjectJsonWriter<ITraktSyncCollectionPostShowSeason> CreateObjectWriter() => new SyncCollectionPostShowSeasonObjectJsonWriter();
    }
}
