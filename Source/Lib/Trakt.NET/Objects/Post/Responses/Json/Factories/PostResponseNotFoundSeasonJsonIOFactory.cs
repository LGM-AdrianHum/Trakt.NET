﻿namespace TraktNet.Objects.Post.Responses.Json.Factories
{
    using Objects.Json;
    using Reader;
    using Writer;

    internal class PostResponseNotFoundSeasonJsonIOFactory : IJsonIOFactory<ITraktPostResponseNotFoundSeason>
    {
        public IObjectJsonReader<ITraktPostResponseNotFoundSeason> CreateObjectReader() => new PostResponseNotFoundSeasonObjectJsonReader();

        public IObjectJsonWriter<ITraktPostResponseNotFoundSeason> CreateObjectWriter() => new PostResponseNotFoundSeasonObjectJsonWriter();
    }
}
