﻿namespace TraktApiSharp.Experimental.Requests.Shows
{
    using Base.Get;
    using Interfaces;
    using Objects.Get.Shows;
    using TraktApiSharp.Requests;

    internal sealed class TraktShowSingleTranslationRequest : ATraktSingleItemGetByIdRequest<TraktShowTranslation>, ITraktObjectRequest
    {
        public TraktShowSingleTranslationRequest(TraktClient client) : base(client) { }

        internal string LanguageCode { get; set; }

        public override TraktAuthorizationRequirement AuthorizationRequirement => TraktAuthorizationRequirement.NotRequired;

        public TraktRequestObjectType RequestObjectType => TraktRequestObjectType.Shows;

        public override string UriTemplate => "shows/{id}/translations/{language}";
    }
}
