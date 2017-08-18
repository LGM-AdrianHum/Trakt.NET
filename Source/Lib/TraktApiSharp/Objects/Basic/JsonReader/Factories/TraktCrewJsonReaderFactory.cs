﻿namespace TraktApiSharp.Objects.Basic.JsonReader.Factories
{
    using Objects.JsonReader;
    using System;

    internal class TraktCrewJsonReaderFactory : IJsonReaderFactory<ITraktCrew>
    {
        public ITraktObjectJsonReader<ITraktCrew> CreateObjectReader() => new TraktCrewObjectJsonReader();

        public IArrayJsonReader<ITraktCrew> CreateArrayReader()
        {
            throw new NotSupportedException($"A array json reader for {nameof(ITraktCrew)} is not supported.");
        }
    }
}
