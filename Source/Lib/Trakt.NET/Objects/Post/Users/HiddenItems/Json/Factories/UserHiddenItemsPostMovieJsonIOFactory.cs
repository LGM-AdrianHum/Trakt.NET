﻿namespace TraktNet.Objects.Post.Users.HiddenItems.Json.Factories
{
    using Objects.Json;
    using Reader;
    using Writer;

    internal class UserHiddenItemsPostMovieJsonIOFactory : IJsonIOFactory<ITraktUserHiddenItemsPostMovie>
    {
        public IObjectJsonReader<ITraktUserHiddenItemsPostMovie> CreateObjectReader() => new UserHiddenItemsPostMovieObjectJsonReader();

        public IObjectJsonWriter<ITraktUserHiddenItemsPostMovie> CreateObjectWriter() => new UserHiddenItemsPostMovieObjectJsonWriter();
    }
}
