﻿namespace TraktApiSharp.Requests.Shows
{
    using Objects.Basic;
    using TraktApiSharp.Objects.Basic.Implementations;

    internal sealed class TraktShowRatingsRequest : ATraktShowRequest<TraktRating>
    {
        public override string UriTemplate => "shows/{id}/ratings";
    }
}
