﻿namespace TraktNet.Objects.Post.Builder.Capabilities
{
    using Get.Shows;

    public interface ITraktPostBuilderAddShowWithWatchedAtWithSeasonsCollection<TPostBuilder, TPostObject, TSeasonCollection>
        where TPostBuilder : ITraktPostBuilder<TPostObject>, ITraktPostBuilderAddShowWithWatchedAtWithSeasonsCollection<TPostBuilder, TPostObject, TSeasonCollection>
    {
        ITraktPostBuilderShowAddedWatchedAtWithSeasonsCollection<TPostBuilder, TPostObject, TSeasonCollection> AddWatchedShowAndSeasonsCollection(ITraktShow show);
    }
}
