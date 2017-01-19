﻿namespace TraktApiSharp.Experimental.Requests.People
{
    using Objects.Get.People.Credits;

    internal sealed class TraktPersonShowCreditsRequest : ATraktPersonCreditsRequest<TraktPersonShowCredits>
    {
        internal TraktPersonShowCreditsRequest(TraktClient client) : base(client) { }

        public string UriTemplate => "people/{id}/shows{?extended}";
    }
}
