﻿namespace TraktNet.Objects.Post.Syncs.History.Responses.Json.Factories
{
    using Objects.Json;
    using Reader;
    using Writer;

    internal class SyncHistoryRemovePostResponseGroupJsonIOFactory : IJsonIOFactory<ITraktSyncHistoryRemovePostResponseGroup>
    {
        public IObjectJsonReader<ITraktSyncHistoryRemovePostResponseGroup> CreateObjectReader()
            => new SyncHistoryRemovePostResponseGroupObjectJsonReader();

        public IObjectJsonWriter<ITraktSyncHistoryRemovePostResponseGroup> CreateObjectWriter()
            => new SyncHistoryRemovePostResponseGroupObjectJsonWriter();
    }
}
