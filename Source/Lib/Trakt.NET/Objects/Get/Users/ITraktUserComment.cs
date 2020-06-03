﻿namespace TraktNet.Objects.Get.Users
{
    using Basic;
    using Enums;
    using Episodes;
    using Lists;
    using Movies;
    using Seasons;
    using Shows;

    /// <summary>A Trakt user comment.</summary>
    public interface ITraktUserComment
    {
        /// <summary>
        /// Gets or sets the object type, which this comment contains.
        /// See also <seealso cref="TraktObjectType" />.
        /// <para>Nullable</para>
        /// </summary>
        TraktObjectType Type { get; set; }

        /// <summary>Gets or sets the comment's content.<para>Nullable</para></summary>
        ITraktComment Comment { get; set; }

        /// <summary>
        /// Gets or sets the movie, if <see cref="Type" /> is <see cref="TraktObjectType.Movie" />.
        /// See also <seealso cref="ITraktMovie" />.
        /// <para>Nullable</para>
        /// </summary>
        ITraktMovie Movie { get; set; }

        /// <summary>
        /// Gets or sets the show, if <see cref="Type" /> is <see cref="TraktObjectType.Episode" />.
        /// See also <seealso cref="ITraktShow" />.
        /// <para>Nullable</para>
        /// </summary>
        ITraktShow Show { get; set; }

        /// <summary>
        /// Gets or sets the season, if <see cref="Type" /> is <see cref="TraktObjectType.Episode" />.
        /// See also <seealso cref="ITraktSeason" />.
        /// <para>Nullable</para>
        /// </summary>
        ITraktSeason Season { get; set; }

        /// <summary>
        /// Gets or sets the episode, if <see cref="Type" /> is <see cref="TraktObjectType.Episode" />.
        /// See also <seealso cref="ITraktEpisode" />.
        /// <para>Nullable</para>
        /// </summary>
        ITraktEpisode Episode { get; set; }

        /// <summary>
        /// Gets or sets the list, if <see cref="Type" /> is <see cref="TraktObjectType.Episode" />.
        /// See also <seealso cref="ITraktList" />.
        /// <para>Nullable</para>
        /// </summary>
        ITraktList List { get; set; }
    }
}
