﻿namespace TraktNet.Objects.Post.Syncs.Collection.Responses.Json.Factories
{
    using Objects.Json;
    using Reader;
    using Writer;

    internal class SyncCollectionPostResponseJsonIOFactory : IJsonIOFactory<ITraktSyncCollectionPostResponse>
    {
        public IObjectJsonReader<ITraktSyncCollectionPostResponse> CreateObjectReader() => new SyncCollectionPostResponseObjectJsonReader();

        public IObjectJsonWriter<ITraktSyncCollectionPostResponse> CreateObjectWriter() => new SyncCollectionPostResponseObjectJsonWriter();
    }
}
