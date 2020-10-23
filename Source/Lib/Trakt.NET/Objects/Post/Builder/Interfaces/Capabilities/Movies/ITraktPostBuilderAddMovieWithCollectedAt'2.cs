﻿namespace TraktNet.Objects.Post.Builder.Capabilities
{
    using Get.Movies;

    public interface ITraktPostBuilderAddMovieWithCollectedAt<TPostBuilder, TPostObject>
        where TPostBuilder : ITraktPostBuilder<TPostObject>, ITraktPostBuilderAddMovieWithCollectedAt<TPostBuilder, TPostObject>
    {
        ITraktPostBuilderMovieAddedCollectedAt<TPostBuilder, TPostObject> AddCollectedMovie(ITraktMovie movie);
    }
}
