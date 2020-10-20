﻿namespace TraktNet.Objects.Post.Builder.Capabilities
{
    using Get.Shows;

    public interface ITraktPostBuilderAddShowWithSeasons<TPostBuilder, TPostObject>
        where TPostBuilder : ITraktPostBuilder<TPostObject>, ITraktPostBuilderAddShowWithSeasons<TPostBuilder, TPostObject>
    {
        ITraktPostBuilderShowAddedSeasons<TPostBuilder, TPostObject> AddShowAndSeasons(ITraktShow show);
    }
}
