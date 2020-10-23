﻿namespace TraktNet.Objects.Basic.Json.Factories
{
    using Objects.Basic.Json.Reader;
    using Objects.Basic.Json.Writer;
    using Objects.Json;

    internal class GenreJsonIOFactory : IJsonIOFactory<ITraktGenre>
    {
        public IObjectJsonReader<ITraktGenre> CreateObjectReader() => new GenreObjectJsonReader();

        public IObjectJsonWriter<ITraktGenre> CreateObjectWriter() => new GenreObjectJsonWriter();
    }
}
