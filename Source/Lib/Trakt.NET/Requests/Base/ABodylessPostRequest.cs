﻿namespace TraktNet.Requests.Base
{
    using Interfaces.Base;
    using System.Net.Http;

    internal abstract class ABodylessPostRequest : ARequest, IBodylessPostRequest
    {
        public override AuthorizationRequirement AuthorizationRequirement => AuthorizationRequirement.Required;

        public sealed override HttpMethod Method => HttpMethod.Post;
    }
}
