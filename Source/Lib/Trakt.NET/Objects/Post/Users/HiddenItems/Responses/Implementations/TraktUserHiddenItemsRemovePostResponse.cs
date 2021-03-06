﻿namespace TraktNet.Objects.Post.Users.HiddenItems.Responses
{
    /// <summary>
    /// Represents the response for an user hidden items remove post. See also <see cref="ITraktUserHiddenItemsPost" />.
    /// <para>Contains the number of deleted and not found movies, shows and seasons.</para>
    /// </summary>
    public class TraktUserHiddenItemsRemovePostResponse : ITraktUserHiddenItemsRemovePostResponse
    {
        /// <summary>
        /// A collection containing the number of deleted movies, shows and seasons.
        /// <para>Nullable</para>
        /// </summary>
        public ITraktUserHiddenItemsPostResponseGroup Deleted { get; set; }

        /// <summary>
        /// A collection containing the ids of movies, shows and seasons, which were not found.
        /// <para>Nullable</para>
        /// </summary>
        public ITraktUserHiddenItemsPostResponseNotFoundGroup NotFound { get; set; }
    }
}
