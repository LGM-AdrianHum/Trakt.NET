﻿namespace TraktNet.Objects.Post.Builder.Capabilities
{
    using System;

    public interface ITraktPostBuilderShowAddedRating<TPostBuilderAddShow, out TPostObject>
        where TPostBuilderAddShow : ITraktPostBuilder<TPostObject>, ITraktPostBuilderAddShowWithRating<TPostBuilderAddShow, TPostObject>
    {
        TPostBuilderAddShow WithRating(int rating);

        TPostBuilderAddShow WithRating(int rating, DateTime ratedAt);
    }
}
