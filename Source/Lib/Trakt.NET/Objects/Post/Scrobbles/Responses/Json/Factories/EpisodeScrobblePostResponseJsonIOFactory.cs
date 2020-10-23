﻿namespace TraktNet.Objects.Post.Scrobbles.Responses.Json.Factories
{
    using Objects.Json;
    using Reader;
    using Writer;

    internal class EpisodeScrobblePostResponseJsonIOFactory : IJsonIOFactory<ITraktEpisodeScrobblePostResponse>
    {
        public IObjectJsonReader<ITraktEpisodeScrobblePostResponse> CreateObjectReader() => new EpisodeScrobblePostResponseObjectJsonReader();

        public IObjectJsonWriter<ITraktEpisodeScrobblePostResponse> CreateObjectWriter() => new EpisodeScrobblePostResponseObjectJsonWriter();
    }
}
