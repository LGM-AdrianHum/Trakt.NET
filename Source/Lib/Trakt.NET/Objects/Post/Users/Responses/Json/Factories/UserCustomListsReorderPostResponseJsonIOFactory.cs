﻿namespace TraktNet.Objects.Post.Users.Responses.Json.Factories
{
    using Objects.Json;
    using Reader;
    using Writer;

    internal class UserCustomListsReorderPostResponseJsonIOFactory : IJsonIOFactory<ITraktUserCustomListsReorderPostResponse>
    {
        public IObjectJsonReader<ITraktUserCustomListsReorderPostResponse> CreateObjectReader() => new UserCustomListsReorderPostResponseObjectJsonReader();

        public IObjectJsonWriter<ITraktUserCustomListsReorderPostResponse> CreateObjectWriter() => new UserCustomListsReorderPostResponseObjectJsonWriter();
    }
}
