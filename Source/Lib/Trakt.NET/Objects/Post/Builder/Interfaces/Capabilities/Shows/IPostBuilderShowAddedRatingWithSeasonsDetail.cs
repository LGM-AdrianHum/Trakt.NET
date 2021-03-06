﻿namespace TraktNet.Objects.Post.Builder.Capabilities
{
    using Get.Shows;
    using System.Collections.Generic;

    internal interface IPostBuilderShowAddedRatingWithSeasonsDetail
    {
        List<PostBuilderRatedObjectWithSeasons<ITraktShow, IEnumerable<int>>> ShowsAndRatingWithSeasons { get; }

        void SetCurrentShow(ITraktShow show);
    }
}
