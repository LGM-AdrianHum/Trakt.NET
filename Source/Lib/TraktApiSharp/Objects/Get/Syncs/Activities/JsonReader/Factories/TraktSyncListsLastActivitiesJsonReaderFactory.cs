﻿namespace TraktApiSharp.Objects.Get.Syncs.Activities.JsonReader.Factories
{
    using Objects.JsonReader;
    using System;

    internal class TraktSyncListsLastActivitiesJsonReaderFactory : IJsonReaderFactory<ITraktSyncListsLastActivities>
    {
        public ITraktObjectJsonReader<ITraktSyncListsLastActivities> CreateObjectReader() => new TraktSyncListsLastActivitiesObjectJsonReader();

        public IArrayJsonReader<ITraktSyncListsLastActivities> CreateArrayReader()
        {
            throw new NotSupportedException($"A array json reader for {nameof(ITraktSyncListsLastActivities)} is not supported.");
        }
    }
}
