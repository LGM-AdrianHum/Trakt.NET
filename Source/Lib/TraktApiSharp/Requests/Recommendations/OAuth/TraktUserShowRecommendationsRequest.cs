﻿namespace TraktApiSharp.Requests.Recommendations.OAuth
{
    using Objects.Get.Shows;

    internal sealed class TraktUserShowRecommendationsRequest : AUserRecommendationsRequest<ITraktShow>
    {
        public override string UriTemplate => "recommendations/shows{?extended,limit}";

        public override void Validate() { }
    }
}
