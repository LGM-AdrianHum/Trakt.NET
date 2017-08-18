﻿namespace TraktApiSharp.Objects.Get.Users.JsonReader.Factories
{
    using Objects.JsonReader;
    using System;

    internal class TraktUserIdsJsonReaderFactory : IJsonReaderFactory<ITraktUserIds>
    {
        public ITraktObjectJsonReader<ITraktUserIds> CreateObjectReader() => new TraktUserIdsObjectJsonReader();

        public IArrayJsonReader<ITraktUserIds> CreateArrayReader()
        {
            throw new NotSupportedException($"A array json reader for {nameof(ITraktUserIds)} is not supported.");
        }
    }
}
