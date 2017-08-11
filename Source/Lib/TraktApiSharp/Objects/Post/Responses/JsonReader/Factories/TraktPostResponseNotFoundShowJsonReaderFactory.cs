﻿namespace TraktApiSharp.Objects.Post.Responses.JsonReader.Factories
{
    using Objects.JsonReader;

    internal class TraktPostResponseNotFoundShowJsonReaderFactory : ITraktJsonReaderFactory<ITraktPostResponseNotFoundShow>
    {
        public ITraktObjectJsonReader<ITraktPostResponseNotFoundShow> CreateObjectReader() => new TraktPostResponseNotFoundShowObjectJsonReader();

        public ITraktArrayJsonReader<ITraktPostResponseNotFoundShow> CreateArrayReader() => new TraktPostResponseNotFoundShowArrayJsonReader();
    }
}