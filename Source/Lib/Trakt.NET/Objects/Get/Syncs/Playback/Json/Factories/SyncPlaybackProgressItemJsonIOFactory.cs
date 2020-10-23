﻿namespace TraktNet.Objects.Get.Syncs.Playback.Json.Factories
{
    using Get.Syncs.Playback.Json.Reader;
    using Get.Syncs.Playback.Json.Writer;
    using Objects.Json;

    internal class SyncPlaybackProgressItemJsonIOFactory : IJsonIOFactory<ITraktSyncPlaybackProgressItem>
    {
        public IObjectJsonReader<ITraktSyncPlaybackProgressItem> CreateObjectReader() => new SyncPlaybackProgressItemObjectJsonReader();

        public IObjectJsonWriter<ITraktSyncPlaybackProgressItem> CreateObjectWriter() => new SyncPlaybackProgressItemObjectJsonWriter();
    }
}
