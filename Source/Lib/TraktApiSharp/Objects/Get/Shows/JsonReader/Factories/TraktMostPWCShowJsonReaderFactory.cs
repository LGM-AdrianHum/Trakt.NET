﻿namespace TraktApiSharp.Objects.Get.Shows.JsonReader.Factories
{
    using Objects.JsonReader;

    internal class TraktMostPWCShowJsonReaderFactory : IJsonReaderFactory<ITraktMostPWCShow>
    {
        public ITraktObjectJsonReader<ITraktMostPWCShow> CreateObjectReader() => new TraktMostPWCShowObjectJsonReader();

        public IArrayJsonReader<ITraktMostPWCShow> CreateArrayReader() => new TraktMostPWCShowArrayJsonReader();
    }
}
