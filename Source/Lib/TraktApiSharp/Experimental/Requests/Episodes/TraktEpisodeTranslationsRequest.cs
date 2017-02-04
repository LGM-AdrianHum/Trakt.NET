﻿namespace TraktApiSharp.Experimental.Requests.Episodes
{
    using Objects.Get.Shows.Episodes;
    using System.Collections.Generic;

    internal sealed class TraktEpisodeTranslationsRequest : ATraktEpisodeRequest<TraktEpisodeTranslation>
    {
        internal string LanguageCode { get; set; }

        public override string UriTemplate => "shows/{id}/seasons/{season}/episodes/{episode}/translations{/language}";

        public override IDictionary<string, object> GetUriPathParameters()
        {
            var uriParams = base.GetUriPathParameters();

            if (!string.IsNullOrEmpty(LanguageCode))
                uriParams.Add("language", LanguageCode);

            return uriParams;
        }
    }
}