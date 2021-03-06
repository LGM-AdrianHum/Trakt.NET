﻿namespace TraktNet.Objects.Post.Builder.Capabilities
{
    using Get.Shows;
    using System.Collections.Generic;

    internal interface IPostBuilderShowAddedRatingDetail
    {
        List<PostBuilderRatedObject<ITraktShow>> ShowsAndRating { get; }

        void SetCurrentShow(ITraktShow show);
    }
}
