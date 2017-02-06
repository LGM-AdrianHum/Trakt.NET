﻿namespace TraktApiSharp.Experimental.Requests.Users.OAuth
{
    using Base;
    using Extensions;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using TraktApiSharp.Requests;

    internal abstract class ATraktUsersDeleteByIdRequest : ATraktDeleteRequest, ITraktHasId
    {
        public string Id { get; set; }

        public abstract TraktRequestObjectType RequestObjectType { get; }

        public override IDictionary<string, object> GetUriPathParameters()
            => new Dictionary<string, object>
            {
                ["id"] = Id
            };

        public override void Validate()
        {
            if (Id == null)
                throw new ArgumentNullException(nameof(Id));

            if (Id == string.Empty || Id.ContainsSpace())
                throw new ArgumentException("id not valid", nameof(Id));
        }
    }
}
