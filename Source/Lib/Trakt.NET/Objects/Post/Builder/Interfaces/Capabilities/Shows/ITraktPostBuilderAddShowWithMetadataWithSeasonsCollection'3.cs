﻿namespace TraktNet.Objects.Post.Builder.Capabilities
{
    using Get.Shows;

    public interface ITraktPostBuilderAddShowWithMetadataWithSeasonsCollection<TPostBuilder, TPostObject, TSeasonCollection>
        where TPostBuilder : ITraktPostBuilder<TPostObject>, ITraktPostBuilderAddShowWithMetadataWithSeasonsCollection<TPostBuilder, TPostObject, TSeasonCollection>
    {
        ITraktPostBuilderShowAddedMetadataWithSeasonsCollection<TPostBuilder, TPostObject, TSeasonCollection> AddShowAndMetadataAndSeasonsCollection(ITraktShow show);
    }
}
