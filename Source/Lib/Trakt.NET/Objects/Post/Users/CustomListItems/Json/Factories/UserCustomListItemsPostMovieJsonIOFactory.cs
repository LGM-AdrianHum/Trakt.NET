﻿namespace TraktNet.Objects.Post.Users.CustomListItems.Json.Factories
{
    using Objects.Json;
    using Reader;
    using Writer;

    internal class UserCustomListItemsPostMovieJsonIOFactory : IJsonIOFactory<ITraktUserCustomListItemsPostMovie>
    {
        public IObjectJsonReader<ITraktUserCustomListItemsPostMovie> CreateObjectReader() => new UserCustomListItemsPostMovieObjectJsonReader();

        public IObjectJsonWriter<ITraktUserCustomListItemsPostMovie> CreateObjectWriter() => new UserCustomListItemsPostMovieObjectJsonWriter();
    }
}
