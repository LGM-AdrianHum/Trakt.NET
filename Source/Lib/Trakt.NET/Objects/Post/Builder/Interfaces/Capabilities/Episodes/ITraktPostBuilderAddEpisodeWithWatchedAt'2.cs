﻿namespace TraktNet.Objects.Post.Builder.Capabilities
{
    using Get.Episodes;

    public interface ITraktPostBuilderAddEpisodeWithWatchedAt<TPostBuilder, TPostObject>
        where TPostBuilder : ITraktPostBuilder<TPostObject>, ITraktPostBuilderAddEpisodeWithWatchedAt<TPostBuilder, TPostObject>
    {
        ITraktPostBuilderEpisodeAddedWatchedAt<TPostBuilder, TPostObject> AddWatchedEpisode(ITraktEpisode episode);
    }
}
