﻿namespace TraktApiSharp.Experimental.Requests.Recommendations.OAuth
{
    using Objects.Get.Shows;

    internal sealed class TraktUserShowRecommendationsRequest : ATraktUserRecommendationsRequest<TraktShow>
    {
        internal TraktUserShowRecommendationsRequest(TraktClient client) : base(client) { }

        public string UriTemplate => "recommendations/shows{?extended,limit}";
    }
}
