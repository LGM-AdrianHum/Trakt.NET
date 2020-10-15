﻿namespace TraktNet.Objects.Post.Users.HiddenItems
{
    using Objects.Json;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// An user hidden items post, containing all movies, shows, and / or episodes,
    /// which should be added to an user's hidden items list.
    /// </summary>
    public class TraktUserHiddenItemsPost : ITraktUserHiddenItemsPost
    {
        /// <summary>
        /// An optional list of <see cref="ITraktUserHiddenItemsPostMovie" />s.
        /// <para>Each <see cref="ITraktUserHiddenItemsPostMovie" /> must have at least a valid Trakt id.</para>
        /// </summary>
        public IEnumerable<ITraktUserHiddenItemsPostMovie> Movies { get; set; }

        /// <summary>
        /// An optional list of <see cref="ITraktUserHiddenItemsPostShow" />s.
        /// <para>Each <see cref="ITraktUserHiddenItemsPostShow" /> must have at least a valid Trakt id.</para>
        /// </summary>
        public IEnumerable<ITraktUserHiddenItemsPostShow> Shows { get; set; }

        /// <summary>
        /// An optional list of <see cref="ITraktUserHiddenItemsPostSeason" />s.
        /// <para>Each <see cref="ITraktUserHiddenItemsPostSeason" /> must have at least a valid Trakt id and a name.</para>
        /// </summary>
        public IEnumerable<ITraktUserHiddenItemsPostSeason> Seasons { get; set; }

        public Task<string> ToJson(CancellationToken cancellationToken = default)
        {
            IObjectJsonWriter<ITraktUserHiddenItemsPost> objectJsonWriter = JsonFactoryContainer.CreateObjectWriter<ITraktUserHiddenItemsPost>();
            return objectJsonWriter.WriteObjectAsync(this, cancellationToken);
        }

        public void Validate()
        {
            // TODO
        }
    }
}
