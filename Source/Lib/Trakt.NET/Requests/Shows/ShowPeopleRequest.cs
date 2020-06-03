﻿namespace TraktNet.Requests.Shows
{
    using Interfaces;
    using Objects.Get.Shows;
    using Parameters;
    using System.Collections.Generic;

    internal sealed class ShowPeopleRequest : AShowRequest<ITraktShowCastAndCrew>, ISupportsExtendedInfo
    {
        public TraktExtendedInfo ExtendedInfo { get; set; }

        public override string UriTemplate => "shows/{id}/people{?extended}";

        public override IDictionary<string, object> GetUriPathParameters()
        {
            var uriParams = base.GetUriPathParameters();

            if (ExtendedInfo != null && ExtendedInfo.HasAnySet)
                uriParams.Add("extended", ExtendedInfo.ToString());

            return uriParams;
        }
    }
}
