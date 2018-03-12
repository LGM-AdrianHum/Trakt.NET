﻿namespace TraktApiSharp.Objects.Post.Comments.Implementations
{
    using Basic;
    using System.Net.Http;

    public abstract class TraktCommentPost : ITraktCommentPost
    {
        /// <summary>Gets or sets the required comment's content.</summary>
        public string Comment { get; set; }

        /// <summary>Gets or sets, whether the comment contains spoiler.</summary>
        public bool? Spoiler { get; set; }

        /// <summary>
        /// Gets or sets the sharing options for the comment post.
        /// See also <seealso cref="ITraktSharing" />.
        /// <para>Nullable</para>
        /// </summary>
        public ITraktSharing Sharing { get; set; }

        public abstract string HttpContentAsString { get; set; }

        public abstract HttpContent ToHttpContent();

        public abstract void Validate();
    }
}
