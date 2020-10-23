﻿namespace TraktNet.Objects.Post.Syncs.History.Responses.Json.Factories
{
    using Objects.Json;
    using Reader;
    using Writer;

    internal class SyncHistoryRemovePostResponseNotFoundGroupJsonIOFactory : IJsonIOFactory<ITraktSyncHistoryRemovePostResponseNotFoundGroup>
    {
        public IObjectJsonReader<ITraktSyncHistoryRemovePostResponseNotFoundGroup> CreateObjectReader()
            => new SyncHistoryRemovePostResponseNotFoundGroupObjectJsonReader();

        public IObjectJsonWriter<ITraktSyncHistoryRemovePostResponseNotFoundGroup> CreateObjectWriter()
            => new SyncHistoryRemovePostResponseNotFoundGroupObjectJsonWriter();
    }
}
