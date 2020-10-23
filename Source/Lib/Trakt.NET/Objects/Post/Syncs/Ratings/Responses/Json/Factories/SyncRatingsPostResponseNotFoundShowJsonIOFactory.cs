﻿namespace TraktNet.Objects.Post.Syncs.Ratings.Responses.Json.Factories
{
    using Objects.Json;
    using Reader;
    using Writer;

    internal class SyncRatingsPostResponseNotFoundShowJsonIOFactory : IJsonIOFactory<ITraktSyncRatingsPostResponseNotFoundShow>
    {
        public IObjectJsonReader<ITraktSyncRatingsPostResponseNotFoundShow> CreateObjectReader()
            => new SyncRatingsPostResponseNotFoundShowObjectJsonReader();

        public IObjectJsonWriter<ITraktSyncRatingsPostResponseNotFoundShow> CreateObjectWriter()
            => new SyncRatingsPostResponseNotFoundShowObjectJsonWriter();
    }
}
