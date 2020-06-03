﻿namespace TraktNet.Objects.Post.Syncs.History.Responses
{
    /// <summary>
    /// Represents the response for a history remove post. See also <see cref="ITraktSyncHistoryRemovePost" />.
    /// <para>Contains the number of deleted and not found movies, shows, seasons, episodes and history item ids.</para>
    /// </summary>
    public interface ITraktSyncHistoryRemovePostResponse
    {
        /// <summary>
        /// A collection containing the number of deleted movies, shows, seasons, episodes and history item ids.
        /// <para>Nullable</para>
        /// </summary>
        ITraktSyncHistoryRemovePostResponseGroup Deleted { get; set; }

        /// <summary>
        /// A collection containing the ids of movies, shows, seasons, episodes and history item ids, which were not found.
        /// <para>Nullable</para>
        /// </summary>
        ITraktSyncHistoryRemovePostResponseNotFoundGroup NotFound { get; set; }
    }
}
